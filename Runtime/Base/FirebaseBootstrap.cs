using System;
using Cysharp.Threading.Tasks;
using Firebase;
using JetBrains.Annotations;
using UnityEngine;

namespace FirebaseServices.Runtime.Base
{
    /// <summary>
    /// Bootstrap class for Firebase.
    /// </summary>
    [PublicAPI]
    public static class FirebaseBootstrap
    {
        /// <summary>
        /// Initializes Firebase asynchronously.
        /// </summary>
        public static async UniTask InitAsync()
        {
            try
            {
                await FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(static task =>
                {
                    var dependencyStatus = task.Result;
                    if (dependencyStatus == DependencyStatus.Available)
                    {
                        Debug.Log("[FirebaseBootstrap::InitAsync] Firebase Analytics initialized successfully");
                        return;
                    }

                    Debug.LogError("[FirebaseBootstrap::InitAsync] " +
                                   $"Could not resolve all Firebase dependencies: {dependencyStatus}");
                });
            }
            catch (Exception ex)
            {
                Debug.LogError("[FirebaseBootstrap::InitAsync] FireBase initialization failed with exception:" +
                               $" {ex.Message}");
                Debug.LogException(ex);
            }
        }
    }
}