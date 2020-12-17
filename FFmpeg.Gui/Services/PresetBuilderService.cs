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
        private readonly Regex _control;

        public PresetBuilderService()
        {
            _control = new Regex("\\{[a-zA-Z0-9.]+\\}", RegexOptions.Compiled);
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

            List<string> results = new List<string>(preset.ArgumentCollection.Count + 1);
            results.Add($"\"{ffmpeg}\"");
            foreach (var argument in preset.ArgumentCollection)
            {
                var matches = _control.Matches(argument);
                if (matches.Count > 0)
                {
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

                        string render = string.Empty;

                        switch (element)
                        {
                            case SliderWithValueText slider:
                                render = PresetBuilder.RenderSlider(argument, match.Value, slider);
                                break;
                            case VideoScaleInput videoScale:
                                render = PresetBuilder.RenderVideoScale(argument, match.Value, videoScale);
                                break;
                            case TimeSpanInput videoTime:
                                render = PresetBuilder.RenderVideoTime(argument, match.Value, subname, videoTime);
                                break;
                        }

                        results.Add(render);
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

            return JoinArgumentsToString(args);
        }

        private string JoinArgumentsToString(string[] args)
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
