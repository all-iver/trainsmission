using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndConditions : MonoBehaviour
{
    public string ShellSceneName;

    public void EndGame_Lose()
    {
        GotoShell();
    }

    public void EndGame_Win()
    {
        GotoShell();
    }

    private void GotoShell()
    {
        SceneManager.LoadSceneAsync(ShellSceneName);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
            GotoShell();
    }
}
