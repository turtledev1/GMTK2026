using UnityEngine;
using UnityEngine.Playables;

public class EndingCutscene : MonoBehaviour {

    private PlayableDirector playableDirector;

    private void Awake() {
        playableDirector = GetComponent<PlayableDirector>();
        playableDirector.stopped += OnTimelineFinished;
        gameObject.SetActive(false);
    }

    private void OnTimelineFinished(PlayableDirector director) {
        SceneLoader.Load(SceneLoader.Scene.MainMenu);
    }
}
