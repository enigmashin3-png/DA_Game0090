using UnityEngine;

namespace DA_Game0090
{
    public class ProjectileMotor : MonoBehaviour
    {
        [Header("Motion")]
        [SerializeField] private float gravity = 9.81f;
        [SerializeField] private float drag = 0.1f;
        [SerializeField] private float radius = 0.15f;
        [SerializeField] private float planeZ = 0f;

        [Header("Collision")]
        [SerializeField] private LayerMask collisionMask = ~0;
        [SerializeField] private ProjectileCollisionResponse defaultResponse = ProjectileCollisionResponse.StickIntoTarget;
        [SerializeField] private int maxBounces = 2;
        [SerializeField] private int maxPenetrations = 1;

        private Vector3 velocity;
        private int remainingBounces;
        private int remainingPenetrations;
        private bool isActive;

        private void Awake()
        {
            remainingBounces = maxBounces;
            remainingPenetrations = maxPenetrations;
        }

        private void Update()
        {
            if (!isActive)
            {
                return;
            }

            float deltaTime = Time.deltaTime;
            velocity += Vector3.down * gravity * deltaTime;
            velocity -= velocity * drag * deltaTime;

            Vector3 displacement = velocity * deltaTime;
            float distance = displacement.magnitude;

            if (distance > 0.0001f)
            {
                Vector3 direction = displacement / distance;
                if (Physics.SphereCast(transform.position, radius, direction, out RaycastHit hit, distance, collisionMask))
                {
                    HandleHit(hit, direction);
                    return;
                }
            }

            MoveBy(displacement);
        }

        public void Launch(Vector3 initialVelocity)
        {
            velocity = initialVelocity;
            isActive = true;
            remainingBounces = maxBounces;
            remainingPenetrations = maxPenetrations;
        }

        private void HandleHit(RaycastHit hit, Vector3 direction)
        {
            ProjectileCollisionResponse response = defaultResponse;
            int responseBounces = remainingBounces;
            int responsePenetrations = remainingPenetrations;

            ProjectileImpactResponse responseOverride = hit.collider.GetComponent<ProjectileImpactResponse>();
            if (responseOverride != null)
            {
                response = responseOverride.Response;
                responseBounces = responseOverride.MaxBounces;
                responsePenetrations = responseOverride.MaxPenetrations;
            }

            switch (response)
            {
                case ProjectileCollisionResponse.Bounce:
                    HandleBounce(hit, responseBounces);
                    break;
                case ProjectileCollisionResponse.Pierce:
                    HandlePierce(hit, responsePenetrations, direction);
                    break;
                default:
                    HandleStick(hit);
                    break;
            }
        }

        private void HandleStick(RaycastHit hit)
        {
            Vector3 position = hit.point - hit.normal * radius;
            position.z = planeZ;
            transform.position = position;
            transform.SetParent(hit.transform);
            isActive = false;
        }

        private void HandleBounce(RaycastHit hit, int responseBounces)
        {
            if (remainingBounces <= 0 && responseBounces <= 0)
            {
                HandleStick(hit);
                return;
            }

            if (responseBounces >= remainingBounces)
            {
                remainingBounces = responseBounces - 1;
            }
            else
            {
                remainingBounces--;
            }

            velocity = Vector3.Reflect(velocity, hit.normal);
            Vector3 position = hit.point + hit.normal * radius;
            position.z = planeZ;
            transform.position = position;
        }

        private void HandlePierce(RaycastHit hit, int responsePenetrations, Vector3 direction)
        {
            if (remainingPenetrations <= 0 && responsePenetrations <= 0)
            {
                HandleStick(hit);
                return;
            }

            if (responsePenetrations >= remainingPenetrations)
            {
                remainingPenetrations = responsePenetrations - 1;
            }
            else
            {
                remainingPenetrations--;
            }

            Vector3 position = hit.point + direction * radius;
            position.z = planeZ;
            transform.position = position;
        }

        private void MoveBy(Vector3 displacement)
        {
            Vector3 newPosition = transform.position + displacement;
            newPosition.z = planeZ;
            transform.position = newPosition;
        }
    }
}
