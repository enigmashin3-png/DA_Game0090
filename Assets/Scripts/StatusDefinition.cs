using UnityEngine;

namespace DA_Game0090
{
    [CreateAssetMenu(menuName = "DA_Game0090/Elements/Status Definition")]
    public class StatusDefinition : ScriptableObject
    {
        [SerializeField] private ElementDefinition element;
        [SerializeField] private float maxBuildup = 100f;
        [SerializeField] private float decayDelay = 0.5f;
        [SerializeField] private float decayPerSecond = 10f;

        public ElementDefinition Element => element;
        public float MaxBuildup => maxBuildup;
        public float DecayDelay => decayDelay;
        public float DecayPerSecond => decayPerSecond;
    }
}
