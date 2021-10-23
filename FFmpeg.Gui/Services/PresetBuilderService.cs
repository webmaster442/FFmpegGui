//-----------------------------------------------------------------------------
// (c) 2020-2021 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using FFmpeg.Gui.Controls;
using FFmpeg.Gui.Domain;
using FFmpeg.Gui.Interfaces;
using FFmpeg.Gui.Presets;
using FFmpeg.Gui.Properties;
using FFmpeg.Gui.ServiceCode;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;

namespace FFmpeg.Gui.Services
{
    internal class PresetBuilderService : IPresetBuilderService
    {
        private readonly Regex _controlName;

        public PresetBuilderService()
        {
            _controlName = new Regex("\\{[a-zA-Z0-9.]+\\}", RegexOptions.Compiled);
        }

        public string Build(IRenderPanel source,
                            Preset? preset,
                            IList<string> files,
                            string OutputDirectory,
                            string ffmpeg,
                            FileHandlingMode fileHandlingMode)
        {
            StringBuilder result = new StringBuilder();
            result.Append(Resources.ScriptHeader.Replace("<ffmpegLocation>", ffmpeg));


            List<string> presetArgValues = ProcessPreset(source, preset);


            foreach (var file in files)
            {

                var outname = Path.Combine(OutputDirectory, Path.GetFileName(file));
                outname = Path.ChangeExtension(outname, preset?.TargetExtension ?? string.Empty);

                var line = RenderSingleFile(presetArgValues.ToArray(), file, outname);
                WrapInAction(result, line, outname, fileHandlingMode);
            }

            return result.ToString();
        }

        private void WrapInAction(StringBuilder results, string command, string file, FileHandlingMode fileHandlingMode)
        {
            results.Append($"if (Test-Path \"{file}\" -PathType Leaf)");
            results.Append(" {\r\n");
            switch (fileHandlingMode)
            {
                case FileHandlingMode.DeleteIfExists:
                    results.AppendFormat("\tRemove-Item \"{0}\"", file);
                    break;
                case FileHandlingMode.RenameIfExists:
                    RenameIfExists(results, file);
                    break;
                case FileHandlingMode.OwerwriteNotify:
                    results.AppendFormat("\techo \"File {0} exists and will be owerwitten.\"\r\n", file);
                    results.AppendLine("\tRead-Host -Prompt \"Press any key to continue\"");
                    results.AppendFormat("\tRemove-Item \"{0}\"", file);
                    break;
            }
            results.Append("}\r\n");
            results.AppendLine(command);
        }

        private static void RenameIfExists(StringBuilder results, string file)
        {
            var newName = $"{Path.GetFileNameWithoutExtension(file)}_backup{Path.GetExtension(file)}";
            results.AppendFormat("\tRename-Item -Path \"{0}\" -NewName \"{1}\"\r\n", file, newName);
        }

        private List<string> ProcessPreset(IRenderPanel source, Preset? preset)
        {
            if (preset == null)
                return new List<string>();

            List<string> results = new List<string>(preset.ArgumentCollection.Length + 1);
            results.Add($"& $ffmpeg");
            foreach (var argument in preset.ArgumentCollection)
            {
                var matches = _controlName.Matches(argument);
                if (matches.Count > 0)
                {
                    //Argument collection has a controller dependent value
                    foreach (Match? match in matches)
                    {
                        if (match == null) continue;

                        string name = match.Value.Replace("{", "").Replace("}", "");
                        string subname = string.Empty;
                        if (name.Contains("."))
                        {
                            var parts = name.Split('.');
                            name = parts[0];
                            subname = parts[1];
                        }

                        FrameworkElement? element = source.GetElement(name);

                        string valueToInsert = string.Empty;

                        switch (element)
                        {
                            case SliderWithValueText slider:
                                valueToInsert = PresetBuilder.ProcessSlider(argument, match.Value, slider);
                                break;
                            case VideoScaleInput videoScale:
                                valueToInsert = PresetBuilder.ProcessVideoScale(argument, match.Value, videoScale);
                                break;
                            case TimeSpanInput videoTime:
                                valueToInsert = PresetBuilder.ProcessVideoTime(argument, match.Value, subname, videoTime);
                                break;
                            case OptionSelector optionSelector:
                                valueToInsert = PresetBuilder.ProcessOptionSelector(argument, match.Value, optionSelector);
                                break;
                        }

                        results.Add(valueToInsert);
                    }
                }
                else
                {
                    results.Add(argument);
                }
            }
            return results;
        }

        private static string RenderSingleFile(string[] args, string file, string outputFile)
        {
            const string sourcefile = "%source%";
            const string targetfile = "%target%";

            for (int i = 0; i < args.Length; i++)
            {
                if (args[i] == sourcefile)
                {
                    args[i] = $"\"{file}\"";
                }
                else if (args[i] == targetfile)
                {
                    args[i] = $"\"{outputFile}\"";
                }
            }

            return JoinArgumentsToString(args);
        }

        private static string JoinArgumentsToString(string[] args)
        {
            var sb = new StringBuilder();
            foreach (var arg in args)
            {
                if (!string.IsNullOrEmpty(arg))
                {
                    sb.Append(arg);
                    sb.Append(' ');
                }
            }
            return sb.ToString().Trim();
        }
    }
}
