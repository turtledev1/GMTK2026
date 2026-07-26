using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    public static SoundManager Instance { get; private set; }

    private AudioSource audioSource;

    private const string PLAYER_PREFS_SOUND_EFFECTS_VOLUME = "SoundEffectsVolume";

    [SerializeField] private AudioClipRefsSO audioClipRefsSO;

    private float volume = 1f;

    private void Awake() {
        Instance = this;

        audioSource = GetComponent<AudioSource>();

        volume = PlayerPrefs.GetFloat(PLAYER_PREFS_SOUND_EFFECTS_VOLUME, 1f);
    }

    private void Start() {

    }

    public void PlayClickPositive() {
        PlaySound(audioClipRefsSO.clickPositive);
    }

    public void PlayClickNegative() {
        PlaySound(audioClipRefsSO.clickNegative);
    }

    public void PlayFootStep() {
        PlaySound(audioClipRefsSO.footstep);
    }

    private void PlaySound(AudioClip audioClip, float volumeMultiplier = 1f) {
        audioSource.PlayOneShot(audioClip, volumeMultiplier * volume);
    }

    private void PlaySound(AudioClip[] audioClips, float volumeMultiplier = 1f) {
        PlaySound(audioClips[Random.Range(0, audioClips.Length)], volumeMultiplier * volume);
    }

    public void PlayCountdownSound() {
        // PlaySound(audioClipRefsSO.warning[1], Vector3.zero);
    }

    public void SetVolume(float newVolume) {
        volume = newVolume;

        PlayerPrefs.SetFloat(PLAYER_PREFS_SOUND_EFFECTS_VOLUME, volume);
        PlayerPrefs.Save();
    }

    public float GetVolume() {
        return volume;
    }
}
