using FFmpeg.Gui.Controls;
using FFmpeg.Gui.Domain;
using FFmpeg.Gui.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Windows;
using System.Text.RegularExpressions;

namespace FFmpeg.Gui.Services
{
    internal class PresetBuilderService : IPresetBuilderService
    {
        private readonly Regex _control;

        public PresetBuilderService()
        {
            _control = new Regex("\\{[a-zA-Z0-9]+\\}", RegexOptions.Compiled);
        }

        public string Build(IRenderPanel source,
                            Preset preset,
                            IList<string> files,
                            string OutputDirectory,
                            string ffmpeg)
        {
            StringBuilder results = new StringBuilder();
            List<string> presetArgValues = ProcessPreset(ffmpeg, source, preset);


            foreach (var file in files)
            {
                var line = RenderSingleFile(presetArgValues.ToArray(), file, preset.TargetExtension, OutputDirectory);
                results.AppendLine(line);
            }

            return results.ToString();
        }

        private List<string> ProcessPreset(string ffmpeg, IRenderPanel source, Preset preset)
        {
            List<string> results = new List<string>(preset.ArgumentCollection.Count + 1);
            results.Add($"\"{ffmpeg}\"");
            foreach (var argument in preset.ArgumentCollection)
            {
                var matches = _control.Matches(argument);
                if (matches.Count > 0)
                {
                    foreach (Match match in matches)
                    {
                        if (match == null) continue;

                        string name = match.Value.Replace("{", "").Replace("}", "");
                        FrameworkElement element = source.GetElement(name);

                        switch (element)
                        {
                            case SliderWithValueText slider:
                                var slid = argument.Replace(match.Value, slider.Value.ToString(CultureInfo.InvariantCulture));
                                results.Add(slid);
                                break;
                        }

                    }
                }
                else
                {
                    results.Add(argument);
                }
            }
            return results;
        }

        private string RenderSingleFile(string[] args, string file, string targetExtension, string outputDirectory)
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

            return string.Join(' ', args);
        }

        public string GetShellScriptHeader(JobOutputFormat outputFormat)
        {
            switch (outputFormat)
            {
                case JobOutputFormat.Bach:
                    return "@echo off\r\nTITLE FFMpeg job\r\n";
                case JobOutputFormat.Powershell:
                    return "";
                default:
                    throw new ArgumentException(nameof(outputFormat));
            }
        }
    }
}
