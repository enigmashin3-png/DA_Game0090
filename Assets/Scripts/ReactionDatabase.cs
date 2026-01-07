using System.Collections.Generic;
using UnityEngine;

namespace DA_Game0090
{
    [CreateAssetMenu(menuName = "DA_Game0090/Elements/Reaction Database")]
    public class ReactionDatabase : ScriptableObject
    {
        [SerializeField] private List<ReactionDefinition> reactions = new List<ReactionDefinition>();

        public IReadOnlyList<ReactionDefinition> Reactions => reactions;
    }
}
