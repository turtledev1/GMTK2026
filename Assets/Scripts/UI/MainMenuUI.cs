using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour {
    [SerializeField] private Button playButton;
    [SerializeField] private Button quitButton;

    private void Awake() {
        playButton.onClick.AddListener(() => {
            SoundManager.Instance.PlayClickPositive();
            StartCoroutine(LoadGameAfterDelay());
        });

        quitButton.onClick.AddListener(() => {
            SoundManager.Instance.PlayClickNegative();
            StartCoroutine(QuitAfterDelay());
        });
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
