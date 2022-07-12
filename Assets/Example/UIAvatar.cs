using UnityEngine;

namespace AvatarSDKExample
{
    /// <summary>
    /// Moves the avatar to the UI layer so the UI camera can see it and spins it around forever
    /// </summary>
    public class UIAvatar : MonoBehaviour
    {
        public float rotationSpeed;
        Transform _transform;

        void Awake()
        {
            _transform = transform;
        }

        void Start()
        {
            int layer = LayerMask.NameToLayer("UI");
            foreach(var t in GetComponentsInChildren<Transform>(true))
                t.gameObject.layer = layer;
        }

        void Update()
        {
            var euler = transform.localEulerAngles;
            _transform.localEulerAngles = new Vector3(euler.x, euler.y + rotationSpeed * Time.deltaTime, euler.z);
        }
    }
}