using FFmpeg.Gui.Controls;
using FFmpeg.Gui.Domain;
using FFmpeg.Gui.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Windows;

namespace FFmpeg.Gui.Services
{
    internal class PresetBuilderService: IPresetBuilderService
    {
        public string Build(IRenderPanel source, Preset preset, IList<string> files, string OutputDirectory)
        {
            StringBuilder results = new StringBuilder();

            List<string> presetArgValues = ProcessPreset(source, preset);


            foreach (var file in files)
            {
                var line = RenderSingleFile(CopyList(presetArgValues), file, preset.TargetExtension, OutputDirectory);
                results.AppendLine(line);
            }

            return results.ToString();
        }

        private List<string> ProcessPreset(IRenderPanel source, Preset preset)
        {
            List<string> results = new List<string>(preset.ArgumentCollection.Count);
            foreach (var argument in preset.ArgumentCollection)
            {
                if (argument.StartsWith("{")
                    && argument.EndsWith("}"))
                {
                    string name = argument.Substring(1, argument.Length - 2);
                    FrameworkElement element = source.GetElement(name);
                    switch (element)
                    {
                        case SliderWithValueText slider:
                            results.Add(slider.Value.ToString(CultureInfo.InvariantCulture));
                            break;
                    }
                }
                else
                {
                    results.Add(argument);
                }
            }
            return results;
        }

        private string[] CopyList(List<string> inputs)
        {
            string[] result = new string[inputs.Count];
            for (int i=0; i<result.Length; i++)
            {
                result[i] = inputs[0];
            }
            return result;
        }

        private string RenderSingleFile(string[] args, string file, string targetExtension, string outputDirectory)
        {
            const string sourcefile = "%source%";
            const string targetfile = "%target%";

            var outname = Path.Combine(outputDirectory, Path.GetFileName(file));

            for (int i=0; i<args.Length; i++)
            {
                if (args[i] == sourcefile)
                {
                    args[i] = file;
                }
                else if (args[i] == targetfile
                    && !string.IsNullOrEmpty(targetExtension))
                {
                    args[i] = Path.ChangeExtension(outname, targetExtension);
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
                case JobOutputFormat.Bash:
                    return "#!/bin/bash\r\n";
                case JobOutputFormat.Powershell:
                    return "";
                default:
                    throw new ArgumentException(nameof(outputFormat));
            }
        }
    }
}
