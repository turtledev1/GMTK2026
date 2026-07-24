using System;
using UnityEngine;

public abstract class RepairMiniGame : MonoBehaviour {
    public event Action OnCompleted;

    protected void Complete() {
        Debug.Log("Repair mini-game completed!");
        OnCompleted?.Invoke();
        Close();
    }

    protected void Close() {
        Player.Instance.SetIsInteracting(false);
        Destroy(gameObject);
    }
}
