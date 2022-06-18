// -----------------------------------------------------------------------
//  <copyright file="LightSpaceProjectSettingsPrompt.cs" company="LightSpace">
//    Copyright (c) LightSpace. All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Unity.XR.LightSpace.Editor
{
    using UnityEditor;
    using UnityEditor.Compilation;

    using UnityEngine;

    using Unity.XR.LightSpace.Editor.Utilities;

    /// <summary>
    /// The popup window for applying LightSpace XR Plugin preferred project settings.
    /// </summary>
    public class LightSpaceProjectSettingsPrompt : EditorWindow
    {
        /// <summary>
        /// The apply button content.
        /// </summary>
        private readonly GUIContent applyButtonContent = new GUIContent("Apply", "Apply configurations to this Unity Project");

        /// <summary>
        /// The later button content.
        /// </summary>
        private readonly GUIContent laterButtonContent = new GUIContent("Later", "Do not show this pop-up notification until next session");

        /// <summary>
        /// The ignore button content.
        /// </summary>
        private readonly GUIContent ignoreButtonContent = new GUIContent("Ignore", "Access this window later at LightSpaceXR > LightSpaceXR Project Configuration");

        /// <summary>
        /// Gets the instance.
        /// </summary>
        public static LightSpaceProjectSettingsPrompt Instance { get; private set; }

        /// <summary>
        /// Value indicating whether the instance is open.
        /// </summary>
        public static bool IsOpen => Instance != null;

        /// <summary>
        /// Shows the Editor window.
        /// </summary>
        [MenuItem("LightSpaceXR/Unity Project Configuration")]
        public static void ShowWindow()
        {
            // There should be only one configuration window open as a "pop-up". If already open, then just force focus on our instance
            if (IsOpen)
            {
                Instance.Focus();
            }
            else
            {
                var window = CreateInstance<LightSpaceProjectSettingsPrompt>();
                window.titleContent = new GUIContent("LightSpaceXR Project Configuration", EditorGUIUtility.IconContent("_Popup").image);
                window.position = new Rect(Screen.width / 2.0f, Screen.height / 2.0f, 600, 400);
                window.ShowUtility();
            }
        }

        /// <summary>
        /// This function is called when the object becomes enabled and active.
        /// </summary>
        private void OnEnable()
        {
            Instance = this;

            CompilationPipeline.assemblyCompilationStarted += this.CompilationPipelineAssemblyCompilationStarted;
        }

        /// <summary>
        /// Event that is invoked on the main thread when the assembly build starts.
        /// </summary>
        /// <param name="assembly">The output assembly path.</param>
        private void CompilationPipelineAssemblyCompilationStarted(string assembly)
        {
            // There should be only one pop-up window which is generally tracked by IsOpen
            // However, when recompiling, Unity will call OnDestroy for this window but not actually destroy the editor window
            // This ensure we have a clean close on recompile when this EditorWindow was open beforehand
            this.Close();
        }

        /// <summary>
        /// OnGUI is called for rendering and handling GUI events.
        /// </summary>
        private void OnGUI()
        {
            LightSpaceInspectorUtilities.RenderLogo();
            EditorGUILayout.LabelField("LightSpace XR Plugin would like to auto-apply useful settings to this Unity project");
            GUILayout.Space(10f);
            GUILayout.BeginVertical();
            EditorGUILayout.HelpBox("Disables QualitySettings.antiAliasing", MessageType.Info, true);
            GUILayout.EndVertical();
            GUILayout.Space(10f);
            EditorGUILayout.LabelField("Apply Default Settings?", EditorStyles.boldLabel);
            GUILayout.Space(15f);

            using (new EditorGUILayout.HorizontalScope())
            {
                if (GUILayout.Button(this.applyButtonContent))
                {
                    LightSpaceProjectSettings.ApplyPreferredConfiguration();
                    this.Close();
                }

                if (GUILayout.Button(this.laterButtonContent))
                {
                    LightSpaceProjectSettings.IgnoreProjectConfigForSession = true;
                    this.Close();
                }

                if (GUILayout.Button(this.ignoreButtonContent))
                {
                    ProjectPreferences.DisableSettingsPrompt = true;
                    this.Close();
                }
            }
        }
    }
}