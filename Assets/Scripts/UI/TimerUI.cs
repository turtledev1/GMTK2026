using TMPro;
using UnityEngine;

public class TimerUI : MonoBehaviour {

    [SerializeField] private TextMeshProUGUI timerText;

    void Start() {

    }

    void Update() {
        int totalSeconds = Mathf.RoundToInt(GameManager.Instance.GetTime());
        int minutes = totalSeconds / 60;
        int seconds = totalSeconds % 60;
        timerText.text = $"{minutes}:{seconds:D2}";
    }
}
