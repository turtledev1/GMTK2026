using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class RotatableTile : MonoBehaviour, IPointerClickHandler {
    [SerializeField] private float rotationDuration = 0.15f;
    [SerializeField] private int[] validAngles;

    private bool isRotating;

    public void OnPointerClick(PointerEventData eventData) {
        if (isRotating)
            return;

        StartCoroutine(Rotate90());
    }

    public bool IsCorrectAngle() {
        float currentAngle = transform.localEulerAngles.z;

        foreach (float validAngle in validAngles) {
            if (Mathf.Abs(Mathf.DeltaAngle(currentAngle, validAngle)) < 1f)
                return true;
        }

        return false;
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

    private float NormalizeAngle(float angle) {
        angle %= 360f;

        if (angle > 180f)
            angle -= 360f;

        if (angle < -180f)
            angle += 360f;

        return angle;
    }
}
