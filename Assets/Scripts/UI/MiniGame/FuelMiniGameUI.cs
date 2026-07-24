using UnityEngine;
using UnityEngine.UI;

public class FuelMiniGameUI : RepairMiniGame {

    [SerializeField] private GameObject screen;
    [SerializeField] private HoldButton fillButton;
    [SerializeField] private Image fuelFillImage;
    [SerializeField] private Button closeButton;
    [SerializeField] private float fillSpeed = 0.5f;

    private bool isFilling = false;
    private float fillAmount = 0f;

    private void Awake() {
        fillButton.OnHoldStart += () => isFilling = true;
        fillButton.OnHoldEnd += () => isFilling = false;

        closeButton.onClick.AddListener(() => {
            Close();
        });
    }

    private void Update() {
        if (!isFilling)
            return;

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
