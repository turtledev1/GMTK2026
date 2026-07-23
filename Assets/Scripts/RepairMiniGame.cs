using System;
using UnityEngine;

public abstract class RepairMiniGame : MonoBehaviour {
    public event Action Completed;

    protected void Complete() {
        Completed?.Invoke();
    }
}
