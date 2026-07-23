using TMPro;
using UnityEngine;

public class TimerUI : MonoBehaviour {

    [SerializeField] private TextMeshProUGUI timerText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        int totalSeconds = Mathf.RoundToInt(GameManager.Instance.GetTime());
        int minutes = totalSeconds / 60;
        int seconds = totalSeconds % 60;
        timerText.text = $"{minutes}:{seconds:D2}";
    }
}
