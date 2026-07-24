using UnityEngine;
using UnityEngine.UI;

public class NavigationMiniGame : RepairMiniGame {
    [SerializeField] private Slider slider;
    [SerializeField] private RectTransform rocket;
    [SerializeField] private RectTransform target;
    [SerializeField] private RectTransform arrow;

    [SerializeField] private float targetTolerance = 5f;
    [SerializeField] private float lockDuration = 2f;
    [SerializeField] private float arrowDistance = 50f;

    private float targetAngle;
    private float lockTimer;

    private Vector2 arrowStartPosition;

    private void Awake() {
        arrowStartPosition = arrow.anchoredPosition;

        float startAngle = Random.Range(0f, 360f);
        slider.value = startAngle / 360f;

        targetAngle = Random.Range(0f, 360f);

        UpdateRocket(startAngle);
        UpdateTarget(targetAngle);
    }

    private void Update() {
        float rocketAngle = slider.value * 360f;

        UpdateRocket(rocketAngle);

        float difference = Mathf.Abs(Mathf.DeltaAngle(rocketAngle, targetAngle));

        if (difference <= targetTolerance) {
            lockTimer += Time.deltaTime;

            MoveArrowForward(lockTimer / lockDuration);

            if (lockTimer >= lockDuration) {
                Complete();
            }
        } else {
            lockTimer = 0f;
            ResetArrow();
        }
    }

    private void UpdateRocket(float angle) {
        rocket.localRotation = Quaternion.Euler(0, 0, -angle);
    }

    private void UpdateTarget(float angle) {
        target.localRotation = Quaternion.Euler(0, 0, -angle);
    }

    private void MoveArrowForward(float progress) {
        arrow.anchoredPosition = arrowStartPosition + Vector2.up * (arrowDistance * progress);
    }

    private void ResetArrow() {
        arrow.anchoredPosition = arrowStartPosition;
    }
}
