using System;
using UnityEngine;

namespace DA_Game0090
{
    [RequireComponent(typeof(Camera))]
    public class CameraOrientationController : MonoBehaviour
    {
        [Serializable]
        public struct CameraProfile
        {
            public Vector3 position;
            public Vector3 eulerAngles;
            public float orthographicSize;
        }

        [Header("Profiles")]
        [SerializeField] private CameraProfile landscapeProfile = new CameraProfile
        {
            position = new Vector3(0f, 0f, -10f),
            eulerAngles = Vector3.zero,
            orthographicSize = 5f
        };

        [SerializeField] private CameraProfile portraitProfile = new CameraProfile
        {
            position = new Vector3(0f, 0f, -10f),
            eulerAngles = Vector3.zero,
            orthographicSize = 7f
        };

        private Camera cachedCamera;
        private bool wasLandscape;

        private void Awake()
        {
            cachedCamera = GetComponent<Camera>();
            ApplyProfile(GetIsLandscape());
        }

        private void Update()
        {
            bool isLandscape = GetIsLandscape();
            if (isLandscape != wasLandscape)
            {
                ApplyProfile(isLandscape);
            }
        }

        private bool GetIsLandscape()
        {
            return Screen.width >= Screen.height;
        }

        private void ApplyProfile(bool isLandscape)
        {
            wasLandscape = isLandscape;
            CameraProfile profile = isLandscape ? landscapeProfile : portraitProfile;
            transform.position = profile.position;
            transform.rotation = Quaternion.Euler(profile.eulerAngles);
            cachedCamera.orthographic = true;
            cachedCamera.orthographicSize = profile.orthographicSize;
        }
    }
}
