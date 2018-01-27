using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BaseScene : MonoBehaviour
{
    public static BaseScene Inst { get; private set; }
    
    public string InitialSceneName;
    private Scene CurrentScene;

    private void Start()
    {
        Inst = this;
        StartCoroutine(EnterScene(InitialSceneName));
    }
    
    private IEnumerator ExitCurrentScene()
    {
        if (CurrentScene == null)
            yield break;
        var op = SceneManager.UnloadSceneAsync(CurrentScene);
        while (!op.isDone)
            yield return null;
    }

    private IEnumerator EnterScene(string sceneName)
    {
        var op = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        while (!op.isDone)
            yield return null;
        CurrentScene = SceneManager.GetSceneByName(sceneName);
        yield break;
    }

    private IEnumerator TransitionToScene(string sceneName)
    {
        yield return ExitCurrentScene();
        yield return EnterScene(sceneName);
    }

    public void GotoScene(string sceneName)
    {
        StartCoroutine(TransitionToScene(sceneName));
    }
}
