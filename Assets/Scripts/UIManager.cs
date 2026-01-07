using UnityEngine;

namespace DA_Game0090
{
    public class UIManager : MonoBehaviour
    {
        [Header("Prefabs")]
        [SerializeField] private GameObject hudLandscapePrefab;
        [SerializeField] private GameObject hudPortraitPrefab;
        [SerializeField] private Transform uiParent;
        [SerializeField] private bool applySafeArea = true;

        private GameObject activeHud;
        private bool wasLandscape;

        private void Awake()
        {
            ApplyHudForCurrentOrientation(forceRefresh: true);
        }

        private void Update()
        {
            ApplyHudForCurrentOrientation(forceRefresh: false);
        }

        private void ApplyHudForCurrentOrientation(bool forceRefresh)
        {
            bool isLandscape = Screen.width >= Screen.height;
            if (!forceRefresh && isLandscape == wasLandscape)
            {
                return;
            }

            wasLandscape = isLandscape;
            GameObject prefab = isLandscape ? hudLandscapePrefab : hudPortraitPrefab;
            if (prefab == null)
            {
                return;
            }

            if (activeHud != null)
            {
                Destroy(activeHud);
            }

            Transform parent = uiParent != null ? uiParent : transform;
            activeHud = Instantiate(prefab, parent);

            if (applySafeArea)
            {
                ApplySafeArea(activeHud);
            }
        }

        private void ApplySafeArea(GameObject hudRoot)
        {
            RectTransform safeAreaRoot = FindSafeAreaRoot(hudRoot);
            if (safeAreaRoot == null)
            {
                return;
            }

            Rect safeArea = Screen.safeArea;
            Vector2 anchorMin = safeArea.position;
            Vector2 anchorMax = safeArea.position + safeArea.size;
            anchorMin.x /= Screen.width;
            anchorMin.y /= Screen.height;
            anchorMax.x /= Screen.width;
            anchorMax.y /= Screen.height;

            safeAreaRoot.anchorMin = anchorMin;
            safeAreaRoot.anchorMax = anchorMax;
            safeAreaRoot.offsetMin = Vector2.zero;
            safeAreaRoot.offsetMax = Vector2.zero;
        }

        private RectTransform FindSafeAreaRoot(GameObject hudRoot)
        {
            RectTransform[] rects = hudRoot.GetComponentsInChildren<RectTransform>(true);
            foreach (RectTransform rect in rects)
            {
                if (rect.name == "SafeAreaRoot")
                {
                    return rect;
                }
            }

            return hudRoot.GetComponent<RectTransform>();
        }
    }
}
