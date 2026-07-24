using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FailuresManager : MonoBehaviour {

    public static FailuresManager Instance { get; private set; }

    [Header("Failure Frequency")]
    [SerializeField] private float startMinDelay = 18f;
    [SerializeField] private float startMaxDelay = 25f;

    [SerializeField] private float endMinDelay = 5f;
    [SerializeField] private float endMaxDelay = 10f;

    private readonly List<RocketSystem> systems = new();

    private RocketSystem lastBroken;

    private void Awake() {
        Instance = this;
    }

    private void Start() {
        StartCoroutine(FailureLoop());
    }

    public void Register(RocketSystem system) {
        if (!systems.Contains(system)) {
            Debug.Log($"Registering system: {system.GetSystemName()}");
            systems.Add(system);
        }
    }

    public void Unregister(RocketSystem system) {
        systems.Remove(system);
    }

    private IEnumerator FailureLoop() {
        while (true) {
            float delay = GetNextDelay();

            while (delay > 0f) {
                if (GameManager.Instance.IsGamePlaying())
                    delay -= Time.deltaTime;

                yield return null;
            }

            BreakRandomSystem();
        }
    }

    private float GetNextDelay() {
        float progress = 1f - (GameManager.Instance.GetTime() / GameManager.Instance.GetGameDuration());

        float min = Mathf.Lerp(startMinDelay, endMinDelay, progress);
        float max = Mathf.Lerp(startMaxDelay, endMaxDelay, progress);

        return Random.Range(min, max);
    }

    private void BreakRandomSystem() {
        List<RocketSystem> candidates = systems
            .Where(s => !s.IsBroken && s != lastBroken)
            .ToList();

        if (candidates.Count == 0) {
            candidates = systems
                .Where(s => !s.IsBroken)
                .ToList();
        }

        if (candidates.Count == 0) {
            Debug.Log("All systems are broken! Cannot break any more systems.");
            return;
        }

        RocketSystem chosen = candidates[Random.Range(0, candidates.Count)];

        chosen.Break();

        lastBroken = chosen;
    }

}
