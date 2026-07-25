using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FadeInUI : MonoBehaviour {
    [SerializeField] private Image fadeImage;
    [SerializeField] private float fadeDuration = 1f;

    private void Start() {
        StartCoroutine(FadeOutCoroutine(1f, 0f));
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

        // TODO: Start ending animation
    }
}
