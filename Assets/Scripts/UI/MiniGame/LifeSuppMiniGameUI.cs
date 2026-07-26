using UnityEngine;
using UnityEngine.UI;

public class LifeSuppMiniGameUI : RepairMiniGame {
    [SerializeField] private Slider[] sliders;
    [SerializeField] private RectTransform[] targets;
    [SerializeField] private int tolerance = 3;

    private float[] targetValues;

    private void Awake() {
        targetValues = new float[sliders.Length];

        for (int i = 0; i < sliders.Length; i++) {
            targetValues[i] = Random.Range(15f, 85f);

            // Set target Y position based on target. Values are from -200 to 130
            float targetY = Mathf.Lerp(-200f, 130f, targetValues[i] / 100f);
            targets[i].anchoredPosition = new Vector2(targets[i].anchoredPosition.x, targetY);
        }
    }

    private void Update() {
        for (int i = 0; i < sliders.Length; i++) {
            if (Mathf.Abs(sliders[i].value - targetValues[i]) > tolerance)
                return;
        }

        Complete();
    }
}