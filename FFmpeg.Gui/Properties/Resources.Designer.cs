﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FFmpeg.Gui.Properties {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("FFmpeg.Gui.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Preset: Invalid time range. Start time is bigger then End time.
        /// </summary>
        internal static string Error_CutPreset_InvalidRange {
            get {
                return ResourceManager.GetString("Error_CutPreset_InvalidRange", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Preset: Start time can&apos;t be negative.
        /// </summary>
        internal static string Error_CutPreset_NegativeStart {
            get {
                return ResourceManager.GetString("Error_CutPreset_NegativeStart", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Failed to execute job..
        /// </summary>
        internal static string Error_Execute {
            get {
                return ResourceManager.GetString("Error_Execute", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to FFMpeg Not found..
        /// </summary>
        internal static string Error_FFmpeg {
            get {
                return ResourceManager.GetString("Error_FFmpeg", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Failed to load the selected file.
        /// </summary>
        internal static string Error_FileLoad {
            get {
                return ResourceManager.GetString("Error_FileLoad", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Failed to write the file.
        /// </summary>
        internal static string Error_FileWrite {
            get {
                return ResourceManager.GetString("Error_FileWrite", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to No input files are selected.
        /// </summary>
        internal static string Error_NoInputFiles {
            get {
                return ResourceManager.GetString("Error_NoInputFiles", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Output directory doesn&apos;t exist.
        /// </summary>
        internal static string Error_OutDirectory {
            get {
                return ResourceManager.GetString("Error_OutDirectory", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to No preset is selected.
        /// </summary>
        internal static string Error_Preset {
            get {
                return ResourceManager.GetString("Error_Preset", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The currently selected preset has invalid values..
        /// </summary>
        internal static string Error_Preset_InvalidState {
            get {
                return ResourceManager.GetString("Error_Preset_InvalidState", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Failed to save Job script..
        /// </summary>
        internal static string Error_Save {
            get {
                return ResourceManager.GetString("Error_Save", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Can&apos;t parse input text as a valid Time.
        /// </summary>
        internal static string Error_Timespan_IncorrectFormat {
            get {
                return ResourceManager.GetString("Error_Timespan_IncorrectFormat", resourceCulture);
            }
        }
    }
}
