// -----------------------------------------------------------------------
//  <copyright file="LightSpaceSettingsEditor.cs" company="LightSpace">
//    Copyright (c) LightSpace. All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Unity.XR.LightSpace.Editor
{
    using System;
    using System.IO;
    using System.Linq;
    using UnityEditor;
    using UnityEngine;

    /// <summary>
    /// Custom Inspector for <see cref="LightSpaceSettings"/>.
    /// </summary>
    [CustomEditor(typeof(LightSpaceSettings))]
    public class LightSpaceSettingsEditor : Editor
    {
        /// <summary>
        /// The stereo rendering mode property path.
        /// </summary>
        private const string StereoRenderingModePropertyPath = "CurrentRenderingMode";

        /// <summary>
        /// The mirror view mode property path.
        /// </summary>
        private const string MirrorViewModePropertyPath = "MirrorView";

        /// <summary>
        /// The tracking mode property path.
        /// </summary>
        private const string TrackingModePropertyPath = "CurrentTrackingMode";

        /// <summary>
        /// The user set compositor host property path.
        /// </summary>
        private const string CompositorHostPropertyPath = "UserSetCompositorHostPath";

        /// <summary>
        /// The override compositor host property path.
        /// </summary>
        private const string OverrideCompositorHostPropertyPath = "OverrideCompositorHostPath";

        /// <summary>
        /// The user set tracking service host property path.
        /// </summary>
        private const string TrackingServiceHostPropertyPath = "UserSetTrackingServiceHostPath";

        /// <summary>
        /// The override tracking service host property path.
        /// </summary>
        private const string OverrideTrackingServiceHostPropertyPath = "OverrideTrackingServiceHostPath";

        /// <summary>
        /// The tracking service plugin editor directory property path.
        /// </summary>
        private const string TrackingServiceDirEditorPropertyPath = "TrackingServicePluginDirEditor";

        /// <summary>
        /// The current tracking plugin name property path.
        /// </summary>
        private const string TrackingPluginNamePropertyPath = "TrackingPluginName";

        /// <summary>
        /// The user set tracking service plugin property path.
        /// </summary>
        private const string TrackingServicePluginPath = "UserSetTrackingServicePluginPath";

        /// <summary>
        /// The override tracking service plugin property path.
        /// </summary>
        private const string OverrideTrackingServicePluginPath = "OverrideTrackingServicePluginPath";

        /// <summary>
        /// The stereo rendering mode label.
        /// </summary>  
        private static readonly GUIContent StereoRenderingModeLabel = EditorGUIUtility.TrTextContent("Stereo Rendering Mode");

        /// <summary>
        /// The mirror view mode label.
        /// </summary>
        private static readonly GUIContent MirrorViewModeLabel = EditorGUIUtility.TrTextContent("Mirror View Mode");

        /// <summary>
        /// The tracking mode label.
        /// </summary>
        private static readonly GUIContent TrackingModeLabel = EditorGUIUtility.TrTextContent("Tracking Mode");

        /// <summary>
        /// The path label.
        /// </summary>
        private static readonly GUIContent PathLabel = EditorGUIUtility.TrTextContent("path:");

        /// <summary>
        /// The path button label.
        /// </summary>
        private static readonly GUIContent PathButtonLabel = EditorGUIUtility.TrTextContent("...");

        /// <summary>
        /// The override compositor host path label.
        /// </summary>
        private static readonly GUIContent OverrideCompositorHostLabel = EditorGUIUtility.TrTextContent("Use custom LightSpace Compositor path");

        /// <summary>
        /// The override tracking service host path label.
        /// </summary>
        private static readonly GUIContent OverrideTrackingServiceHostLabel = EditorGUIUtility.TrTextContent("Use custom LightSpace Tracking Service path");

        /// <summary>
        /// The override tracking service plugin path label.
        /// </summary>
        private static readonly GUIContent OverrideTrackingServicePluginLabel = EditorGUIUtility.TrTextContent("Use custom LightSpace Tracking Plugin path");

        /// <summary>
        /// The stereo rendering mode property.
        /// </summary>
        private SerializedProperty stereoRenderingModeProperty;

        /// <summary>
        /// The mirror view mode property.
        /// </summary>
        private SerializedProperty mirrorViewModeProperty;

        /// <summary>
        /// The tracking mode property.
        /// </summary>
        private SerializedProperty trackingModeProperty;

        /// <summary>
        /// The user set compositor path property.
        /// </summary>
        private SerializedProperty compositorHostProperty;

        /// <summary>
        /// The tracking service host path property.
        /// </summary>
        private SerializedProperty trackingServiceHostProperty;

        /// <summary>
        /// The tracking service plugin name property.
        /// </summary>
        private SerializedProperty trackingPluginNameProperty;

        /// <summary>
        /// The tracking service plugin editor mode directory property.
        /// </summary>
        private SerializedProperty trackingPluginDirEditorProperty;

        /// <summary>
        /// The user set tracking plugin path property.
        /// </summary>
        private SerializedProperty trackingPluginProperty;

        /// <summary>
        /// The override compositor host property.
        /// </summary>
        private SerializedProperty overrideCompositorHostProperty;

        /// <summary>
        /// The override tracking service host property.
        /// </summary>
        private SerializedProperty overrideTrackingServiceHostProperty;

        /// <summary>
        /// The override tracking service plugin property.
        /// </summary>
        private SerializedProperty overrideTrackingServicePluginProperty;

        /// <summary>
        /// XR Settings for Windows build platform.
        /// </summary>
        private GUIContent windowsTab;

        /// <summary>
        /// This function is called when the object becomes enabled and active.
        /// </summary>
        public void OnEnable()
        {
            this.windowsTab = new GUIContent(string.Empty, EditorGUIUtility.IconContent("BuildSettings.Standalone.Small").image);
        }

        /// <summary>
        /// Adds custom GUI for the inspector.
        /// </summary>
        public override void OnInspectorGUI()
        {
            if (this.serializedObject.targetObject == null)
            {
                return;
            }

            if (this.stereoRenderingModeProperty == null)
            {
                this.stereoRenderingModeProperty = this.serializedObject.FindProperty(StereoRenderingModePropertyPath);
            }

            if (this.mirrorViewModeProperty == null)
            {
                this.mirrorViewModeProperty = this.serializedObject.FindProperty(MirrorViewModePropertyPath);
            }

            if (this.trackingModeProperty == null)
            {
                this.trackingModeProperty = this.serializedObject.FindProperty(TrackingModePropertyPath);
            }

            if (this.trackingPluginNameProperty == null)
            {
                this.trackingPluginNameProperty = this.serializedObject.FindProperty(TrackingPluginNamePropertyPath);
            }

            if (this.trackingPluginDirEditorProperty == null)
            {
                this.trackingPluginDirEditorProperty = this.serializedObject.FindProperty(TrackingServiceDirEditorPropertyPath);
            }

            if (this.compositorHostProperty == null)
            {
                this.compositorHostProperty = this.serializedObject.FindProperty(CompositorHostPropertyPath);
            }

            if (this.overrideCompositorHostProperty == null)
            {
                this.overrideCompositorHostProperty = this.serializedObject.FindProperty(OverrideCompositorHostPropertyPath);
            }

            if (this.trackingServiceHostProperty == null)
            {
                this.trackingServiceHostProperty = this.serializedObject.FindProperty(TrackingServiceHostPropertyPath);
            }

            if (this.overrideTrackingServiceHostProperty == null)
            {
                this.overrideTrackingServiceHostProperty = this.serializedObject.FindProperty(OverrideTrackingServiceHostPropertyPath);
            }

            if (this.overrideTrackingServicePluginProperty == null)
            {
                this.overrideTrackingServicePluginProperty = this.serializedObject.FindProperty(OverrideTrackingServicePluginPath);
            }

            if (this.trackingPluginProperty == null)
            {
                this.trackingPluginProperty = this.serializedObject.FindProperty(TrackingServicePluginPath);
            }

            this.serializedObject.Update();

            EditorGUILayout.Space();
            EditorGUILayout.BeginVertical(GUILayout.ExpandWidth(true));

            GUILayout.Toolbar(0, new GUIContent[] { this.windowsTab }, EditorStyles.toolbarButton);
            EditorGUILayout.Space();

            if (EditorApplication.isPlayingOrWillChangePlaymode)
            {
                EditorGUILayout.HelpBox("Settings cannot be changed when the editor is in play mode.", MessageType.Info);
                EditorGUILayout.Space();
            }

            EditorGUI.BeginDisabledGroup(EditorApplication.isPlayingOrWillChangePlaymode);
            EditorGUILayout.PropertyField(this.stereoRenderingModeProperty, StereoRenderingModeLabel);
            EditorGUILayout.PropertyField(this.mirrorViewModeProperty, MirrorViewModeLabel);
            EditorGUILayout.PropertyField(this.trackingModeProperty, TrackingModeLabel);

            if (this.trackingModeProperty.intValue > 0)
            {
                var availableTrackingPlugins = this.GetAvailablePlugins();
                if (availableTrackingPlugins.Length > 0)
                {
                    var selectedIndex = Array.FindIndex(availableTrackingPlugins, x => x == this.trackingPluginNameProperty.stringValue);
                    selectedIndex = EditorGUILayout.Popup("Tracking Plugin", selectedIndex, availableTrackingPlugins);
                    this.trackingPluginNameProperty.stringValue = selectedIndex >= 0 ? availableTrackingPlugins[selectedIndex] : string.Empty;
                }
                else
                {
                    EditorGUILayout.HelpBox("No available tracking plugins found!", MessageType.Error);
                }
            }

            EditorGUILayout.Space();
            EditorGUIUtility.labelWidth = 235.0f;
            EditorGUILayout.PropertyField(this.overrideCompositorHostProperty, OverrideCompositorHostLabel);
            EditorGUIUtility.labelWidth = 0;

            if (overrideCompositorHostProperty.boolValue)
            {
                EditorGUILayout.BeginHorizontal(GUILayout.ExpandWidth(true));
                
                EditorGUIUtility.labelWidth = 30.0f;
                EditorGUILayout.PropertyField(this.compositorHostProperty, PathLabel);
                EditorGUIUtility.labelWidth = 0;
                
                if (GUILayout.Button(PathButtonLabel, GUILayout.Width(19f)))
                {
                    this.OpenCompositorHostFileDialog();
                }
                
                EditorGUILayout.EndHorizontal();

                var path = this.compositorHostProperty.stringValue;
                if (path == string.Empty || !File.Exists(path))
                {
                    EditorGUILayout.HelpBox("Please set valid path to LightSpace Compositor Host!", MessageType.Error);
                }
            }

            EditorGUIUtility.labelWidth = 270.0f;
            EditorGUILayout.PropertyField(this.overrideTrackingServiceHostProperty, OverrideTrackingServiceHostLabel);
            EditorGUIUtility.labelWidth = 0;

            if (overrideTrackingServiceHostProperty.boolValue)
            {
                EditorGUILayout.BeginHorizontal(GUILayout.ExpandWidth(true));

                EditorGUIUtility.labelWidth = 30.0f;
                EditorGUILayout.PropertyField(this.trackingServiceHostProperty, PathLabel);
                EditorGUIUtility.labelWidth = 0;

                if (GUILayout.Button(PathButtonLabel, GUILayout.Width(19f)))
                {
                    this.OpenTrackingServiceHostFileDialog();
                }

                EditorGUILayout.EndHorizontal();

                var path = this.trackingServiceHostProperty.stringValue;
                if (path == string.Empty || !File.Exists(path))
                {
                    EditorGUILayout.HelpBox("Please set valid path to LightSpace Tracking Service Host!", MessageType.Error);
                }
            }

            EditorGUIUtility.labelWidth = 260.0f;
            EditorGUILayout.PropertyField(this.overrideTrackingServicePluginProperty, OverrideTrackingServicePluginLabel);
            EditorGUIUtility.labelWidth = 0;

            if (overrideTrackingServicePluginProperty.boolValue)
            {
                EditorGUILayout.BeginHorizontal(GUILayout.ExpandWidth(true));

                EditorGUIUtility.labelWidth = 30.0f;
                EditorGUILayout.PropertyField(this.trackingPluginProperty, PathLabel);
                EditorGUIUtility.labelWidth = 0;

                if (GUILayout.Button(PathButtonLabel, GUILayout.Width(19f)))
                {
                    this.OpenTrackingServicePluginFolderDialog();
                }

                EditorGUILayout.EndHorizontal();

                var path = this.trackingPluginProperty.stringValue;
                if (string.IsNullOrEmpty(path) || !Directory.Exists(path))
                {
                    EditorGUILayout.HelpBox("Please set valid path to LightSpace Tracking Service Plugin folder!", MessageType.Error);
                }
            }

            EditorGUI.EndDisabledGroup();
            
            EditorGUILayout.EndVertical();

            this.serializedObject.ApplyModifiedProperties();
        }

        /// <summary>
        /// Opens File Dialog window for compositor host.
        /// </summary>
        private void OpenCompositorHostFileDialog()
        {
            var path = EditorUtility.OpenFilePanel("Select LightSpace Compositor Host", "", "exe");
            if (path.Length != 0)
            {
                this.compositorHostProperty.stringValue = path;
            }
        }

        /// <summary>
        /// Opens File Dialog window for tracking service host.
        /// </summary>
        private void OpenTrackingServiceHostFileDialog()
        {
            var path = EditorUtility.OpenFilePanel("Select LightSpace Tracking Service Host", "", "exe");
            if (path.Length != 0)
            {
                this.trackingServiceHostProperty.stringValue = path;
            }
        }

        /// <summary>
        /// Opens Folder Dialog window for tracking service plugin.
        /// </summary>
        private void OpenTrackingServicePluginFolderDialog()
        {
            var path = EditorUtility.OpenFolderPanel("Select LightSpace Tracking Plugin", "", "");
            if (path.Length != 0)
            {
                this.trackingPluginProperty.stringValue = path;
            }
        }

        /// <summary>
        /// Gets the available tracking plugins.
        /// </summary>
        /// <returns>A collection of tracking plugins.</returns>
        public string[] GetAvailablePlugins()
        {
            var dir = this.trackingPluginDirEditorProperty.stringValue;

            if (!string.IsNullOrEmpty(dir))
            {
                var directory = new DirectoryInfo(dir);
                return directory
                    .GetDirectories("*Plugin", SearchOption.TopDirectoryOnly)
                    .Select(subDirectory => subDirectory.Name)
                    .ToArray();
            }

            return Array.Empty<string>();
        }
    }
}