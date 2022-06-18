// -----------------------------------------------------------------------
//  <copyright file="TrackedObjectsManager.cs" company="LightSpace">
//    Copyright (c) LightSpace. All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------

namespace LightSpaceXR
{
    using System;
    using System.Collections.Generic;
    using LightspaceTrackingServiceClient;
    using LightspaceTrackingServiceContracts;
    using UnityEngine;

    /// <summary>
    /// A class used for managing tracking connection and provide tracked object poses.
    /// </summary>
    public class TrackedObjectsManager : MonoBehaviour
    {
        /// <summary>
        /// The tracking service client instance.
        /// </summary>
        private ITrackingService trackingService;

        /// <summary>
        /// Gets a value indicating whether the client is initialized or not.
        /// </summary>
        public bool ClientInitialized => this.trackingService != null;

        /// <summary>
        /// Queries the tracked objects from the service.
        /// </summary>
        /// <returns>An IEnumerable of tracking objects if available. Null, otherwise.</returns>
        public IEnumerable<TrackedObject> GetTrackedObjects()
        {
            try
            {
                if (this.trackingService != null && this.trackingService.Streaming)
                {
                    return this.trackingService.GetTrackedObjects(0f);
                }
            }
            catch (Exception e)
            {
                Debug.Log($"LightSpaceXR: Caught {e.GetType().Name} while requesting tracked objects '{e.Message}'{Environment.NewLine}{e.StackTrace}");
            }

            return default;
        }

        /// <summary>
        /// Update is called once per frame.
        /// </summary>
        private void Start()
        {
            if (this.trackingService != null)
            {
                return;
            }

            if (!LightspaceTrackingServiceClient.TryConnectToExistingHostProcess(out var client))
            {
                Debug.Log("Could not initialize TrackedObjectsManager, service not running");
                return;
            }

            this.trackingService = client;
        }

        /// <summary>
        /// Called when a Scene or game ends.
        /// </summary>
        private void OnDestroy()
        {
            try
            {
                if (this.trackingService is LightspaceTrackingServiceClient trackingServiceClient)
                {
                    trackingServiceClient.Dispose();
                }
            }
            catch (Exception e)
            {
                Debug.Log($"LightSpaceXR: Caught {e.GetType().Name} while disposing tracking client '{e.Message}'{Environment.NewLine}{e.StackTrace}");
            }

            this.trackingService = null;
        }
    }
}