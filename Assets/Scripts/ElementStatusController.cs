using System.Collections.Generic;
using UnityEngine;

namespace DA_Game0090
{
    public class ElementStatusController : MonoBehaviour
    {
        [Header("Definitions")]
        [SerializeField] private List<StatusDefinition> statusDefinitions = new List<StatusDefinition>();
        [SerializeField] private ReactionDatabase reactionDatabase;

        private readonly Dictionary<ElementDefinition, StatusRuntime> statusLookup = new Dictionary<ElementDefinition, StatusRuntime>();

        private void Awake()
        {
            statusLookup.Clear();
            foreach (StatusDefinition status in statusDefinitions)
            {
                if (status != null && status.Element != null && !statusLookup.ContainsKey(status.Element))
                {
                    statusLookup[status.Element] = new StatusRuntime(status);
                }
            }
        }

        private void Update()
        {
            float deltaTime = Time.deltaTime;
            foreach (StatusRuntime runtime in statusLookup.Values)
            {
                runtime.Tick(deltaTime);
            }
        }

        public void ApplyElement(ElementDefinition element, float buildupAmount)
        {
            if (element == null || !statusLookup.TryGetValue(element, out StatusRuntime runtime))
            {
                return;
            }

            runtime.AddBuildup(buildupAmount);
            TryTriggerReactions();
        }

        private void TryTriggerReactions()
        {
            if (reactionDatabase == null)
            {
                return;
            }

            foreach (ReactionDefinition reaction in reactionDatabase.Reactions)
            {
                if (reaction == null || reaction.RequiredElements.Count == 0)
                {
                    continue;
                }

                if (HasAllElements(reaction.RequiredElements))
                {
                    ConsumeElements(reaction);
                    Debug.Log($"Reaction triggered: {reaction.ResultType} - {reaction.ResultDescription}", this);
                }
            }
        }

        private bool HasAllElements(IReadOnlyList<ElementDefinition> requiredElements)
        {
            foreach (ElementDefinition element in requiredElements)
            {
                if (element == null || !statusLookup.TryGetValue(element, out StatusRuntime runtime))
                {
                    return false;
                }

                if (!runtime.HasBuildup)
                {
                    return false;
                }
            }

            return true;
        }

        private void ConsumeElements(ReactionDefinition reaction)
        {
            foreach (ElementDefinition element in reaction.RequiredElements)
            {
                if (element == null || !statusLookup.TryGetValue(element, out StatusRuntime runtime))
                {
                    continue;
                }

                if (reaction.ConsumeAllBuildup)
                {
                    runtime.Clear();
                }
                else
                {
                    runtime.Consume(reaction.ConsumeAmount);
                }
            }
        }

        private class StatusRuntime
        {
            private readonly StatusDefinition definition;
            private float buildup;
            private float timeSinceApplied;

            public StatusRuntime(StatusDefinition definition)
            {
                this.definition = definition;
            }

            public bool HasBuildup => buildup > 0f;

            public void AddBuildup(float amount)
            {
                buildup = Mathf.Clamp(buildup + amount, 0f, definition.MaxBuildup);
                timeSinceApplied = 0f;
            }

            public void Consume(float amount)
            {
                buildup = Mathf.Max(0f, buildup - amount);
            }

            public void Clear()
            {
                buildup = 0f;
            }

            public void Tick(float deltaTime)
            {
                if (buildup <= 0f)
                {
                    return;
                }

                timeSinceApplied += deltaTime;
                if (timeSinceApplied < definition.DecayDelay)
                {
                    return;
                }

                float decayAmount = definition.DecayPerSecond * deltaTime;
                buildup = Mathf.Max(0f, buildup - decayAmount);
            }
        }
    }
}
