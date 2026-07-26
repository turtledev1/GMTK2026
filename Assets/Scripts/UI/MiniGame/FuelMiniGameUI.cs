using UnityEngine;
using UnityEngine.UI;

public class FuelMiniGameUI : RepairMiniGame {

    [SerializeField] private GameObject screen;
    [SerializeField] private HoldButton fillButton;
    [SerializeField] private Image fuelFillImage;
    [SerializeField] private float fillSpeed = 0.5f;

    private AudioSource fillSound;

    private bool isFilling = false;
    private float fillAmount = 0f;

    private void Awake() {
        fillSound = GetComponent<AudioSource>();

        fillButton.OnHoldStart += () => isFilling = true;
        fillButton.OnHoldEnd += () => isFilling = false;
    }

    private void Update() {
        if (!isFilling) {
            fillSound.Pause();
            return;
        }

        fillSound.UnPause();

        fillAmount += fillSpeed * Time.deltaTime;
        fillAmount = Mathf.Clamp01(fillAmount);

        fuelFillImage.fillAmount = fillAmount;

        if (fillAmount >= 1f) {
            fillAmount = 1f;
            isFilling = false;

            Complete();
        }
    }
}
