using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    /* --- TODO ---
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void ChangeScene(string scene)
    {
        SceneManager.LoadScene("LoadingScene");
        StartCoroutine(LoadYourAsyncScene(scene));
    }

    public void TurnOn(GameObject on)
    {
        on.SetActive(true);
    }

    IEnumerator LoadYourAsyncScene(string scene)
    {
        yield return null;

        //Begin to load the Scene you specify
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(scene);
        //Don't let the Scene activate until you allow it to
        asyncOperation.allowSceneActivation = false;
        Debug.Log("Pro :" + asyncOperation.progress);
        //When the load is still in progress, output the Text and progress bar
        while (!asyncOperation.isDone)
        {
            // Check if the load has finished
            if (asyncOperation.progress >= 0.9f)
            {
                //Wait to you press the space key to activate the Scene
                asyncOperation.allowSceneActivation = true;
            }
            yield return null;
        }
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
    }*/

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