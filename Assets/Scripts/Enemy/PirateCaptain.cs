using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PirateCaptain : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnDestroy()
    {
        if (!Application.isEditor)
            SceneManager.LoadScene(0);
    }
}
