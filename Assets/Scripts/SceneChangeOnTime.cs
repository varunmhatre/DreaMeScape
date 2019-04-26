using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneChangeOnTime : MonoBehaviour
{
    [SerializeField] public SceneTransition sceneTransition;
    private float timer;

    [SerializeField] private GameObject skipIndicatorObj;

    private bool setupSkip;

    // Start is called before the first frame update
    void Start()
    {
        setupSkip = false;
        timer = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {

        if (timer >= 25.0f)
        {
            sceneTransition.ChangeScene("TutorialScene");
            //sceneTransition.ChangeScene("PirateshipScene");
        }

        CheckForSkip();
        ShowSkipButton(setupSkip);

        timer += Time.deltaTime;
    }


    public void CheckForSkip()
    {
        if (Input.GetMouseButtonDown(0) && setupSkip)
        {
            sceneTransition.ChangeScene("TutorialScene");
            //sceneTransition.ChangeScene("PirateshipScene");
        }

        if (Input.GetMouseButtonDown(0))
        {
            setupSkip = true;
        }
    }

    public void ShowSkipButton(bool isShowing)
    {
        if (isShowing)
        {
            skipIndicatorObj.SetActive(true);
        }
    }
}
