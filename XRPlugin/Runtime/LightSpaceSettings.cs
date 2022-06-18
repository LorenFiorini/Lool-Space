// -----------------------------------------------------------------------
//  <copyright file="LightSpaceSettings.cs" company="LightSpace">
//    Copyright (c) LightSpace. All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Unity.XR.LightSpace
{
    using System;
    using System.IO;
    using UnityEditor;
    using UnityEngine;
    using UnityEngine.XR.Management;

    /// <summary>
    /// Custom XR Settings for LightSpace XR Plugin.
    /// </summary>
    [Serializable]
    [XRConfigurationData("LightSpace", "Unity.XR.LightSpace.Settings")]
    public class LightSpaceSettings : ScriptableObject
    {
        /// <summary>
        /// Stereo rendering modes.
        /// </summary>
        public enum StereoRenderingMode
        {
            /// <summary>
            /// Submit separate draw calls for each eye.
            /// </summary>
            MultiPass = 0,

            /// <summary>
            /// Submit one draw call for both eyes.
            /// </summary>
            SinglePassInstanced = 1
        }

        /// <summary>
        /// Mirror view modes
        /// </summary>
        public enum MirrorViewModes
        {
            /// <summary>
            /// Copy left eye to preview.
            /// </summary>
            Left = 0,

            /// <summary>
            /// Copy right eye to preview.
            /// </summary>
            Right = 1,

            /// <summary>
            /// Don't copy to preview.
            /// </summary>
            None = 2
        }

        public enum TrackingMode
        {
            /// <summary>
            /// The tracking mode is disabled
            /// </summary>
            Disabled = 0,

            /// <summary>
            /// The default tracking mode - tracking service and plugin
            /// </summary>
            Default = 1,
        }

        /// <summary>
        /// The current stereo rendering mode.
        /// </summary>
        [SerializeField, Tooltip("Set the Stereo Rendering Method")]
        public StereoRenderingMode CurrentRenderingMode;

        /// <summary>
        /// The current mirror view mode.
        /// </summary>
        [SerializeField, Tooltip("Which eye to use when rendering the headset view to the main window (left, right, or none)")]
        public MirrorViewModes MirrorView = MirrorViewModes.Right;

        /// <summary>
        /// The current tracking mode.
        /// </summary>
        [SerializeField, Tooltip("Set the Tracking Mode, e.g. disabled to ignore tracking or default for plugin based tracking")]
        public TrackingMode CurrentTrackingMode = TrackingMode.Disabled;

        /// <summary>
        /// The current name of the Tracking Plugin used with Tracking Service.
        /// </summary>
        [SerializeField, Tooltip("A name of LightSpace Tracking Service Plugin to use.")]
        public string TrackingPluginName = string.Empty;

        /// <summary>
        /// Value indicating if user has overriden path to Compositor Host.
        /// </summary>
        [SerializeField, Tooltip("Use custom path to LightSpace Compositor Host.")]
        public bool OverrideCompositorHostPath = false;

        /// <summary>
        /// Value indicating if user has overriden path to Tracking Service Host.
        /// </summary>
        [SerializeField, Tooltip("Use custom path to LightSpace Tracking Service Host.")]
        public bool OverrideTrackingServiceHostPath = false;

        /// <summary>
        /// Value indicating if user has overriden path to Tracking Service Plugin.
        /// </summary>
        [SerializeField, Tooltip("Use custom path to LightSpace Tracking Service Plugin.")]
        public bool OverrideTrackingServicePluginPath = false;

        /// <summary>
        /// Path to Compositor Host set by user.
        /// </summary>
        [SerializeField, Tooltip("Current path to LightSpace Compositor Host.")]
        public string UserSetCompositorHostPath;

        /// <summary>
        /// Path to Tracking Service Host set by user.
        /// </summary>
        [SerializeField, Tooltip("Current path to LightSpace Tracking Service Host.")]
        public string UserSetTrackingServiceHostPath;

        /// <summary>
        /// Path to Tracking Service Plugin Directory set by user.
        /// </summary>
        [SerializeField, Tooltip("Current path to LightSpace Tracking Service Plugin.")]
        public string UserSetTrackingServicePluginPath;

        /// <summary>
        /// The default path to Compositor Host in Editor.
        /// </summary>
        [SerializeField, Tooltip("Internal value that tells the system path to Compositor Host while application in the editor")]
        private const string CompositorHostPathEditor = "Packages/com.lightspace.xr/Runtime/windows/x64/CompositorHost/LightspaceCompositorHost.exe";

        /// <summary>
        /// The default path to Tracking Service Host in Editor.
        /// </summary>
        [SerializeField, Tooltip("Internal value that tells the system path to Tracking Service Host while application in the editor")]
        private const string TrackingServiceHostPathEditor = "Packages/com.lightspace.xr/Runtime/windows/x64/TrackingServiceHost/LightspaceTrackingServiceHost.exe";

        /// <summary>
        /// The default path to Tracking Service Plugin Directory in Editor.
        /// </summary>
        [SerializeField, Tooltip("Internal value that tells the system path to Tracking Service Plugin directory while application in the editor.")]
        public string TrackingServicePluginDirEditor = "Packages/com.lightspace.xr/Runtime/windows/x64/TrackingServiceHost/TrackingPlugins/";

        /// <summary>
        /// The default path to Compositor Host in Standalone Build.
        /// </summary>
        [SerializeField, Tooltip("Internal value that tells the system path to Compositor Host in Standalone build.")]
        public string CompositorHostPath;

        /// <summary>
        /// The default path to Tracking Service Host in Standalone Build.
        /// </summary>
        [SerializeField, Tooltip("Internal value that tells the system path to Tracking Service Host in Standalone build")]
        public string TrackingServiceHostPath;

        /// <summary>
        /// The default path to Tracking Service Plugin Directory in Standalone Build.
        /// </summary>
        [SerializeField, Tooltip("Internal value that tells the system path to Tracking Service Plugin Directory in Standalone Build")]
        public string TrackingServicePluginDir;

        /// <summary>
        /// Gets the current stereo rendering mode.
        /// </summary>
        /// <returns>Current stereo rendering mode as ushort.</returns>
        public ushort GetStereoRenderingMode()
        {
            return (ushort)CurrentRenderingMode;
        }

        /// <summary>
        /// Gets the current mirror view mode.
        /// </summary>
        /// <returns>Current mirror view mode as ushort.</returns>
        public ushort GetMirrorViewMode()
        {
            return (ushort)MirrorView;
        }

        /// <summary>
        /// Gets the current tracking mode.
        /// </summary>
        /// <returns>Current tracking mode as ushort.</returns>
        public ushort GetTrackingMode()
        {
            return (ushort)CurrentTrackingMode;
        }

        /// <summary>
        /// Gets the path to Compositor Host.
        /// </summary>
        /// <returns>Current path to Compositor Host.</returns>
        public string GetCompositorHostPath()
        {
            if (OverrideCompositorHostPath)
            {
                return UserSetCompositorHostPath;
            }

#if UNITY_EDITOR
            return Path.GetFullPath(CompositorHostPathEditor);
#else
            return CompositorHostPath;
#endif
        }

        /// <summary>
        /// Gets the path to Tracking Service Host.
        /// </summary>
        /// <returns>Current path to Tracking Service Host.</returns>
        public string GetTrackingServiceHostPath()
        {
            if (OverrideTrackingServiceHostPath)
            {
                return UserSetTrackingServiceHostPath;
            }

#if UNITY_EDITOR
            return Path.GetFullPath(TrackingServiceHostPathEditor);
#else
            return TrackingServiceHostPath;
#endif
        }

        /// <summary>
        /// Gets the path to Tracking Service Plugin.
        /// </summary>
        /// <returns>Current path to Tracking Service Plugin.</returns>
        public string GetTrackingServicePluginPath()
        {
            if (OverrideTrackingServicePluginPath)
            {
                return UserSetTrackingServicePluginPath;
            }

#if UNITY_EDITOR
            return Path.GetFullPath(Path.Combine(TrackingServicePluginDirEditor, TrackingPluginName));
#else
            return Path.Combine(TrackingServicePluginDir, TrackingPluginName);
#endif
        }

#if !UNITY_EDITOR
        /// <summary>
        /// Static instance that will hold the runtime asset instance created in build process.
        /// </summary>
        public static LightSpaceSettings s_Settings;

        /// <summary>
        /// Awake is called when the script instance is being loaded.
        /// </summary>
        public void Awake()
        {
            s_Settings = this;
        }
#endif
    }
}