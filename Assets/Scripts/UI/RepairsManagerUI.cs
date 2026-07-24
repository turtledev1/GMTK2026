using System;
using UnityEngine;

public class RepairsManagerUI : MonoBehaviour {

    public static RepairsManagerUI Instance { get; private set; }

    private RocketSystem currentlyBrokenSystem;
    private RepairMiniGame currentMiniGame;

    private void Awake() {
        Instance = this;
    }

    public void OpenRepair(RocketSystem rocketSystem) {
        currentlyBrokenSystem = rocketSystem;
        currentMiniGame = Instantiate(rocketSystem.GetMiniGamePrefab(), this.transform);

        currentMiniGame.OnCompleted += Game_OnCompleted;
    }

    public void CloseRepair() {
        if (!IsRepairing()) {
            return;
        }
        Destroy(currentMiniGame.gameObject);
        currentlyBrokenSystem = null;
        currentMiniGame = null;

        Player.Instance.SetIsInteracting(false);
    }

    private void Game_OnCompleted() {
        currentlyBrokenSystem.Repair();
        CloseRepair();
    }

    public bool IsRepairing() {
        return currentlyBrokenSystem != null;
    }
}
