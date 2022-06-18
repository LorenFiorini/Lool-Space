// -----------------------------------------------------------------------
//  <copyright file="LightSpaceLoader.cs" company="LightSpace">
//    Copyright (c) LightSpace. All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Unity.XR.LightSpace
{
    using System.Collections.Generic;
    using System.Runtime.InteropServices;
#if UNITY_EDITOR
    using UnityEditor;
#endif
    using UnityEngine;
    using UnityEngine.XR;
    using UnityEngine.XR.Management;

    /// <summary>
    /// XR Loader class for initialization and management of LightSpace XR providers.
    /// </summary>
    public class LightSpaceLoader : XRLoaderHelper
#if UNITY_EDITOR
    , IXRLoaderPreInit
#endif
    {
        /// <summary>
        /// The XR display subsystem descriptors.
        /// </summary>
        private static readonly List<XRDisplaySubsystemDescriptor> DisplaySubsystemDescriptors = new List<XRDisplaySubsystemDescriptor>();

        /// <summary>
        /// The XR input subsystem descriptors.
        /// </summary>
        private static readonly List<XRInputSubsystemDescriptor> InputSubsystemDescriptors = new List<XRInputSubsystemDescriptor>();

        /// <summary>
        /// The XR display subsystem.
        /// </summary>
        public XRDisplaySubsystem DisplaySubsystem => this.GetLoadedSubsystem<XRDisplaySubsystem>();

        /// <summary>
        /// The XR input subsystem.
        /// </summary>
        public XRInputSubsystem InputSubsystem => this.GetLoadedSubsystem<XRInputSubsystem>();

        /// <summary>
        /// Initializes the loader.
        /// This should initialize all subsystems to support the desired runtime setup this loader represents.
        /// </summary>
        /// <returns> Whether initialization succeeded.</returns>
        public override bool Initialize()
        {
            var settings = GetSettings();
            if (settings != null)
            {
                UserDefinedSettings userDefinedSettings;
                userDefinedSettings.stereoRenderingMode = settings.GetStereoRenderingMode();
                userDefinedSettings.mirrorViewMode = settings.GetMirrorViewMode();
                userDefinedSettings.trackingMode = settings.GetTrackingMode();
                userDefinedSettings.compositorHostPath = settings.GetCompositorHostPath();
                userDefinedSettings.trackingServiceHostPath = settings.GetTrackingServiceHostPath();
                userDefinedSettings.trackingPluginPath = settings.GetTrackingServicePluginPath();
                SetUserDefinedSettings(userDefinedSettings);
            }

            this.CreateSubsystem<XRDisplaySubsystemDescriptor, XRDisplaySubsystem>(DisplaySubsystemDescriptors, "LightSpace Display");
            this.CreateSubsystem<XRInputSubsystemDescriptor, XRInputSubsystem>(InputSubsystemDescriptors, "LightSpace Head Tracking");

            if (this.DisplaySubsystem == null || this.InputSubsystem == null)
            {
                Debug.LogError("Unable to start LightSpace XR Plugin.");
            }

            if (this.DisplaySubsystem == null)
            {
                Debug.LogError("Failed to load LightSpace display subsystem.");
            }

            if (this.InputSubsystem == null)
            {
                Debug.LogError("Failed to load LightSpace input subsystem.");
            }

            return this.DisplaySubsystem != null && this.InputSubsystem != null;
        }

        /// <summary>
        /// Starts all initialized subsystems.
        /// </summary>
        /// <returns> Whether all subsystems were successfully started.</returns>
        public override bool Start()
        {
            this.StartSubsystem<XRDisplaySubsystem>();
            this.StartSubsystem<XRInputSubsystem>();

            return true;
        }

        /// <summary>
        /// Stops all initialized subsystems.
        /// </summary>
        /// <returns> Whether all subsystems were successfully stopped.</returns>
        public override bool Stop()
        {
            this.StopSubsystem<XRDisplaySubsystem>();
            this.StopSubsystem<XRInputSubsystem>();

            return true;
        }

        /// <summary>
        /// De-initializes all initialized subsystems.
        /// </summary>
        /// <returns> Whether de-initialization succeeded.</returns>
        public override bool Deinitialize()
        {
            this.DestroySubsystem<XRDisplaySubsystem>();
            this.DestroySubsystem<XRInputSubsystem>();

            return true;
        }

        [StructLayout(LayoutKind.Sequential)]
        struct UserDefinedSettings
        {
            public ushort stereoRenderingMode;
            public ushort mirrorViewMode;
            public ushort trackingMode;
            public string compositorHostPath;
            public string trackingServiceHostPath;
            public string trackingPluginPath;
        }

        [DllImport("LightSpaceXR", CharSet = CharSet.Auto)]
        static extern void SetUserDefinedSettings(UserDefinedSettings settings);

        /// <summary>
        /// Gets the LightSpaceSettings
        /// </summary>
        /// <returns>
        /// The current <see cref="LightSpaceSettings"/>.</returns>
        public LightSpaceSettings GetSettings()
        {
            LightSpaceSettings settings = null;
#if UNITY_EDITOR
            UnityEditor.EditorBuildSettings.TryGetConfigObject<LightSpaceSettings>("Unity.XR.LightSpace.Settings", out settings);
#else
            settings = LightSpaceSettings.s_Settings;
#endif
            return settings;
        }

#if UNITY_EDITOR
        public string GetPreInitLibraryName(BuildTarget buildTarget, BuildTargetGroup buildTargetGroup)
        {
            return "LightSpaceXR";
        }
#endif
    }
}