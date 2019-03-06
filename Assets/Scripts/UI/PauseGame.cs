using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour
{
    private bool isPaused;

    [SerializeField] private GameObject pauseBackground;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
        }
    }
    public void PauseTheGame()
    {       
        isPaused = !isPaused;

        if(isPaused)
        {
            Time.timeScale = 0;
            pauseBackground.SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
            pauseBackground.SetActive(false);
        } 
    }
}
