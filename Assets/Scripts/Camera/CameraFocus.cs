using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFocus : MonoBehaviour
{
    public Transform pirate;

    Vector3 offset;

    Vector3 playerCameraPosition;
    Vector3 initialPosition;
    Vector3 finalPosition;

    float startZoom;
    float endZoom;

    float playerCameraZoom;
    float timerForMovement;
    float timerForZoom;

    bool focusSet;
    bool isZoomRequired;
    bool isMovementRequired;

    Camera camera;
    public bool getoffset;

    private void Start()
    {
        camera = Camera.main;
        focusSet = false;
        isZoomRequired = false;
        isMovementRequired = false;
        timerForMovement = 0.0f;
        timerForZoom = 0.0f;

        //Temp
        offset = new Vector3(-0.2336006f, 22.02439f, -38.58832f);
    }

    void CalculateOffset(Transform pirate)
    {

    }

    public void ResetCamera()
    {
        pirate = null;
        focusSet = false;
        startZoom = endZoom;
        endZoom = playerCameraZoom;
        if (startZoom != endZoom)
        {
            isZoomRequired = true;
        }

        finalPosition = playerCameraPosition;
        initialPosition = transform.position;
        if (initialPosition != finalPosition)
        {
            isMovementRequired = true;
        }
    }

    public void Initiate(Transform pirate)
    {
        CalculateOffset(pirate);
        playerCameraPosition = transform.position;
        playerCameraZoom = camera.orthographicSize;
        startZoom = playerCameraZoom;
        endZoom = 3.7f;
        if (endZoom != startZoom)
        {
            isZoomRequired = true;
        }
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
        //Zoom
        if (isZoomRequired)
        {
            timerForZoom += Time.deltaTime;
            camera.orthographicSize = Mathf.Lerp(startZoom, endZoom, timerForZoom);
            if (timerForZoom >= 1.0f)
            {
                camera.orthographicSize = endZoom;
                timerForZoom = 0.0f;
                isZoomRequired = false;
            }
        }
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