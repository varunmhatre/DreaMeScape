using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

    public bool pirateLock;
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

    [SerializeField] private Vector3[] positions;
    [SerializeField] private float[] times;
    private float timer;
    private int cameraLocNum;
    private Vector3 goalLocation;
    [SerializeField] private float panningSpeed;

    private bool firstTime;

	// Use this for initialization
	void Start()
    {
        firstTime = true;
        positions = new Vector3[3];
        times = new float[3];

        positions[0] = transform.position;
        positions[1] = transform.position + transform.right * 30.0f;
        positions[2] = transform.position;

        times[0] = 2.5f;
        times[1] = 8.0f;
        times[2] = 15.0f;
        goalLocation = transform.position;
        cameraLocNum = 0;
        timer = 0.0f;
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
        if (!pirateLock && DialoguePanelManager.playerControlsUnlocked)
        {
            if (firstTime)
            {
                transform.position = positions[0];
                firstTime = false;
            }
            if (!TutorialCards.isTutorialRunning)
            {
                //check to see if you are moving the camera up, down, left, or right
                MoveCamera(Input.GetAxis("SecondaryCommandHoriz"), Input.GetAxis("SecondaryCommandVert"));
                MoveCamera(Input.GetAxis("CameraCommandHoriz"), Input.GetAxis("CameraCommandVert"));
                ZoomCamera(Input.GetAxis("Mouse Scrollwheel"), zoomInMax, zoomOutMin);
                SwapCameras(Input.GetKeyDown(KeyCode.L));
            }
        } 
        else if (firstTime == true)
        {
            PirateShipMoveSetup(positions, times);
        }
        AdjustCameraValues(currentCamera, prevCamera);
    }

    public void SetCameraIndex(int index)
    {
        prevCamera = cameraViews[currentCameraIndex];
        currentCameraIndex = index;
        currentCamera = cameraViews[currentCameraIndex];
        moving = true;
    }

    public int GetCameraIndex()
    {
        return currentCameraIndex;
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
        //Debug.Log(zoomedInAmount);
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

    public void PirateShipMoveSetup(Vector3[] locations, float[] times)
    {
        goalLocation = locations[cameraLocNum];
        if (timer >= times[cameraLocNum])
        {
            timer = 0.0f;
            cameraLocNum++;
        }

        if (transform.position != goalLocation)
        {
            Vector3 goVector = goalLocation - transform.position;
            Vector3 goWay = goVector.normalized;
            if (goVector.magnitude <= 2.5f)
            {
                transform.position = goalLocation;
            }
            transform.position += goWay * panningSpeed * Time.deltaTime;
        }

        timer += Time.deltaTime;
    }
}
