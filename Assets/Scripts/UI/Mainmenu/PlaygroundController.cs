using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class PlaygroundController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
   // [SerializeField]
   // private Button[] buttonSize;
    public static bool isMouseover;
    public Sprite onSprite; 
    public Sprite offSprite;

    [SerializeField] private int backgroundMoveId;
    private float currSetLocation;
    private bool onJourney;
    private float journeySpeed;

    // the parallax objects and their starting positions
    [SerializeField] public GameObject[] parallaxObjects;
    public Vector3[] startingPositions;
    public float parallaxMultiplier;

    [SerializeField]
    private string buttonText;

    private float timeCount = 0.0f;
    private float flipSpeed = 0.0f;

    [SerializeField] public GameObject backgroundObj;

    // Start is called before the first frame update
    void Start()
    {
        journeySpeed = 20.0f;
        onJourney = false;
        parallaxMultiplier = 0.9f;
        startingPositions = new Vector3[1];
        startingPositions[0] = backgroundObj.transform.position;
        currSetLocation = startingPositions[0].x;
        isMouseover = false; 
        /*
        for (int i = 0; i < parallaxObjects.Length; i++)
        {
            startingPositions[i] = parallaxObjects[i].transform.position;
        }
        */
    }

    void Update()
    {

        if (onJourney)
        {
            EaseToSetLocation(backgroundObj);
        }
        //ApplyParallax(parallaxObjects, startingPositions);
    }

    public void OnPointerEnter(PointerEventData eventData)
    { 
        transform.GetComponent<Image>().sprite = onSprite;
        Vector3 newPosition = transform.position;
        transform.position = newPosition;
        SetBackgroundToId(backgroundObj, backgroundMoveId);
        onJourney = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.GetComponent<Image>().sprite = offSprite;
        Vector3 newPosition = transform.position;
        transform.position = newPosition;
        //SetBackgroundToDefault(backgroundObj);
    }
    
    public void ApplyParallax(GameObject[] objs, Vector3[] positions)
    {
        Vector3 tempPosition = new Vector3(0.0f, 0.0f, 0.0f);
        for (int i = 0; i < objs.Length; i++)
        {
            float mousePos = Input.mousePosition.x;
            if (mousePos < 0.0f)
            {
                mousePos = 0.0f;
            }
            tempPosition = new Vector3(-mousePos * parallaxMultiplier + positions[i].x, positions[i].y, 0.0f);
            objs[i].transform.position = tempPosition;
        }
    }

    public void SetBackgroundToId(GameObject obj, int id)
    {
        if (id == 1)
        {
            Vector3 moveResult = startingPositions[0] - new Vector3(0.0f, 0.0f, 0.0f);
            currSetLocation = moveResult.x;
        }
        else if (id == 2)
        {
            Vector3 moveResult = startingPositions[0] - new Vector3(600.0f, 0.0f, 0.0f);
            currSetLocation = moveResult.x;
        }
        else if (id == 3)
        {
            Vector3 moveResult = startingPositions[0] - new Vector3(1300.0f, 0.0f, 0.0f);
            currSetLocation = moveResult.x;
        }
        else if (id == 4)
        {
            Vector3 moveResult = startingPositions[0] - new Vector3(2000.0f, 0.0f, 0.0f);
            currSetLocation = moveResult.x;
        }
    }

    public void SetBackgroundToDefault(GameObject obj)
    {
        obj.transform.position = startingPositions[0];
    }

    public void EaseToSetLocation(GameObject obj)
    {
        Vector3 goalPosition = new Vector3(currSetLocation, startingPositions[0].y, startingPositions[0].z);
        Vector3 currentPosition = obj.transform.position;

        if (goalPosition == currentPosition)
        {
            onJourney = false;
        }

        Vector3 moveGoal = goalPosition - currentPosition;

        if (moveGoal.magnitude <= 10.0f)
        {
            obj.transform.position = goalPosition;
            onJourney = false;
        }

        Vector3 movement = moveGoal.normalized;
        obj.transform.position += movement * journeySpeed;
    }
}
