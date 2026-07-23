using UnityEngine;

public class Interactable : MonoBehaviour {
    [SerializeField] private Transform canvasTransform;
    [SerializeField] private Object miniGameUIPrefab;

    public void Interact() {
        Instantiate(miniGameUIPrefab, canvasTransform);
    }
}
