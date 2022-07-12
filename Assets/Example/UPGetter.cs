using UnityEngine;
using UniversalProfileSDK.Avatars;

namespace AvatarSDKExample
{
    /// <summary>
    /// Loads Universal Profiles
    /// </summary>
    public class UPGetter : MonoBehaviour
    {
        public void GetProfile(string address, AvatarSDKDelegates.GetProfileSuccess onSuccess, AvatarSDKDelegates.GetProfileFailed onFailed)
        {
            StartCoroutine(AvatarSDK.GetProfileRemote(address, onSuccess, onFailed));
        }

        public void GetProfileLocal(string filePath, AvatarSDKDelegates.GetProfileSuccess onSuccess, AvatarSDKDelegates.GetProfileFailed onFailed)
        {
            StartCoroutine(AvatarSDK.GetProfileLocal(filePath, onSuccess, onFailed));
        }
    }
}