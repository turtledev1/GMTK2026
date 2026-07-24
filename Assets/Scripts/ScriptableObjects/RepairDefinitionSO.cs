using UnityEngine;

[CreateAssetMenu]
public class RepairDefinitionSO : ScriptableObject {
    public RocketSystemType type;
    public RepairMiniGame repairMiniGame;
    public float repairTime;
    public int priority;
}
