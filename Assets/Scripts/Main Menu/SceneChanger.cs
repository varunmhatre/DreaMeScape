using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneChanger : MonoBehaviour {

	public void LoadNextScene()
    {
        int currentSceneInd = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneInd + 1);
    }
}
