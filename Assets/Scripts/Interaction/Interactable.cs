using UnityEngine;

public class Interactable : MonoBehaviour {
    [SerializeField] private RepairDefinitionSO repairDefinitionSO;

    public void Interact() {
        RepairsManagerUI.Instance.OpenRepair(this);
    }

    public RepairMiniGame GetMiniGamePrefab() {
        return repairDefinitionSO.repairMiniGame;
    }
}
