using UnityEngine;
using UnityEngine.UI;

public class AlarmManager : MonoBehaviour {
    [SerializeField] private Image alarmLight;
    [SerializeField] private float flashSpeed = 3f;
    [SerializeField] private float minAlpha = 0.1f;
    [SerializeField] private float maxAlpha = 0.5f;

    private AudioSource alarmSound;

    private void Awake() {
        alarmSound = GetComponent<AudioSource>();
        alarmSound.Stop();

        SetAlpha(0f);
    }

    private void Update() {
        bool isBroken = FailuresManager.Instance.IsSomethingBroken();

        if (isBroken) {
            if (!alarmSound.isPlaying)
                alarmSound.Play();

            float t = Mathf.PingPong(Time.time * flashSpeed, 1f);
            float alpha = Mathf.Lerp(minAlpha, maxAlpha, t);
            SetAlpha(alpha);
        } else {
            if (alarmSound.isPlaying)
                alarmSound.Stop();

            SetAlpha(0f);
        }
    }

    private void SetAlpha(float alpha) {
        Color color = alarmLight.color;
        color.a = alpha;
        alarmLight.color = color;
    }
}
