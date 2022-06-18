// -----------------------------------------------------------------------
//  <copyright file="LightSpaceInspectorUtilities.cs" company="LightSpace">
//    Copyright (c) LightSpace. All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Unity.XR.LightSpace.Editor.Utilities
{
    using UnityEngine;
    using UnityEditor;

    /// <summary>
    /// Class for LightSpace Unity Inspector utilities.
    /// </summary>
    public static class LightSpaceInspectorUtilities
    {
        /// <summary>
        /// Light themed company logo asset GUID.
        /// </summary>
        private const string LogoLightThemeGuid = "93457814ec800d64b9f8476a0a3d9a27";

        /// <summary>
        /// Dark themed company logo asset GUID.
        /// </summary>
        private const string LogoDarkThemeGuid = "6c932b863bbd97e479907f25267f6f90";

        /// <summary>
        /// Light themed company logo texture.
        /// </summary>
        public static readonly Texture2D LogoLightTheme = AssetDatabase.LoadAssetAtPath<Texture2D>(AssetDatabase.GUIDToAssetPath(LogoLightThemeGuid));

        /// <summary>
        /// Dark themed company logo texture.
        /// </summary>
        public static readonly Texture2D LogoDarkTheme = AssetDatabase.LoadAssetAtPath<Texture2D>(AssetDatabase.GUIDToAssetPath(LogoDarkThemeGuid));

        /// <summary>
        /// Renders company logo.
        /// </summary>
        public static void RenderLogo()
        {
            GUILayout.Space(10f);
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            GUILayout.Label(EditorGUIUtility.isProSkin ? LogoDarkTheme : LogoLightTheme, GUILayout.MaxHeight(100));
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
            GUILayout.Space(3f);
        }
    }
}