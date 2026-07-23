using UnityEngine;
using UnityEngine.UI;

public class PauseScreenUI : MonoBehaviour {
    [SerializeField] private GameObject screen;
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button closeButton;

    private void Awake() {
        mainMenuButton.onClick.AddListener(() => {
            SceneLoader.Load(SceneLoader.Scene.MainMenu);
        });

        closeButton.onClick.AddListener(() => {
            GameManager.Instance.Unpause();
        });
    }

    private void Start() {
        GameManager.Instance.OnGameStateChanged += GameManager_OnGameStateChanged;
        Hide();
    }

    private void GameManager_OnGameStateChanged(object sender, System.EventArgs e) {
        if (GameManager.Instance.IsGamePaused()) {
            Show();
        } else {
            Hide();
        }
    }

    private void Hide() {
        screen.SetActive(false);
    }

    private void Show() {
        screen.SetActive(true);
    }
}
