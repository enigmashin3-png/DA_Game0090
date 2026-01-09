using UnityEngine;

namespace DA_Game0090
{
    public class TrajectoryPredictor : MonoBehaviour
    {
        [Header("Rendering")]
        [SerializeField] private LineRenderer lineRenderer;

        [Header("Simulation")]
        [SerializeField] private float gravity = 9.81f;
        [SerializeField] private float drag = 0.1f;
        [SerializeField] private float radius = 0.15f;
        [SerializeField] private float planeZ = 0f;
        [SerializeField] private float simulationStep = 0.05f;
        [SerializeField] private LayerMask collisionMask = ~0;

        [Header("Steps")]
        [SerializeField] private int landscapeSteps = 30;
        [SerializeField] private int portraitSteps = 20;

        private Vector3[] pointsBuffer;

        private void Awake()
        {
            EnsureBuffer();
        }

        private void OnValidate()
        {
            EnsureBuffer();
        }

        public int Predict(Vector3 startPosition, Vector3 startVelocity, Vector3[] outPoints)
        {
            if (outPoints == null || outPoints.Length == 0)
            {
                return 0;
            }

            int stepCount = GetStepCount();
            int maxPoints = Mathf.Min(outPoints.Length, stepCount + 1);

            Vector3 position = startPosition;
            position.z = planeZ;
            Vector3 velocity = startVelocity;

            outPoints[0] = position;
            int count = 1;

            for (int i = 0; i < stepCount && count < maxPoints; i++)
            {
                velocity += Vector3.down * gravity * simulationStep;
                velocity -= velocity * drag * simulationStep;

                Vector3 displacement = velocity * simulationStep;
                float distance = displacement.magnitude;

                if (distance > 0.0001f)
                {
                    Vector3 direction = displacement / distance;
                    if (Physics.SphereCast(position, radius, direction, out RaycastHit hit, distance, collisionMask))
                    {
                        Vector3 hitPoint = hit.point;
                        hitPoint.z = planeZ;
                        outPoints[count++] = hitPoint;
                        return count;
                    }
                }

                position += displacement;
                position.z = planeZ;
                outPoints[count++] = position;
            }

            return count;
        }

        public void RenderTrajectory(Vector3 startPosition, Vector3 startVelocity)
        {
            if (lineRenderer == null)
            {
                return;
            }

            EnsureBuffer();
            int pointCount = Predict(startPosition, startVelocity, pointsBuffer);
            lineRenderer.positionCount = pointCount;

            for (int i = 0; i < pointCount; i++)
            {
                lineRenderer.SetPosition(i, pointsBuffer[i]);
            }
        }

        public void ClearTrajectory()
        {
            if (lineRenderer != null)
            {
                lineRenderer.positionCount = 0;
            }
        }

        private int GetStepCount()
        {
            bool isLandscape = Screen.width >= Screen.height;
            int steps = isLandscape ? landscapeSteps : portraitSteps;
            return Mathf.Max(1, steps);
        }

        private void EnsureBuffer()
        {
            int bufferSize = Mathf.Max(landscapeSteps, portraitSteps) + 1;
            if (pointsBuffer == null || pointsBuffer.Length != bufferSize)
            {
                pointsBuffer = new Vector3[bufferSize];
            }
        }
    }
}
