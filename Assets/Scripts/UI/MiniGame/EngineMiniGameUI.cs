using UnityEngine;

public class EngineMiniGameUI : RepairMiniGame {
    [SerializeField] private RectTransform[] allTiles;
    [SerializeField] private RotatableTile[] solutionTiles;

    private void Awake() {
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
        foreach (RotatableTile tile in solutionTiles) {
            if (!tile.IsCorrectAngle()) {
                return false;
            }
        }

        return true;
    }
}
