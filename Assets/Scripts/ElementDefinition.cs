using UnityEngine;

namespace DA_Game0090
{
    [CreateAssetMenu(menuName = "DA_Game0090/Elements/Element Definition")]
    public class ElementDefinition : ScriptableObject
    {
        [SerializeField] private string elementName;
        [SerializeField] private Color elementColor = Color.white;

        public string ElementName => elementName;
        public Color ElementColor => elementColor;
    }
}
