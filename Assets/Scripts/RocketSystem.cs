using Unity.VisualScripting;
using UnityEngine;

public class RocketSystem : MonoBehaviour {
    [SerializeField] private RepairDefinitionSO repairDefinitionSO;
    [SerializeField] private BreakingTimerUI timerUI;

    private AudioSource audioSource;

    public bool IsBroken { get; private set; }
    public bool IsPermanentlyBroken { get; private set; }
    public float TimeRemaining { get; private set; }

    private float RepairTime => repairDefinitionSO.repairTime;
    public RocketSystemType Type => repairDefinitionSO.type;
    public int Priority => repairDefinitionSO.priority;

    private void Awake() {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start() {
        FailuresManager.Instance.Register(this);
        UpAndRunningSystems.RegisterSystem(this);
    }

    public bool Interact() {
        if (!IsBroken) {
            return false;
        }
        if (IsPermanentlyBroken) {
            return false;
        }
        RepairsManagerUI.Instance.OpenRepair(this);
        return true;
    }

    public void Break() {
        if (IsBroken || IsPermanentlyBroken) {
            Debug.LogError("Trying to break a system that is already broken");
            return;
        }
        IsBroken = true;
        BrokenIndicatorsUI.Instance.ImBroken(this);
        timerUI.Initialize(this);

        TimeRemaining = RepairTime + Random.Range(-3f, 3f);
        // just in case I mess something up
        TimeRemaining = Mathf.Max(1f, TimeRemaining);

        Debug.Log($"{Type} broke! Repair within {TimeRemaining:F1}s");
    }

    public void Repair() {
        Debug.Log($"{Type} repaired!");
        IsBroken = false;
        BrokenIndicatorsUI.Instance.ImRepaired(this);
        TimeRemaining = 0f;
    }

    private void Update() {
        if (!IsBroken || IsPermanentlyBroken)
            return;

        if (!GameManager.Instance.IsGamePlaying())
            return;

        TimeRemaining -= Time.deltaTime;

        if (TimeRemaining <= 0f) {
            PermanentlyBreak();
        }
    }

    private void PermanentlyBreak() {
        IsBroken = false;
        IsPermanentlyBroken = true;
        audioSource.Play();
        BrokenIndicatorsUI.Instance.ImRepaired(this);

        Debug.Log($"{Type} permanently failed!");

        RepairsManagerUI.Instance.CloseRepair();
    }

    public RepairMiniGame GetMiniGamePrefab() {
        return repairDefinitionSO.repairMiniGame;
    }

    // For debugging
    public string GetSystemName() {
        return $"{Type}";
    }
}
