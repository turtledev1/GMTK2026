using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class Player : MonoBehaviour {
    [SerializeField] private float moveSpeed = 8f;

    public static Player Instance { get; private set; }

    private Rigidbody2D rb;

    private bool interactPressedThisFrame;
    private bool canMove = true;
    private bool isWalking = false;
    private bool isInteracting = false;
    private readonly List<Interactable> nearbyInteractables = new();

    private void Awake() {
        Instance = this;

        rb = GetComponent<Rigidbody2D>();
    }

    private void Start() {
        GameInputManager.Instance.OnInteractAction += GameInputManager_OnInteractAction;
        GameManager.Instance.OnGameStateChanged += GameManager_OnGameStateChanged;
    }

    private void GameManager_OnGameStateChanged(object sender, System.EventArgs e) {
        if (GameManager.Instance.IsGamePlaying()) {
            canMove = true;
        } else {
            canMove = false;
        }
    }

    private void Update() {
        if (interactPressedThisFrame) {
            Interact();
            interactPressedThisFrame = false;
        }
    }

    private void FixedUpdate() {
        if (!canMove) {
            return;
        }
        if (isInteracting) {
            return;
        }

        Vector2 movementInput = GameInputManager.Instance.GetMovementVectorNormalized();

        rb.MovePosition(rb.position + movementInput * moveSpeed * Time.fixedDeltaTime);

        isWalking = movementInput != Vector2.zero;
    }

    private void GameInputManager_OnInteractAction(object sender, System.EventArgs e) {
        interactPressedThisFrame = true;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("OnTriggerEnter2D: " + other.name);
        Interactable interactable = other.GetComponent<Interactable>();

        if (interactable != null) {
            nearbyInteractables.Add(interactable);
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        Debug.Log("OnTriggerExit2D: " + other.name);
        Interactable interactable = other.GetComponent<Interactable>();

        if (interactable != null) {
            nearbyInteractables.Remove(interactable);
        }
    }

    private void Interact() {
        if (isInteracting) {
            return;
        }
        if (!GameManager.Instance.IsGamePlaying()) {
            return;
        }

        Interactable nearestInteractable = null;
        float closestDistance = float.MaxValue;

        foreach (Interactable interactable in nearbyInteractables) {
            float distance = Vector2.Distance(transform.position, interactable.transform.position);

            if (distance < closestDistance) {
                closestDistance = distance;
                nearestInteractable = interactable;
            }
        }

        if (nearestInteractable != null) {
            nearestInteractable.Interact();
            isInteracting = true;
        }
    }

    public bool IsWalking() {
        return isWalking;
    }

    public void SetIsInteracting(bool isInteracting) {
        this.isInteracting = isInteracting;
    }
}
