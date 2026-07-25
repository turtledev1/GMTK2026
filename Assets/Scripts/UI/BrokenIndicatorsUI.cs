using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BrokenIndicatorsUI : MonoBehaviour {
    [SerializeField] private Camera playerCamera;
    [SerializeField] private RectTransform canvas;
    [SerializeField] private GameObject arrowPrefab;

    public static BrokenIndicatorsUI Instance { get; private set; }

    private Dictionary<RocketSystem, RectTransform> arrows = new();

    private void Awake() {
        Instance = this;
    }
    private void Update() {
        foreach (var pair in arrows) {
            UpdateArrow(pair.Key, pair.Value);
        }
    }

    public void ImBroken(RocketSystem system) {
        if (arrows.ContainsKey(system))
            return;

        GameObject arrow = Instantiate(arrowPrefab, transform);

        arrows.Add(system, arrow.GetComponent<RectTransform>());
    }

    public void ImRepaired(RocketSystem system) {
        if (!arrows.TryGetValue(system, out var arrow))
            return;

        Destroy(arrow.gameObject);
        arrows.Remove(system);
    }

    private void UpdateArrow(RocketSystem system, RectTransform arrow) {
        Vector3 viewport = playerCamera.WorldToViewportPoint(system.transform.position);

        bool visible =
            viewport.x > 0 &&
            viewport.x < 1 &&
            viewport.y > 0 &&
            viewport.y < 1 &&
            viewport.z > 0;

        arrow.gameObject.SetActive(!visible);

        if (visible)
            return;

        Vector3 direction = system.transform.position - playerCamera.transform.position;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        arrow.rotation = Quaternion.Euler(0, 0, angle);

        Vector2 screenPosition = new Vector2(
                (viewport.x - 0.5f) * canvas.sizeDelta.x,
                (viewport.y - 0.5f) * canvas.sizeDelta.y
            );

        float padding = 100f;

        screenPosition.x = Mathf.Clamp(
                screenPosition.x,
                -canvas.sizeDelta.x / 2 + padding,
                canvas.sizeDelta.x / 2 - padding
            );

        screenPosition.y = Mathf.Clamp(
                screenPosition.y,
                -canvas.sizeDelta.y / 2 + padding,
                canvas.sizeDelta.y / 2 - padding
            );

        arrow.anchoredPosition = screenPosition;
    }
}
