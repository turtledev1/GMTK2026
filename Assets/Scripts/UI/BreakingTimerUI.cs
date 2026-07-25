using TMPro;
using UnityEngine;

public class BreakingTimerUI : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI timerText;

    private RocketSystem rocketSystem;

    private void Awake() {
        gameObject.SetActive(false);
    }

    public void Initialize(RocketSystem system) {
        rocketSystem = system;
        gameObject.SetActive(true);
    }

    private void Update() {
        if (rocketSystem == null)
            return;

        timerText.text = $"{rocketSystem.TimeRemaining:F1}";

        if (!rocketSystem.IsBroken || rocketSystem.IsPermanentlyBroken) {
            gameObject.SetActive(false);
        }
    }
}
