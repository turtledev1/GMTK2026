using System;
using UnityEngine;

public class SelectedRocketSystem : MonoBehaviour {
    [SerializeField] private RocketSystem selectedSystem;
    [SerializeField] private Transform selectedSystemObject;

    private void Start() {
        Player.Instance.OnInteractableChanged += Player_OnInteractableChanged;
    }

    private void Player_OnInteractableChanged(object sender, EventArgs e) {
        if (Player.Instance.GetNearbyInteractables().Contains(selectedSystem)) {
            selectedSystemObject.gameObject.SetActive(true);
        } else {
            selectedSystemObject.gameObject.SetActive(false);
        }
    }
}
