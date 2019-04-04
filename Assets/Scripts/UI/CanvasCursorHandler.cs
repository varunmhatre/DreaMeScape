using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CanvasCursorHandler : MonoBehaviour,IPointerEnterHandler, IPointerExitHandler
{
    CursorTexture customCursor;
    // Start is called before the first frame update
    void Start()
    {
        customCursor = GetComponent<CursorTexture>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (TutorialCards.isTutorialRunning)
        {
            customCursor.EnableCrossBar();
        }
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        if (TutorialCards.isTutorialRunning)
        {
            customCursor.DisableCrossBar();
        }
    }
}
