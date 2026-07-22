using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneLoader {

    public enum Scene {
        MainMenu,
        LoadingScene,
    }

    private static Scene targetScene;

    public static void Load(Scene targetScene) {
        Debug.Log(targetScene.ToString());
        SceneLoader.targetScene = targetScene;

        SceneManager.LoadScene(Scene.LoadingScene.ToString());
    }

    public static void SceneLoaderCallback() {
        Debug.Log("Callback to " + SceneLoader.targetScene.ToString());
        SceneManager.LoadScene(targetScene.ToString());
    }
}
