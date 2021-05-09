using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public Button newRun;
    public Button resumeGame;
    public Button exitGame;

    Animator myAnimator;
    CanvasGroup canvasGroup;

    float activeSceond;
    public void OnQuit()
    {
        Debug.Log("1");
    }
    public void OnResume()
    {
        Debug.Log("2");
    }

    public void OnRestart()
    {
        Debug.Log("3");

    }
}
