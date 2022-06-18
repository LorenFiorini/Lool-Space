// -----------------------------------------------------------------------
//  <copyright file="StereoViewManager.cs" company="LightSpace">
//    Copyright (c) LightSpace. All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace LightSpaceXR
{
    using System.Runtime.InteropServices;
    using UnityEngine;

    /// <summary>
    /// MonoBehaviour for passing and reading stereo parameters to and from LightSpace Compositor.
    /// </summary>
    public class StereoViewManager : MonoBehaviour
    {
        private float defaultInterPupillaryDistanceMeters;
        private float interPupillaryDistanceMeters;

        [DllImport("LightSpaceXR", CharSet = CharSet.Auto)]
        static extern void SetStereoParams(float ipd);

        [DllImport("LightSpaceXR", CharSet = CharSet.Auto)]
        static extern float GetStereoParams();

        /// <summary>
        /// Awake is called when the script instance is being loaded.
        /// </summary>
        private void Awake()
        {
            defaultInterPupillaryDistanceMeters = interPupillaryDistanceMeters = GetStereoParams();
            Debug.Log($"LightSpaceXR: InterPupillaryDistance set to {this.interPupillaryDistanceMeters:0.0000} meters");
        }

        /// <summary>
        /// Update is called every frame, if the MonoBehaviour is enabled.
        /// </summary>
        void Update()
        {
            if (Input.GetKey(KeyCode.P))
            {
                if (Input.GetKeyDown(KeyCode.KeypadMinus))
                {
                    this.interPupillaryDistanceMeters -= 0.0001f;
                    SetStereoParams(interPupillaryDistanceMeters);
                    Debug.Log($"LightSpaceXR: InterPupillaryDistance set to {this.interPupillaryDistanceMeters:0.0000} meters");
                }

                if (Input.GetKeyDown(KeyCode.KeypadPlus))
                {
                    this.interPupillaryDistanceMeters += 0.0001f;
                    SetStereoParams(interPupillaryDistanceMeters);
                    Debug.Log($"LightSpaceXR: InterPupillaryDistance set to {this.interPupillaryDistanceMeters:0.0000} meters");
                }

                if (Input.GetKeyDown(KeyCode.R))
                {
                    this.interPupillaryDistanceMeters = defaultInterPupillaryDistanceMeters;
                    SetStereoParams(interPupillaryDistanceMeters);
                    Debug.Log($"LightSpaceXR: InterPupillaryDistance set to {this.interPupillaryDistanceMeters:0.0000} meters");
                }
            }
        }
    }
}