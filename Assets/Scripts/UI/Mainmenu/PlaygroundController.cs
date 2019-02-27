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

    // the parallax objects and their starting positions
    [SerializeField] public GameObject[] parallaxObjects;
    public Vector3[] startingPositions;
    public float parallaxMultiplier;

    [SerializeField]
    private string buttonText;
    //public Transform target;
    //public Transform to;

    private float timeCount = 0.0f;
    private float flipSpeed = 0.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        parallaxMultiplier = 0.9f;
        startingPositions = new Vector3[parallaxObjects.Length];
        isMouseover = false; 
        for (int i = 0; i < parallaxObjects.Length; i++)
        {
            startingPositions[i] = parallaxObjects[i].transform.position;
        }
    }

    void Update()
    {
        ApplyParallax(parallaxObjects, startingPositions);
    }

    public void OnPointerEnter(PointerEventData eventData)
    { 
        transform.GetComponent<Image>().sprite = onSprite;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.GetComponent<Image>().sprite = offSprite; 
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
}
