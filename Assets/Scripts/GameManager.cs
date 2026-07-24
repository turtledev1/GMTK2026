using System;
using UnityEngine;

public class GameManager : MonoBehaviour {

    [SerializeField] private int maxTimeMinute = 5;

    public static GameManager Instance { get; private set; }

    public event EventHandler OnGameStateChanged;

    private float currentTimeLeftSeconds;

    private enum State {
        WaitingToStart,
        GamePlaying,
        Paused,
        LaunchSequence,
        Ending,
    }

    private State state;

    private void Awake() {
        Instance = this;

        UpAndRunningSystems.ResetSystems();
        state = State.GamePlaying;
        currentTimeLeftSeconds = GetGameDuration();
    }

    private void Start() {
        GameInputManager.Instance.OnPauseAction += GameInputManager_OnPauseAction;
    }

    private void GameInputManager_OnPauseAction(object sender, EventArgs e) {
        if (state == State.GamePlaying) {
            if (!RepairsManagerUI.Instance.IsRepairing()) {
                Debug.Log("Pausing game");
                ChangeState(State.Paused);
            } else {
                Debug.Log("Repair UI opened, closing it instead");
                RepairsManagerUI.Instance.CloseRepair();
            }
        } else if (state == State.Paused) {
            Unpause();
        }
    }

    void Update() {
        switch (state) {
            case State.WaitingToStart:
                break;
            case State.GamePlaying:
                currentTimeLeftSeconds -= Time.deltaTime;
                if (currentTimeLeftSeconds <= 0) {
                    currentTimeLeftSeconds = 0;
                    ChangeState(State.LaunchSequence);
                }
                break;
            case State.Paused:
                break;
            case State.LaunchSequence:
                break;
            case State.Ending:
                break;
        }
    }

    private void ChangeState(State newState) {
        state = newState;
        OnGameStateChanged?.Invoke(this, EventArgs.Empty);
    }

    public void Unpause() {
        ChangeState(State.GamePlaying);
    }

    public bool IsGamePlaying() {
        return state == State.GamePlaying;
    }

    public bool IsGamePaused() {
        return state == State.Paused;
    }

    public float GetTime() {
        return currentTimeLeftSeconds;
    }

    public float GetGameDuration() {
        return maxTimeMinute * 60;
    }
}
