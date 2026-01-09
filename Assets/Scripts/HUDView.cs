using UnityEngine;
namespace DA_Game0090
{
    public class HUDView : MonoBehaviour
    {
        [Header("HUD Elements")]
        [SerializeField] private GameObject hpBar;
        [SerializeField] private GameObject waveCounter;
        [SerializeField] private GameObject pauseButton;
        [SerializeField] private GameObject upgradePromptIndicator;

        public GameObject HpBar => hpBar;
        public GameObject WaveCounter => waveCounter;
        public GameObject PauseButton => pauseButton;
        public GameObject UpgradePromptIndicator => upgradePromptIndicator;

        public void SetUpgradePromptVisible(bool isVisible)
        {
            if (upgradePromptIndicator != null)
            {
                upgradePromptIndicator.SetActive(isVisible);
            }
        }
    }
}
