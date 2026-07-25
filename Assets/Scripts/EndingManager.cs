using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndingManager : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI debugText;

    private void Awake() {
        Debug.Log(UpAndRunningSystems.GetPermanentlyBrokenSystemsByPriority());
    }

    private void Start() {
        List<RocketSystem> brokenSystems = UpAndRunningSystems.GetPermanentlyBrokenSystemsByPriority();
        debugText.text = "Most important broken system is " + brokenSystems[0].GetSystemName();
    }
}
