// -----------------------------------------------------------------------
//  <copyright file="LightSpaceBuildProcessor.cs" company="LightSpace">
//    Copyright (c) LightSpace. All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------

using JetBrains.Annotations;

namespace UnityEditor.XR.LightSpace
{
    using System.IO;
    using System.Linq;
    using UnityEngine;
    using UnityEngine.Rendering;
    using UnityEditor.Build;
    using UnityEditor.Build.Reporting;
    using Unity.XR.LightSpace;
    using UnityEditor;
    using UnityEditor.XR.Management;

    /// <summary>
    /// The LightSpace build processor.
    /// </summary>
    public class LightSpaceBuildProcessor : XRBuildHelper<LightSpaceSettings>
    {
        /// <summary>
        /// The build settings key.
        /// </summary>
        public override string BuildSettingsKey => "Unity.XR.LightSpace.Settings";
    }

    /// <summary>
    /// The LightSpace build tools.
    /// </summary>
    public static class LightSpaceBuildTools
    {
        /// <summary>
        /// Gets whether the LightSpace loader is present in settings for build target.
        /// </summary>
        /// <param name="buildTargetGroup"> The build target group. </param>
        public static bool LightSpaceLoaderPresentInSettingsForBuildTarget(BuildTargetGroup buildTargetGroup)
        {
            var generalSettingsForBuildTarget = XRGeneralSettingsPerBuildTarget.XRGeneralSettingsForBuildTarget(buildTargetGroup);
            if (!generalSettingsForBuildTarget)
            {
                return false;
            }

            var settings = generalSettingsForBuildTarget.AssignedSettings;
            if (!settings)
            {
                return false;
            }

            var loaders = settings.loaders;
            return loaders.Exists(loader => loader is LightSpaceLoader);
        }

        /// <summary>
        /// Gets the LightSpaceSettings
        /// </summary>
        /// <returns>
        /// The current <see cref="LightSpaceSettings"/>.</returns>
        public static LightSpaceSettings GetSettings()
        {
            LightSpaceSettings settings = null;
#if UNITY_EDITOR
            UnityEditor.EditorBuildSettings.TryGetConfigObject<LightSpaceSettings>("Unity.XR.LightSpace.Settings", out settings);
#else
            settings = LightSpaceSettings.s_Settings;
#endif
            return settings;
        }
    }

    /// <summary>
    /// Checks for play mode while in settings editor.
    /// </summary>
    [InitializeOnLoad]
    public static class EnterPlayModeSettingsCheck
    {
        /// <summary>
        /// Initializes static members of the <see cref="EnterPlayModeSettingsCheck"/> class.
        /// </summary>
        static EnterPlayModeSettingsCheck()
        {
            EditorApplication.playModeStateChanged += PlayModeStateChangedEvent;
        }

        /// <summary>
        /// The play mode state changed event.
        /// </summary> <param name="state">The current play mode state.</param>
        private static void PlayModeStateChangedEvent(PlayModeStateChange state)
        {
            if (state == PlayModeStateChange.EnteredPlayMode)
            {
                if (!LightSpaceBuildTools.LightSpaceLoaderPresentInSettingsForBuildTarget(BuildTargetGroup.Standalone))
                {
                    return;
                }

                if (PlayerSettings.GetGraphicsAPIs(BuildTarget.StandaloneWindows)[0] != GraphicsDeviceType.Direct3D11)
                {
                    Debug.LogError("D3D11 is currently the only graphics API compatible with the LightSpace XR Plugin on desktop platforms. Please change the preferred Graphics API setting in Player Settings.");
                }
            }
        }
    }

    /// <summary>
    /// The LightSpace pre build settings.
    /// </summary>
    internal class LightSpacePreBuildSettings : IPreprocessBuildWithReport, IPostprocessBuildWithReport
    {
        /// <summary>
        /// The callback order.
        /// </summary>
        public int callbackOrder => 0;

        /// <summary>
        /// Callback before the build is started.
        /// </summary>
        /// <param name="report"> The build report containing information about the build, such as its target platform and output path.</param>
        public void OnPreprocessBuild(BuildReport report)
        {
            if (!LightSpaceBuildTools.LightSpaceLoaderPresentInSettingsForBuildTarget(report.summary.platformGroup))
            {
                return;
            }

            if (report.summary.platformGroup == BuildTargetGroup.Standalone)
            {
                if (PlayerSettings.GetGraphicsAPIs(EditorUserBuildSettings.activeBuildTarget)[0] != GraphicsDeviceType.Direct3D11)
                {
                    throw new BuildFailedException("D3D11 is currently the only graphics API compatible with the LightSpace XR Plugin on desktop platforms. Please change the Graphics API setting in Player Settings.");
                }
            }

            var buildOutputPath = Path.GetDirectoryName(report.summary.outputPath);
            if (buildOutputPath == null)
            {
                return;
            }

            var destDirName = GetPluginRelativeDependencyPath(buildOutputPath);
            if (destDirName == null)
            {
                Debug.LogWarning($"LightSpaceXR: Couldn't include dependencies. Destination directory not found.");
                return;
            }

            var settings = LightSpaceBuildTools.GetSettings();
            if (settings != null)
            {
                settings.CompositorHostPath = Path.Combine(destDirName, "CompositorHost\\LightspaceCompositorHost.exe");
                settings.TrackingServiceHostPath = Path.Combine(destDirName, "TrackingServiceHost\\LightspaceTrackingServiceHost.exe");
                settings.TrackingServicePluginDir = Path.Combine(destDirName, "TrackingServiceHost\\TrackingPlugins\\");
            }
            else
            {
                Debug.LogWarning("LightSpaceXR: Couldn't set default Host paths.");
            }
        }

        /// <summary>
        /// Callback after the build is complete.
        /// </summary>
        /// <param name="report">A BuildReport containing information about the build, such as the target platform and output path.</param>
        public void OnPostprocessBuild(BuildReport report)
        {
            var buildOutputPath = Path.GetDirectoryName(report.summary.outputPath);
            if (buildOutputPath == null)
            {
                return;
            }

            var compositorHostPath = Directory.GetDirectories("Packages/com.lightspace.xr/Runtime/windows", "CompositorHost", SearchOption.AllDirectories).FirstOrDefault();
            if (compositorHostPath == null)
            {
                Debug.LogWarning($"LightSpaceXR: Couldn't include dependencies. CompositorHost not found.");
                return;
            }

            var trackingServiceHostPath = Directory.GetDirectories("Packages/com.lightspace.xr/Runtime/windows", "TrackingServiceHost", SearchOption.AllDirectories).FirstOrDefault();
            if (trackingServiceHostPath == null)
            {
                Debug.LogWarning($"LightSpaceXR: Couldn't include dependencies. TrackingServiceHost not found.");
                return;
            }

            var destDirName = GetPluginDependencyPath(buildOutputPath);
            if (destDirName == null)
            {
                Debug.LogWarning($"LightSpaceXR: Couldn't include dependencies. Destination directory not found.");
                return;
            }

            var compositorHostDestination = Path.Combine(destDirName, "CompositorHost");
            DirectoryCopy(compositorHostPath, compositorHostDestination);

            var trackingServiceHostDestination = Path.Combine(destDirName, "TrackingServiceHost");
            DirectoryCopy(trackingServiceHostPath, trackingServiceHostDestination);
        }

        /// <summary>
        /// Gets the absolute path for LightSpace XR Plugin dependencies in Standalone build.
        /// </summary>
        /// <param name="buildOutputPath">The build output path.</param>
        /// <returns>Returns path to LightSpace XR Plugin dependencies folder.</returns>
        [CanBeNull]
        private static string GetPluginDependencyPath(string buildOutputPath)
        {
            var pluginDirectory = Directory.GetDirectories(buildOutputPath, "LightSpaceXR", SearchOption.AllDirectories).FirstOrDefault();

            if (pluginDirectory == null)
            {
                Debug.LogWarning($"LightSpaceXR: Couldn't include dependencies. Subsystem not found.");
                return null;
            }

            return pluginDirectory;
        }

        /// <summary>
        /// Gets the relative path for LightSpace XR Plugin dependencies in Standalone build.
        /// </summary>
        /// <param name="buildOutputPath">The build output path.</param>
        /// <returns>Returns path to LightSpace XR Plugin dependencies folder.</returns>
        private static string GetPluginRelativeDependencyPath(string buildOutputPath)
        {
            var pluginDirectory = GetPluginDependencyPath(buildOutputPath);
            return GetRelativePath(buildOutputPath, pluginDirectory);
        }

        /// <summary>
        /// Gets relative path from full and root paths.
        /// </summary>
        /// <param name="rootPath">A root path to subtract.</param>
        /// <param name="fullPath">A path to get relative path from.</param>
        /// <returns>A relative path if available, null otherwise.</returns>
        public static string GetRelativePath(string rootPath, string fullPath)
        {
            if (!fullPath.StartsWith(rootPath))
            {
                return null;
            }

            return "." + fullPath.Substring(rootPath.Length);
        }

        /// <summary>
        /// Copies directory content to a new location.
        /// </summary>
        /// <param name="sourceDirName">The source directory path.</param>
        /// <param name="destDirName">The destination directory path.</param>
        private void DirectoryCopy(string sourceDirName, string destDirName)
        {
            // Get the subdirectories for the specified directory.
            var directoryInfo = new DirectoryInfo(sourceDirName);

            // If the destination directory doesn't exist, create it.   
            Directory.CreateDirectory(destDirName);

            // Get the files in the directory and copy them to the new location.
            var allFiles = directoryInfo.GetFiles();
            foreach (var file in allFiles)
            {
                var tempPath = Path.Combine(destDirName, file.Name);
                file.CopyTo(tempPath, true);
            }

            // If there are any subdirectories copy them as well. 
            var subDirectoryInfos = directoryInfo.GetDirectories();
            foreach (var directory in subDirectoryInfos)
            {
                var tempPath = Path.Combine(destDirName, directory.Name);
                DirectoryCopy(directory.FullName, tempPath);
            }
        }
    }
}