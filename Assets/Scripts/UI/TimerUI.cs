using TMPro;
using UnityEngine;

public class TimerUI : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private float finalFontSize = 590f;

    private float originalFontSize;
    private AudioSource audioSource;

    private int lastDisplayedSecond = -1;

    private void Awake() {
        audioSource = GetComponent<AudioSource>();
        originalFontSize = timerText.fontSize;
    }

    private void Update() {
        int totalSeconds = Mathf.CeilToInt(GameManager.Instance.GetTime());

        // Trigger events only once per second.
        if (totalSeconds != lastDisplayedSecond) {
            lastDisplayedSecond = totalSeconds;

            // Beep every minute (5:00, 4:00, 3:00, 2:00, 1:00)
            if (totalSeconds > 0 && totalSeconds % 60 == 0) {
                audioSource.Play();
            }

            // Beep during the last 10 seconds (10..1)
            if (totalSeconds <= 10 && totalSeconds > 0) {
                audioSource.Play();
            }
        }

        int minutes = totalSeconds / 60;
        int seconds = totalSeconds % 60;
        timerText.text = $"{minutes}:{seconds:D2}";

        if (totalSeconds > 10) {
            timerText.fontSize = originalFontSize;
        } else {
            int step = 10 - totalSeconds;
            timerText.fontSize = Mathf.Lerp(originalFontSize, finalFontSize, step / 10f);
        }
    }
}
