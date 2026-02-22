using System;
using System.Threading;
using CustomUtils.Runtime.Extensions;
using Cysharp.Threading.Tasks;
using Firebase;
using Firebase.RemoteConfig;
using JetBrains.Annotations;
using UnityEngine;

namespace FirebaseServices.Runtime.Config
{
    [PublicAPI]
    public sealed class RemoteConfigService : IRemoteConfigService
    {
        private FirebaseRemoteConfig _remoteConfig;

        public async UniTask InitAsync(CancellationToken token)
        {
            var dependencyStatus = await FirebaseApp.CheckAndFixDependenciesAsync().AsUniTask(token);

            if (dependencyStatus is not DependencyStatus.Available)
            {
                Debug.LogError("[RemoteConfigManager::InitializeAsync] " +
                               $"Firebase dependencies not available: {dependencyStatus}");
                return;
            }

            _remoteConfig = FirebaseRemoteConfig.DefaultInstance;

            await FetchAndActivateAsync(token);
        }

        private async UniTask FetchAndActivateAsync(CancellationToken token)
        {
            try
            {
                await _remoteConfig.FetchAsync().AsUniTask(token);
                await _remoteConfig.ActivateAsync().AsUniTask(token);

                Debug.Log("[RemoteConfigManager::FetchAndActivateAsync] Config fetched and activated successfully");
            }
            catch (Exception ex)
            {
                Debug.LogError($"[RemoteConfigManager::FetchAndActivateAsync] Config fetch failed: {ex.Message}");
                Debug.LogException(ex);
            }
        }

        public bool TryGetString(string key, out string value)
        {
            value = _remoteConfig.GetValue(key).StringValue;
            return string.IsNullOrEmpty(value) is false;
        }
    }
}