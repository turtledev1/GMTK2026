using UnityEngine;

public class EngineMiniGameUI : RepairMiniGame {
    [SerializeField] private RectTransform[] allTiles;
    [SerializeField] private RectTransform[] solutionTiles;

    private void Awake() {
        for (int i = 0; i < allTiles.Length; i++) {
            Debug.Log($"All tile {i}: {allTiles[i]}");
        }

        for (int i = 0; i < solutionTiles.Length; i++) {
            Debug.Log($"Solution tile {i}: {solutionTiles[i]}");
        }
        RandomizeTiles();
    }

    private void RandomizeTiles() {
        foreach (RectTransform tile in allTiles) {
            int rotationSteps = Random.Range(0, 4);

            tile.localRotation = Quaternion.Euler(0, 0, rotationSteps * 90f);
        }
    }

    private void Update() {
        if (IsSolved()) {
            Complete();
        }
    }

    private bool IsSolved() {
        foreach (RectTransform tile in solutionTiles) {
            float rotation = NormalizeAngle(tile.localEulerAngles.z);

            if (!Mathf.Approximately(rotation, 0f)) {
                return false;
            }
        }

        return true;
    }

    private float NormalizeAngle(float angle) {
        angle %= 360f;

        if (angle < 0) {
            angle += 360f;
        }

        return angle;
    }
}
