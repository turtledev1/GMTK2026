using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class EndingManager : MonoBehaviour {
    [SerializeField] private EndingCutscene successTimeline;
    [SerializeField] private EndingCutscene engineTimeline;
    [SerializeField] private EndingCutscene fuelTimeline;
    [SerializeField] private EndingCutscene navigationTimeline;
    [SerializeField] private EndingCutscene lifeSupportTimeline;
    [SerializeField] private EndingCutscene communicationsTimeline;

    private void Start() {
        var brokenSystems = UpAndRunningSystems.GetPermanentlyBrokenSystemsByPriority();

        if (brokenSystems.Count == 0) {
            successTimeline.gameObject.SetActive(true);
            return;
        }

        switch (brokenSystems[0].Type) {
            case RocketSystemType.Engine:
                engineTimeline.gameObject.SetActive(true);
                break;

            case RocketSystemType.Fuel:
                fuelTimeline.gameObject.SetActive(true);
                break;

            case RocketSystemType.Navigation:
                navigationTimeline.gameObject.SetActive(true);
                break;

            case RocketSystemType.LifeSupport:
                lifeSupportTimeline.gameObject.SetActive(true);
                break;

            case RocketSystemType.Communications:
                communicationsTimeline.gameObject.SetActive(true);
                break;
        }
    }
}
