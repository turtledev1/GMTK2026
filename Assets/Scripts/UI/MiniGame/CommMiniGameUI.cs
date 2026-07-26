using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CommMiniGameUI : RepairMiniGame {
    [Header("Sequence Display")]
    [SerializeField] private Transform sequenceContainer;

    // 0=Red, 1=Blue, 2=Yellow, 3=Green
    [Header("Sprites")]
    [SerializeField] private Transform redSpritePrefab;
    [SerializeField] private Transform blueSpritePrefab;
    [SerializeField] private Transform yellowSpritePrefab;
    [SerializeField] private Transform greenSpritePrefab;

    // 0=Red, 1=Blue, 2=Yellow, 3=Green
    [Header("Button")]
    [SerializeField] private Button redButton;
    [SerializeField] private Button blueButton;
    [SerializeField] private Button yellowButton;
    [SerializeField] private Button greenButton;

    private readonly List<int> sequence = new();
    private int progress;

    private void Awake() {
        progress = 0;

        redButton.onClick.AddListener(() => {
            PressColor(0);
        });
        blueButton.onClick.AddListener(() => {
            PressColor(1);
        });
        yellowButton.onClick.AddListener(() => {
            PressColor(2);
        });
        greenButton.onClick.AddListener(() => {
            PressColor(3);
        });

        GenerateSequence();
        DisplaySequence();
    }

    private void GenerateSequence() {
        sequence.Clear();

        int length = Random.Range(3, 6); // 3, 4 or 5

        for (int i = 0; i < length; i++) {
            sequence.Add(Random.Range(0, 4));
        }

        progress = 0;
    }

    private void DisplaySequence() {
        foreach (Transform child in sequenceContainer)
            Destroy(child.gameObject);

        foreach (int color in sequence) {
            Instantiate(GetPrefab(color), sequenceContainer);
        }
    }

    private Transform GetPrefab(int color) {
        return color switch {
            0 => redSpritePrefab,
            1 => blueSpritePrefab,
            2 => yellowSpritePrefab,
            3 => greenSpritePrefab,
            _ => null
        };
    }

    public void PressColor(int color) {
        if (color == sequence[progress]) {
            SoundManager.Instance.PlayClickPositive();
            progress++;

            if (progress >= sequence.Count) {
                Complete();
            }
        } else {
            SoundManager.Instance.PlayClickNegative();
            progress = 0;
        }
    }
}
