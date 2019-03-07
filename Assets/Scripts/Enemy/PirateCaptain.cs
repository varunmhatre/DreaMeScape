using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PirateCaptain : MonoBehaviour
{
    bool isApplicationQuitting = false;
    // Start is called before the first frame update
    private void OnDestroy()
    {
        if (isApplicationQuitting)
            return;
        SceneTransition.GoFade("MainMenu");
    }

    void OnApplicationQuit()
    {
        isApplicationQuitting = true;
    }
}
