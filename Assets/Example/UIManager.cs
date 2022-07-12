using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UniversalProfileSDK;
using UniversalProfileSDK.Avatars;

namespace AvatarSDKExample
{
    /// <summary>
    /// Manages all the UI in our scene
    /// </summary>
    public class UIManager : MonoBehaviour
    {
        [Header("Load Profile Panel")] [SerializeField]
        InputField inputUPAddress;

        [SerializeField] Text labelUPStatus;
        [SerializeField] UIProgressBar loadProgressBar;

        [Header("Profile Info Panel")] [SerializeField]
        Text labelUPName;

        [SerializeField] Text labelUPDescription;

        [Header("Avatars Panel")] [SerializeField]
        Dropdown dropdownAvailableAvatars;

        [SerializeField] Toggle toggleFilterToCurrentPlatform;

        [Space] [SerializeField] Text labelAvatarHashFunction;
        [SerializeField] Text labelAvatarHash;
        [SerializeField] Text labelAvatarUrl;
        [SerializeField] Text labelAvatarFileType;

        [Header("Avatar Preview Panel")] [SerializeField]
        Transform avatarRoot;

        [Space] [SerializeField] float avatarRotationSpeed = 10;
        [SerializeField] RuntimeAnimatorController avatarAnimator;

        UPGetter upGetter;
        UniversalProfile loadedProfile;

        UPAvatar[] availableAvatars;
        UPAvatar[] availableAvatarsPlatformFiltered;

        UIAvatar loadedAvatar;

        List<Dropdown.OptionData> avatarDropdownOptions;
        List<Dropdown.OptionData> avatarDropdownOptionsPlatformFiltered;

        void Awake()
        {
            upGetter = GetComponent<UPGetter>();
            loadProgressBar.gameObject.SetActive(false);
        }

        public void LoadProfile()
        {
            string address = inputUPAddress.text;

            Debug.Log($"Loading profile {address}...");

            if(address.StartsWith("0x"))
            {
                labelUPStatus.text = "Loading remote profile...";
                upGetter.GetProfile(address, OnProfileLoaded, Debug.LogException);
            }
            else
            {
                labelUPStatus.text = "Loading local LSP3 JSON...";
                upGetter.GetProfileLocal(address, OnProfileLoaded, Debug.LogException);
            }
        }

        void OnProfileLoaded(UniversalProfile up)
        {
            if(loadProgressBar)
                loadProgressBar.gameObject.SetActive(false);

            labelUPStatus.text = $"Loaded universal profile '{up.Name}'";

            labelUPName.text = up.Name;
            labelUPDescription.text = up.Description;

            loadedProfile = up;

            availableAvatars = up.Avatars;
            availableAvatarsPlatformFiltered = up.Avatars.Where(av => av.UPAvatarIsForCurrentPlatform()).ToArray();

            avatarDropdownOptions = availableAvatars.Select(av => new Dropdown.OptionData(av.Hash)).ToList();
            avatarDropdownOptionsPlatformFiltered = availableAvatarsPlatformFiltered
                                                    .Select(av => new Dropdown.OptionData(av.Hash))
                                                    .ToList();
            OnFilterAvatarsToggleChanged();
            OnAvatarDropdownChanged();
        }

        public void LoadAvatar()
        {
            if(loadedProfile == null)
            {
                Debug.LogError("Load a profile before attempting to load an avatar.");
                return;
            }

            if(loadProgressBar)
            {
                loadProgressBar.gameObject.SetActive(true);
                loadProgressBar.ResetValue();
            }

            UPAvatar[] avatars = toggleFilterToCurrentPlatform.isOn
                ? availableAvatarsPlatformFiltered
                : availableAvatars;
            UPAvatar avatar = avatars[dropdownAvailableAvatars.value];
            labelUPStatus.text = $"Loading avatar {avatar.Hash}";
            StartCoroutine(AvatarCache.LoadAvatar(avatar, OnAvatarLoaded, OnAvatarLoadFailed, OnLoadProgressChanged));
        }

        void OnLoadProgressChanged(float percent)
        {
            loadProgressBar.SetPercent(percent);
        }

        void OnAvatarLoadFailed(Exception ex)
        {
            if(loadProgressBar)
                loadProgressBar.gameObject.SetActive(false);
        }

        void OnAvatarLoaded(GameObject avatar)
        {
            labelUPStatus.text = $"Loaded avatar!";

            if(loadedAvatar)
                Destroy(loadedAvatar.gameObject);

            if(loadProgressBar)
                loadProgressBar.gameObject.SetActive(false);

            var av = Instantiate(avatar, avatarRoot.position, avatarRoot.rotation);
            av.transform.parent = avatarRoot;
            av.transform.localScale = Vector3.one;

            if(av.TryGetComponent<Animator>(out var anim))
                anim.runtimeAnimatorController = avatarAnimator;

            loadedAvatar = av.AddComponent<UIAvatar>();
            loadedAvatar.rotationSpeed = avatarRotationSpeed;
        }

        public void OnFilterAvatarsToggleChanged()
        {
            dropdownAvailableAvatars.options = toggleFilterToCurrentPlatform.isOn
                ? avatarDropdownOptionsPlatformFiltered
                : avatarDropdownOptions;

            dropdownAvailableAvatars.value = 0;
        }

        public void OnAvatarDropdownChanged()
        {
            var avatars = toggleFilterToCurrentPlatform.isOn ? availableAvatarsPlatformFiltered : availableAvatars;
            UPAvatar avatar = avatars[dropdownAvailableAvatars.value];

            labelAvatarHashFunction.text = avatar?.HashFunction;
            labelAvatarHash.text = avatar?.Hash;
            labelAvatarUrl.text = avatar?.Url;
            labelAvatarFileType.text = avatar?.FileType;
        }
    }
}