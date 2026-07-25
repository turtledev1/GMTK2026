using UnityEngine;

public class AlarmManager : MonoBehaviour {
    private AudioSource alarmSound;

    private void Awake() {
        alarmSound = GetComponent<AudioSource>();
        alarmSound.Stop();
    }

    private void Update() {
        if (FailuresManager.Instance.IsSomethingBroken()) {
            if (!alarmSound.isPlaying) {
                alarmSound.Play();
            }
        } else {
            if (alarmSound.isPlaying) {
                alarmSound.Stop();
            }
        }
    }
}
