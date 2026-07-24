using UnityEngine;

public class RepairsManagerUI : MonoBehaviour {

    public static RepairsManagerUI Instance { get; private set; }

    private void Awake() {
        Instance = this;
    }

    public void OpenRepair(Interactable interactable) {
        var game = Instantiate(interactable.GetMiniGamePrefab(), this.transform);

        game.OnCompleted += Game_OnCompleted;
    }

    private void Game_OnCompleted() {
        Debug.Log("Repair complete");
    }
}
