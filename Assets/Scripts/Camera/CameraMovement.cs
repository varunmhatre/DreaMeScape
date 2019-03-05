using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

    [SerializeField] private float cameraMoveSpeed;

    [SerializeField] private GameObject[] cameraViews;
    private GameObject currentCamera;
    private GameObject prevCamera;
    private int currentCameraIndex;
    private float portionOfJourney;
    private bool moving;
    [SerializeField] private float journeySpeed;

    [SerializeField] private float horizCap;
    [SerializeField] private float vertCap;
    private float currHorizVal;
    private float currVertVal;

    private float zoomedInAmount;

    [SerializeField] private float zoomOutMin;
    [SerializeField] private float zoomInMax;
    [SerializeField] private float scrollSpeed;

	// Use this for initialization
	void Start()
    {
        zoomedInAmount = 0.0f;
        moving = false;
        portionOfJourney = 0.0f;
        currentCameraIndex = 0;
        currentCamera = cameraViews[0];
        prevCamera = currentCamera;
        currHorizVal = 0.0f;
        currVertVal = 0.0f;
	}
	
	// Update is called once per frame
	void Update()
    {

        //check to see if you are moving the camera up, down, left, or right
        MoveCamera(Input.GetAxis("SecondaryCommandHoriz"), Input.GetAxis("SecondaryCommandVert"));
        ZoomCamera(Input.GetAxis("Mouse Scrollwheel"), zoomInMax, zoomOutMin);
        SwapCameras(Input.GetKeyDown(KeyCode.L));
        AdjustCameraValues(currentCamera, prevCamera);
    }

    //takes in input values and moves the camera based on the values
    //positive right if moving right, negative if moving left
    public void MoveCamera(float right, float up)
    {
        if (right > 0 && currHorizVal < horizCap)
        {
            for (int i = 0; i < cameraViews.Length; i++)
            {
                cameraViews[i].transform.position += cameraViews[i].transform.right * cameraMoveSpeed * Time.deltaTime;
            }
            transform.position += transform.right * cameraMoveSpeed * Time.deltaTime;
            currHorizVal += transform.right.magnitude * cameraMoveSpeed * Time.deltaTime;
        }
        else if (right < 0 && currHorizVal > - horizCap)
        {
            for (int i = 0; i < cameraViews.Length; i++)
            {
                cameraViews[i].transform.position -= cameraViews[i].transform.right * cameraMoveSpeed * Time.deltaTime;
            }
            transform.position -= transform.right * cameraMoveSpeed * Time.deltaTime;
            currHorizVal -= transform.right.magnitude * cameraMoveSpeed * Time.deltaTime;
        }

        if (up > 0 && currVertVal < vertCap)
        {
            for (int i = 0; i < cameraViews.Length; i++)
            {
                cameraViews[i].transform.position += new Vector3(0.0f, 0.0f, 1.0f) * cameraMoveSpeed * Time.deltaTime;
            }
            transform.position += new Vector3(0.0f, 0.0f, 1.0f) * cameraMoveSpeed * Time.deltaTime;
            currVertVal += cameraMoveSpeed * Time.deltaTime;
        }
        else if (up < 0 && currVertVal > -vertCap)
        {
            for (int i = 0; i < cameraViews.Length; i++)
            {
                cameraViews[i].transform.position -= new Vector3(0.0f, 0.0f, 1.0f) * cameraMoveSpeed * Time.deltaTime;
            }
            transform.position -= new Vector3(0.0f, 0.0f, 1.0f) * cameraMoveSpeed * Time.deltaTime;
            currVertVal -= cameraMoveSpeed * Time.deltaTime;
        }
    }

    //zoom the camera when adjusting the scrollwheel
    public void ZoomCamera(float mouseScroll, float min, float max)
    {
        if (gameObject.GetComponent<Camera>() != null)
        {
            Camera obtainedCam = gameObject.GetComponent<Camera>();
            /*
            if (obtainedCam.orthographicSize)
            {
                if (mouseScroll > 0 && obtainedCam.orthographicSize > min)
                {
                    obtainedCam.orthographicSize -= scrollSpeed;
                }
                else if (mouseScroll < 0 && obtainedCam.orthographicSize < max)
                {
                    obtainedCam.orthographicSize += scrollSpeed;
                }
            }
            */
            if (mouseScroll > 0 && zoomedInAmount <= zoomInMax)
            {
                obtainedCam.transform.position += gameObject.transform.forward * scrollSpeed;
                for (int i = 0; i < cameraViews.Length; i++)
                {
                    cameraViews[i].transform.position += gameObject.transform.forward * scrollSpeed;
                }
                zoomedInAmount += gameObject.transform.forward.magnitude * scrollSpeed;
            }
            else if (mouseScroll < 0 && zoomedInAmount >= zoomOutMin)
            {
                obtainedCam.transform.position -= gameObject.transform.forward * scrollSpeed;
                for (int i = 0; i < cameraViews.Length; i++)
                {
                    cameraViews[i].transform.position -= gameObject.transform.forward * scrollSpeed;
                }
                zoomedInAmount -= gameObject.transform.forward.magnitude * scrollSpeed;
            }
        }
        Debug.Log(zoomedInAmount);
    }

    public void SwapCameras(float clicked)
    {
        if (clicked > 0)
        {
            prevCamera = cameraViews[currentCameraIndex];
            currentCameraIndex++;
            if (currentCameraIndex >= cameraViews.Length)
            {
                currentCameraIndex = 0;
            }
            currentCamera = cameraViews[currentCameraIndex];
            moving = true;
        }
    }

    public void SwapCameras(bool clicked)
    {
        if (clicked)
        {
            prevCamera = cameraViews[currentCameraIndex];
            currentCameraIndex++;
            if (currentCameraIndex >= cameraViews.Length)
            {
                currentCameraIndex = 0;
            }
            currentCamera = cameraViews[currentCameraIndex];
            moving = true;
        }
    }

    public void AdjustCameraValues(GameObject newObjToTakeValues, GameObject prevObjToTakeValues)
    {
        if (moving)
        {
            if (transform.position != newObjToTakeValues.transform.position || transform.rotation != newObjToTakeValues.transform.rotation)
            {
                transform.position = Vector3.Lerp(prevObjToTakeValues.transform.position, newObjToTakeValues.transform.position, portionOfJourney);
                transform.rotation = Quaternion.Lerp(prevObjToTakeValues.transform.rotation, newObjToTakeValues.transform.rotation, portionOfJourney);
                portionOfJourney += Time.deltaTime * journeySpeed;
            }
            else
            {
                moving = false;
                portionOfJourney = 0.0f;
            }
        }
    }
}
