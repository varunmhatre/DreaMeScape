using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneChangeOnTime : MonoBehaviour
{
    [SerializeField] public SceneTransition sceneTransition;
    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {

        if (timer >= 25.0f)
        {
            sceneTransition.ChangeScene("MainMenu");
        }

        timer += Time.deltaTime;
    }
}
