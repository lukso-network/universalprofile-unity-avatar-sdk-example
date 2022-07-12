using UnityEngine;
using UniversalProfileSDK.Avatars;

namespace AvatarSDKExample
{
    /// <summary>
    /// Loads Universal Profiles
    /// </summary>
    public class UPGetter : MonoBehaviour
    {
        /// <summary>
        /// Gets remote profile from the blockchain
        /// </summary>
        /// <param name="address">Profile address</param>
        /// <param name="onSuccess">Delegate to run on successful load</param>
        /// <param name="onFailed">Delegate to run on failed load</param>
        public void GetProfile(string address, AvatarSDKDelegates.GetProfileSuccess onSuccess, AvatarSDKDelegates.GetProfileFailed onFailed)
        {
            StartCoroutine(AvatarSDK.GetProfileRemote(address, onSuccess, onFailed));
        }

        /// <summary>
        /// Gets profile from a local lsp3 json
        /// </summary>
        /// <param name="filePath">Json filepath</param>
        /// <param name="onSuccess">Delegate to run on successful load</param>
        /// <param name="onFailed">Delegate to run on failed load</param>
        public void GetProfileLocal(string filePath, AvatarSDKDelegates.GetProfileSuccess onSuccess, AvatarSDKDelegates.GetProfileFailed onFailed)
        {
            StartCoroutine(AvatarSDK.GetProfileLocal(filePath, onSuccess, onFailed));
        }
    }
}