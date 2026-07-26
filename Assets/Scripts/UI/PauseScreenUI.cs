using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PauseScreenUI : MonoBehaviour {
    [SerializeField] private GameObject screen;
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button closeButton;

    private void Awake() {
        mainMenuButton.onClick.AddListener(() => {
            SoundManager.Instance.PlayClickPositive();
            StartCoroutine(QuitAfterDelay());
        });

        closeButton.onClick.AddListener(() => {
            SoundManager.Instance.PlayClickPositive();
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

    private IEnumerator QuitAfterDelay() {
        yield return new WaitForSeconds(1f);
        SceneLoader.Load(SceneLoader.Scene.MainMenu);
    }
}
