//-----------------------------------------------------------------------------
// (c) 2020 Ruzsinszki Gábor
// This code is licensed under MIT license (see LICENSE for details)
//-----------------------------------------------------------------------------

using FFmpeg.Gui.Controls;
using FFmpeg.Gui.Domain;
using FFmpeg.Gui.Interfaces;
using FFmpeg.Gui.Presets;
using FFmpeg.Gui.ServiceCode;
using System;
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
                            string ffmpeg)
        {
            StringBuilder results = new StringBuilder();
            List<string> presetArgValues = ProcessPreset(ffmpeg, source, preset);


            foreach (var file in files)
            {
                var line = RenderSingleFile(presetArgValues.ToArray(), file, preset?.TargetExtension ?? string.Empty, OutputDirectory);
                results.AppendLine(line);
            }

            return results.ToString();
        }

        private List<string> ProcessPreset(string ffmpeg, IRenderPanel source, Preset? preset)
        {
            if (preset == null)
                return new List<string>();

            List<string> results = new List<string>(preset.ArgumentCollection.Length + 1);
            results.Add($"\"{ffmpeg}\"");
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

        private static string RenderSingleFile(string[] args, string file, string targetExtension, string outputDirectory)
        {
            const string sourcefile = "%source%";
            const string targetfile = "%target%";

            var outname = Path.Combine(outputDirectory, Path.GetFileName(file));

            for (int i = 0; i < args.Length; i++)
            {
                if (args[i] == sourcefile)
                {
                    args[i] = $"\"{file}\"";
                }
                else if (args[i] == targetfile
                    && !string.IsNullOrEmpty(targetExtension))
                {
                    args[i] = $"\"{Path.ChangeExtension(outname, targetExtension)}\"";
                }
            }

            return JoinArgumentsToString(args);
        }

        private static string JoinArgumentsToString(string[] args)
        {
            StringBuilder sb = new StringBuilder();
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

        public string GetShellScriptHeader(JobOutputFormat outputFormat)
        {
            return outputFormat switch
            {
                JobOutputFormat.Bach => "@echo off\r\nTITLE FFMpeg job\r\n",
                JobOutputFormat.Powershell => "",
                _ => throw new ArgumentException(nameof(outputFormat)),
            };
        }
    }
}
