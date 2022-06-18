// -----------------------------------------------------------------------
//  <copyright file="LightSpaceProjectSettings.cs" company="LightSpace">
//    Copyright (c) LightSpace. All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Unity.XR.LightSpace.Editor
{
    using UnityEditor;
    using UnityEngine;

    /// <summary>
    /// Class for managing LightSpace XR Plugin preferred project settings.
    /// </summary>
    [InitializeOnLoad]
    public class LightSpaceProjectSettings
    {
        /// <summary>
        /// Session key for tracking whether we have shown settings prompt and can ignore it for current Editor session.
        /// </summary>
        private const string SessionKey = "_LightSpaceXR_Editor_IgnoreSettingsPrompts";

        /// <summary>
        /// Initializes static members of the <see cref="LightSpaceProjectSettings"/> class.
        /// </summary>
        static LightSpaceProjectSettings()
        {
            // Subscribe to editor application update which will call us once the editor is initialized and running
            EditorApplication.update += OnInit;
        }

        /// <summary>
        /// Gets or sets a value indicating whether to ignore checking project settings for the current Unity session
        /// </summary>
        public static bool IgnoreProjectConfigForSession
        {
            get => SessionState.GetBool(SessionKey, false);
            set => SessionState.SetBool(SessionKey, value);
        }

        /// <summary>
        /// Apples the preferred project configurations.
        /// </summary>
        public static void ApplyPreferredConfiguration()
        {
            // Disable Anti-Aliasing
            QualitySettings.antiAliasing = 0;
        }

        /// <summary>
        /// Function is called on Editor initialized.
        /// </summary>
        private static void OnInit()
        {
            // We only want to execute once to initialize, unsubscribe from update event
            EditorApplication.update -= OnInit;

            ShowProjectSettingsPrompt();
        }

        /// <summary>
        /// Shows project settings prompt window.
        /// </summary>
        private static void ShowProjectSettingsPrompt()
        {
            if (!EditorApplication.isPlayingOrWillChangePlaymode
                && !IgnoreProjectConfigForSession
                && !ProjectPreferences.DisableSettingsPrompt
                && !IsProjectConfigured())
            {
                LightSpaceProjectSettingsPrompt.ShowWindow();
            }
        }

        /// <summary>
        /// Checks whether project is configured according to LightSpaceXR recommendations.
        /// </summary>
        /// <returns><c>true</c> if project is configured according to LightSpaceXR recommendations, <c>false</c> otherwise</returns>
        private static bool IsProjectConfigured()
        {
            return QualitySettings.antiAliasing == 0;
        }
    }
}