using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InitialSplash : MonoBehaviour
{
    public string GameSceneName;
    public Animator Animator;

    private bool FadeInFinished;
    private bool Loading;

    public void OnFadeInFinished()
    {
        FadeInFinished = true;
    }

    public void OnFadeOutFinished()
    {
        BeginLoad();
    }

    private void BeginLoad()
    {
        Animator.SetTrigger("FadeOut");
        BaseScene.Inst.GotoScene(GameSceneName);
        Loading = true;
    }

    private void Start()
    {
        Animator.SetTrigger("FadeIn");
    }

    private void Update()
    {
        if (FadeInFinished && Input.anyKeyDown)
            Animator.SetTrigger("FadeOut");
    }
}
