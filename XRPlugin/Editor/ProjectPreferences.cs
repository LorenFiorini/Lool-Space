// -----------------------------------------------------------------------
//  <copyright file="ProjectPreferences.cs" company="LightSpace">
//    Copyright (c) LightSpace. All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace Unity.XR.LightSpace.Editor
{
    using System.IO;
    
    using UnityEditor;
    using UnityEngine;

    /// <summary>
    /// LightSpaceXR project preferences.
    /// </summary>
    public class ProjectPreferences : ScriptableObject
    {
        /// <summary>
        /// The default generated asset file name.
        /// </summary>
        private const string DefaultFileName = "ProjectPreferences.asset";

        /// <summary>
        /// The default generated asset folder path.
        /// </summary>
        private const string DefaultFilePath = "LightSpaceXR";

        /// <summary>
        /// Backing field for Instance.
        /// </summary>
        private static ProjectPreferences instance;
        
        /// <summary>
        /// Backing field for DisableSettingsPrompt.
        /// </summary>
        [SerializeField] private bool disableSettingsPrompt;
        
        /// <summary>
        /// Gets or sets a value indicating whether to show project settings prompt when the project isn't configured according to LightSpaceXR recommendations?
        /// </summary>
        public static bool DisableSettingsPrompt
        {
            get => Instance.disableSettingsPrompt;
            set
            {
                Instance.disableSettingsPrompt = value;
                EditorUtility.SetDirty(Instance);
                AssetDatabase.SaveAssets();
            }
        }

        /// <summary>
        /// Gets or creates new a LightSpaceXR project preferences asset file.
        /// </summary>
        private static ProjectPreferences Instance
        {
            get
            {
                var folderPath = $"Assets/{DefaultFilePath}";
                if (!AssetDatabase.IsValidFolder(folderPath))
                {
                    var folderGuid = AssetDatabase.CreateFolder("Assets", DefaultFilePath);
                    folderPath = AssetDatabase.GUIDToAssetPath(folderGuid);
                }

                var filePath = Path.Combine(folderPath, DefaultFileName);

                // Sometimes Unity has weird bug where asset file exists but Unity will not load it resulting in instance = null. 
                // Force refresh of asset database before we try to access our preferences file
                AssetDatabase.Refresh();
                instance = (ProjectPreferences)AssetDatabase.LoadAssetAtPath(filePath, typeof(ProjectPreferences));

                if (instance == null)
                {
                    instance = ScriptableObject.CreateInstance<ProjectPreferences>();
                    AssetDatabase.CreateAsset(instance, filePath);
                    AssetDatabase.SaveAssets();

                    Debug.Log("Generated new LightSpaceXR project preferences asset at:" + folderPath);
                }

                return instance;
            }
        }
    }
}