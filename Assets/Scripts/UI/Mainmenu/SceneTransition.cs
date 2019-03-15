﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public void ChangeScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void TurnOn(GameObject on)
    {
        on.SetActive(true);
    }

    public void TurnOff(GameObject off)
    {
        off.SetActive(false);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
    public static void GoFade(string scene)
    {
        Initiate.Fade(scene, Color.white, 1.0f);
    }

    public void ExitLevel(string scene)
    {
        Time.timeScale = 1;
        DialoguePanelManager.playerControlsUnlocked = false;
        DialoguePanelManager.countDialogueLength = 0;
        SceneManager.LoadScene(scene);
    }
}
