using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour {
    [SerializeField] private Button playButton;

    private void Awake() {
        playButton.onClick.AddListener(() => {
            SceneLoader.Load(SceneLoader.Scene.Game);
        });
    }

    private void Start() {
        Debug.Log(UpAndRunningSystems.GetPermanentlyBrokenSystemsByPriority().Count);
    }
}
