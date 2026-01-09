using System.Collections.Generic;
using UnityEngine;

namespace DA_Game0090
{
    public enum ReactionResultType
    {
        ChainLightning,
        Shatter,
        Explode,
        SteamBurst,
        Overload,
        ToxicCloud
    }

    [CreateAssetMenu(menuName = "DA_Game0090/Elements/Reaction Definition")]
    public class ReactionDefinition : ScriptableObject
    {
        [SerializeField] private List<ElementDefinition> requiredElements = new List<ElementDefinition>();
        [SerializeField] private ReactionResultType resultType = ReactionResultType.ChainLightning;
        [SerializeField] private string resultDescription;
        [SerializeField] private bool consumeAllBuildup = true;
        [SerializeField] private float consumeAmount = 50f;

        public IReadOnlyList<ElementDefinition> RequiredElements => requiredElements;
        public ReactionResultType ResultType => resultType;
        public string ResultDescription => resultDescription;
        public bool ConsumeAllBuildup => consumeAllBuildup;
        public float ConsumeAmount => consumeAmount;
    }
}
