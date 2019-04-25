using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFocus : MonoBehaviour
{
    public Transform pirate;
    CameraMovement cameraMov;
    Vector3 offset;

    Vector3 playerCameraPosition;
    Vector3 initialPosition;
    Vector3 finalPosition;

    float timerForMovement;

    bool focusSet;
    bool isMovementRequired;

    Camera camera;

    private void Start()
    {
        cameraMov = GetComponent<CameraMovement>();
        camera = Camera.main;
        focusSet = false;
        isMovementRequired = false;
        timerForMovement = 0.0f;
    }

    void CalculateOffset(Transform pirate)
    {
        //float lambda = (pirate.position.y - transform.position.y) / transform.forward.y;
        //offset = Vector3.Normalize(new Vector3(transform.forward.x * lambda, pirate.position.y - transform.position.y, transform.forward.z * lambda) * -1.0f) * 8.0f;
        offset = transform.forward * -8.0f;
    }
    
    public void ResetCamera()
    {
        pirate = null;
        initialPosition = transform.position;
        finalPosition = playerCameraPosition;
        if (initialPosition != finalPosition)
        {
            isMovementRequired = true;
        }

        IEnumerator cor = DelayedCameraReset();
        StartCoroutine(cor);
    }

    IEnumerator DelayedCameraReset()
    {
        yield return new WaitForSeconds(1.0f);
        cameraMov.pirateLock = false;
    }

    public void Initiate(Transform pirate)
    {
        playerCameraPosition = transform.position;
        cameraMov.pirateLock = true;
        CalculateOffset(pirate);
        ChangePirate(pirate);
    }

    public void ChangePirate(Transform pirate)
    {
        this.pirate = pirate;
        finalPosition = pirate.position + offset;
        initialPosition = transform.position;
        if (initialPosition != finalPosition)
        {
            isMovementRequired = true;
            focusSet = false;
        }
    }

    void Update()
    {
        //Setting offset
        if (isMovementRequired)
        {
            timerForMovement += Time.deltaTime;
            transform.position = Vector3.Lerp(initialPosition, finalPosition, timerForMovement);
            if (timerForMovement >= 1.0f)
            {
                transform.position = finalPosition;
                timerForMovement = 0.0f;
                isMovementRequired = false;
                focusSet = true;
            }
        }

        if (focusSet && pirate)
        {
            //Position
            transform.position = pirate.position + offset;
        }
    }
}