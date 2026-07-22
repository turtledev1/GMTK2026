using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInputManager : MonoBehaviour {

    private const string PLAYER_PREFS_BINDINGS = "InputBindings";

    private InputSystem_Actions inputSystemActions;

    public static GameInputManager Instance { get; private set; }

    public event EventHandler OnJumpAction;
    public event EventHandler OnInteractAction;
    public event EventHandler OnPauseAction;

    public enum Binding {
        MoveUp,
        MoveDown,
        MoveLeft,
        MoveRight,
        Jump,
        Interact,
        Reset,
        Pause,
    }

    private void Awake() {
        Instance = this;

        inputSystemActions = new InputSystem_Actions();

        if (PlayerPrefs.HasKey(PLAYER_PREFS_BINDINGS)) {
            inputSystemActions.LoadBindingOverridesFromJson(PlayerPrefs.GetString(PLAYER_PREFS_BINDINGS));
        }

        inputSystemActions.Player.Enable();

        inputSystemActions.Player.Jump.performed += Jump_performed;
        inputSystemActions.Player.Interact.performed += Interact_performed;
        inputSystemActions.Player.Pause.performed += Pause_performed;
    }

    private void OnDestroy() {
        inputSystemActions.Player.Jump.performed -= Jump_performed;
        inputSystemActions.Player.Interact.performed -= Interact_performed;
        inputSystemActions.Player.Pause.performed -= Pause_performed;

        inputSystemActions.Dispose();
    }

    private void Jump_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
        OnJumpAction?.Invoke(this, EventArgs.Empty);
    }

    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
        OnInteractAction?.Invoke(this, EventArgs.Empty);
    }

    private void Pause_performed(InputAction.CallbackContext obj) {
        OnPauseAction?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 GetMovementVectorNormalized() {
        Vector2 inputVector = inputSystemActions.Player.Move.ReadValue<Vector2>();

        return inputVector.normalized;
    }
}
