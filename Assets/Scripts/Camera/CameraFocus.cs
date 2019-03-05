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

    float startZoom;
    float endZoom;

    float playerCameraZoom;
    float timerForMovement;
    float timerForZoom;
    float timeToWaitForCameraChange;

    bool focusSet;
    bool isZoomRequired;
    bool isMovementRequired;

    int playerCameraIndex;

    Camera camera;

    private void Start()
    {
        cameraMov = GetComponent<CameraMovement>();
        camera = Camera.main;
        focusSet = false;
        isZoomRequired = false;
        isMovementRequired = false;
        timerForMovement = 0.0f;
        timerForZoom = 0.0f;
    }

    void CalculateOffset(Transform pirate)
    {
        Vector3 vecToTarget = transform.position - pirate.position;
        float lambda = (pirate.position.y - transform.position.y) / transform.forward.y;
        Vector3 intersection = new Vector3(transform.position.x + (transform.forward.x * lambda), pirate.position.y, transform.position.z + (transform.forward.z * lambda));
        offset = transform.position - intersection;
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

        startZoom = endZoom;
        endZoom = playerCameraZoom;
        if (startZoom != endZoom)
        {
            isZoomRequired = true;
        }
        IEnumerator cor = DelayedCameraReset();
        StartCoroutine(cor);
    }

    IEnumerator DelayedCameraReset()
    {
        yield return new WaitForSeconds(1.0f);

        cameraMov.pirateLock = false;
        //cameraMov.SetCameraIndex(playerCameraIndex);
    }

    public void Initiate(Transform pirate)
    {
        timeToWaitForCameraChange = 0.0f;
        playerCameraIndex = cameraMov.GetCameraIndex();
        //cameraMov.SetCameraIndex(0);
        if (playerCameraIndex != 0)
        {
            timeToWaitForCameraChange = 1.0f;
        }
        playerCameraPosition = transform.position;
        IEnumerator cor = DelayedInitialization(pirate);
        StartCoroutine(cor);
    }

    IEnumerator DelayedInitialization(Transform pirate)
    {
        yield return new WaitForSeconds(timeToWaitForCameraChange);
        cameraMov.pirateLock = true;
        CalculateOffset(pirate);
        
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