using System;
using UnityEngine;

public abstract class RepairMiniGame : MonoBehaviour {
    public event Action Completed;

    protected void Complete() {
        Debug.Log("Repair mini-game completed!");
        Completed?.Invoke();
    }
}
