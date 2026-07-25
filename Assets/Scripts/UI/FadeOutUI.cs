using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FadeOutUI : MonoBehaviour {
    [SerializeField] private Image fadeImage;
    [SerializeField] private float fadeDuration = 1f;

    private void Start() {
        GameManager.Instance.OnGameStateChanged += GameManager_OnGameStateChanged;
    }

    private void GameManager_OnGameStateChanged(object sender, EventArgs e) {
        if (GameManager.Instance.IsGameEnding()) {
            StartCoroutine(FadeOutCoroutine(0f, 1f));
        }
    }

    private IEnumerator FadeOutCoroutine(float from, float to) {
        Color color = fadeImage.color;

        float timer = 0f;

        while (timer < fadeDuration) {
            timer += Time.deltaTime;

            color.a = Mathf.Lerp(from, to, timer / fadeDuration);
            fadeImage.color = color;

            yield return null;
        }

        color.a = to;
        fadeImage.color = color;

        SceneManager.LoadScene(SceneLoader.Scene.Ending.ToString());
    }
}
