using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour {
    [SerializeField] private Button playButton;
    [SerializeField] private Button howToPlayButton;
    [SerializeField] private Button quitButton;

    [SerializeField] private GameObject mainMenuUI;
    [SerializeField] private GameObject howToPlayUI;

    [SerializeField] private Button backToMainMenuButton;

    private void Awake() {
        playButton.onClick.AddListener(() => {
            SoundManager.Instance.PlayClickPositive();
            StartCoroutine(LoadGameAfterDelay());
        });

        howToPlayButton.onClick.AddListener(() => {
            SoundManager.Instance.PlayClickPositive();
            mainMenuUI.SetActive(false);
            howToPlayUI.SetActive(true);
        });
        backToMainMenuButton.onClick.AddListener(() => {
            SoundManager.Instance.PlayClickPositive();
            mainMenuUI.SetActive(true);
            howToPlayUI.SetActive(false);
        });

        quitButton.onClick.AddListener(() => {
            SoundManager.Instance.PlayClickNegative();
            StartCoroutine(QuitAfterDelay());
        });
    }

    private void Start() {
        mainMenuUI.SetActive(true);
        howToPlayUI.SetActive(false);
    }

    private IEnumerator LoadGameAfterDelay() {
        yield return new WaitForSeconds(1f);
        SceneLoader.Load(SceneLoader.Scene.Game);
    }

    private IEnumerator QuitAfterDelay() {
        yield return new WaitForSeconds(1f);
        Application.Quit();
    }
}
