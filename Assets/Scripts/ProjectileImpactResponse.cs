using UnityEngine;

namespace DA_Game0090
{
    public enum ProjectileCollisionResponse
    {
        StickIntoTarget,
        Bounce,
        Pierce
    }

    public class ProjectileImpactResponse : MonoBehaviour
    {
        [SerializeField] private ProjectileCollisionResponse response = ProjectileCollisionResponse.StickIntoTarget;
        [SerializeField] private int maxBounces = 2;
        [SerializeField] private int maxPenetrations = 1;

        public ProjectileCollisionResponse Response => response;
        public int MaxBounces => maxBounces;
        public int MaxPenetrations => maxPenetrations;
    }
}
