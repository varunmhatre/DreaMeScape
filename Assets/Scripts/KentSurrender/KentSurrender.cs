using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KentSurrender : MonoBehaviour
{
    private Vector3 startingPosition;
    private Vector3 currDirection;
    private float currSpeed;
    private float timer;

    [SerializeField] private SceneTransition sceneTransition;

    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;
        currSpeed = 2.0f;
        currDirection = new Vector3(0.0f, 1.0f, 1.0f);
        timer = 0.0f;
        currDirection.Normalize();
    }

    // Update is called once per frame
    void Update()
    {
        AdjustPosition(currDirection, currSpeed);
        AdjustMovementValues();
        LookForNewScene();
        timer += Time.deltaTime;
    }

    public void AdjustPosition(Vector3 direction, float speed)
    {
        transform.localPosition += direction * speed * Time.deltaTime;
    }

    public void AdjustMovementValues()
    {
        if (timer <= 2.0f || (timer > 3.25f && timer <= 9.0f))
        {
            currSpeed = 0.0f;
        }
        else if (timer < 9.4f)
        {
            currSpeed = 2.0f;
        }
        if (timer > 2.0f && timer <= 2.55f)
        {
            currDirection += new Vector3(0.0f, -2.5f, 2.5f) * Time.deltaTime;
        }
        else if (timer > 2.55f && timer <= 2.65f)
        {
            currDirection = new Vector3(0.0f, 1.0f, 1.0f) * Time.deltaTime;
        }
        else if (timer > 2.65f && timer <= 3.25f)
        {
            currDirection += new Vector3(0.0f, -2.5f, 2.5f) * Time.deltaTime;
        }
        else if (timer > 4.0f && timer <= 5.5f)
        {
            transform.Rotate(0.0f, Time.deltaTime*100.0f, 0.0f);
        }
        else if (timer > 7.0 && timer <= 8.5f)
        {
            transform.Rotate(0.0f, -Time.deltaTime*100.0f, 0.0f);
        }
        else if (timer > 8.5f && timer <= 8.6f)
        {
            currDirection = new Vector3(0.0f, 1.5f, 1.0f) * Time.deltaTime;
        }
        else if (timer > 9.0f && timer <= 11.0f)
        {
            currDirection += new Vector3(0.0f, -4.0f, 4.0f) * Time.deltaTime;
        }

        if (timer >= 9.4f && timer <= 15.0f)
        {
            currSpeed += 10.0f * Time.deltaTime;
        }



        currDirection.Normalize();
    }

    public void LookForNewScene()
    {
        if (timer >= 16.0f)
        {
            sceneTransition.ChangeScene("WinScene");
        }
    }
}
