using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CursorHandler : MonoBehaviour  //, IPointerEnterHandler, IPointerExitHandler
{

    CursorTexture customCursor;
    CannonScript cannon;
    // Start is called before the first frame update
    void Start()
    {
        customCursor = GetComponent<CursorTexture>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnMouseEnter()
    {       
       if(!CannonStaticVariables.isCannonSelected && RaycastManager.leftClicked)
        {
            customCursor.EnableCrossBar();
        }      
    }

    private void OnMouseExit()
    {
        if (!CannonStaticVariables.isCannonSelected && RaycastManager.leftClicked)
        {
            customCursor.DisableCrossBar();
        }
    }
 }
