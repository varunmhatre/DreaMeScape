using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PirateCaptain : MonoBehaviour
{
    public static bool isPirateDefeated = false;
    // Start is called before the first frame update
    private void OnDestroy()
    {
        if (isPirateDefeated)
        {
            SceneTransition.GoFade("WinSCene");
        }            
    }
}
