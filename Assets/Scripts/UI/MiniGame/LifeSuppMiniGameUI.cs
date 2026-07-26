using UnityEngine;
using UnityEngine.UI;

public class LifeSuppMiniGameUI : RepairMiniGame {
    [SerializeField] private Slider[] sliders;
    [SerializeField] private RectTransform[] targets;
    [SerializeField] private int tolerance = 5;

    private Color defaultColor = Color.black;
    private Color correctColor = new Color(0.3f, 0.8f, 0.3f);

    private float[] targetValues;
    private Image[] targetImages;

    private void Awake() {
        targetValues = new float[sliders.Length];
        targetImages = new Image[targets.Length];

        for (int i = 0; i < sliders.Length; i++) {
            targetValues[i] = Random.Range(15f, 85f);
            targetImages[i] = targets[i].GetComponent<Image>();

            // Set target Y position based on target. Values are from -200 to 130
            float targetY = Mathf.Lerp(-200f, 130f, targetValues[i] / 100f);
            targets[i].anchoredPosition = new Vector2(targets[i].anchoredPosition.x, targetY);
        }
    }

    private void Update() {
        bool allCorrect = true;

        for (int i = 0; i < sliders.Length; i++) {
            bool correct = Mathf.Abs(sliders[i].value - targetValues[i]) <= tolerance;

            targetImages[i].color = correct ? correctColor : defaultColor;

            if (!correct) {
                allCorrect = false;
            }
        }

        if (allCorrect) {
            Complete();
        }
    }
}