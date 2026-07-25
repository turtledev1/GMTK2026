using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class RotatableTile : MonoBehaviour, IPointerClickHandler {
    [SerializeField] private float rotationDuration = 0.15f;

    private bool isRotating;

    public void OnPointerClick(PointerEventData eventData) {
        if (isRotating)
            return;

        StartCoroutine(Rotate90());
    }

    private IEnumerator Rotate90() {
        isRotating = true;

        Quaternion startRotation = transform.localRotation;
        Quaternion endRotation = startRotation * Quaternion.Euler(0, 0, 90f);

        float timer = 0f;

        while (timer < rotationDuration) {
            timer += Time.deltaTime;

            transform.localRotation = Quaternion.Lerp(
                startRotation,
                endRotation,
                timer / rotationDuration
            );

            yield return null;
        }

        transform.localRotation = endRotation;

        isRotating = false;
    }
}
