using UnityEngine;

namespace DA_Game0090
{
    public class PlayerAimShoot : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Camera aimCamera;
        [SerializeField] private Transform projectileSpawnPoint;
        [SerializeField] private ProjectileMotor projectilePrefab;

        [Header("Launch")]
        [SerializeField] private float launchPower = 6f;
        [SerializeField] private float maxLaunchPower = 18f;
        [SerializeField] private float minimumDragDistance = 0.1f;
        [SerializeField] private float planeZ = 0f;

        private bool isDragging;
        private Vector3 dragStartWorld;

        private void Awake()
        {
            if (aimCamera == null)
            {
                aimCamera = Camera.main;
            }
        }

        private void Update()
        {
            if (TryGetPointer(out Vector2 screenPosition, out bool isPressed, out bool isReleased))
            {
                if (isPressed)
                {
                    isDragging = true;
                    dragStartWorld = ScreenToWorldOnPlane(screenPosition);
                }

                if (isDragging)
                {
                    if (isReleased)
                    {
                        Vector3 dragEndWorld = ScreenToWorldOnPlane(screenPosition);
                        Vector3 dragVector = dragEndWorld - dragStartWorld;
                        dragVector.z = 0f;

                        if (dragVector.magnitude >= minimumDragDistance)
                        {
                            FireProjectile(dragVector);
                        }

                        isDragging = false;
                    }
                }
            }
        }

        private void FireProjectile(Vector3 dragVector)
        {
            if (projectilePrefab == null || projectileSpawnPoint == null)
            {
                return;
            }

            Vector3 direction = dragVector.normalized;
            float power = Mathf.Clamp(dragVector.magnitude * launchPower, 0f, maxLaunchPower);
            Vector3 velocity = direction * power;

            ProjectileMotor projectile = Instantiate(projectilePrefab, projectileSpawnPoint.position, Quaternion.identity);
            projectile.Launch(velocity);
        }

        private Vector3 ScreenToWorldOnPlane(Vector2 screenPosition)
        {
            if (aimCamera == null)
            {
                return Vector3.zero;
            }

            Ray ray = aimCamera.ScreenPointToRay(screenPosition);
            float distance = 0f;
            if (Mathf.Abs(ray.direction.z) > 0.0001f)
            {
                distance = (planeZ - ray.origin.z) / ray.direction.z;
            }

            Vector3 point = ray.origin + ray.direction * distance;
            point.z = planeZ;
            return point;
        }

        private bool TryGetPointer(out Vector2 screenPosition, out bool isPressed, out bool isReleased)
        {
            screenPosition = Vector2.zero;
            isPressed = false;
            isReleased = false;

            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                screenPosition = touch.position;
                isPressed = touch.phase == TouchPhase.Began;
                isReleased = touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled;
                return true;
            }

            if (Input.GetMouseButtonDown(0))
            {
                screenPosition = Input.mousePosition;
                isPressed = true;
                return true;
            }

            if (Input.GetMouseButton(0))
            {
                screenPosition = Input.mousePosition;
                return true;
            }

            if (Input.GetMouseButtonUp(0))
            {
                screenPosition = Input.mousePosition;
                isReleased = true;
                return true;
            }

            return false;
        }
    }
}
