using System.Threading;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;

namespace FirebaseServices.Runtime.Config
{
    /// <summary>
    /// Provides remote configuration management using Firebase Remote Config.
    /// </summary>
    [PublicAPI]
    public interface IRemoteConfigService
    {
        /// <summary>
        /// Initializes the remote config service and fetches the latest configuration.
        /// </summary>
        /// <param name="token">Cancellation token to cancel the initialization process.</param>
        /// <returns>A task representing the initialization operation.</returns>
        UniTask InitAsync(CancellationToken token);

        /// <summary>
        /// Attempts to retrieve a string value from the remote configuration.
        /// </summary>
        /// <param name="key">The configuration key to retrieve.</param>
        /// <param name="value">When this method returns,
        /// contains the string value if the key exists and has a valid value otherwise, null.</param>
        /// <returns>True if the key exists and has a valid string value; otherwise, false.</returns>
        bool TryGetString(string key, out string value);
    }
}