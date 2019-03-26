using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CursorHandler : MonoBehaviour  //, IPointerEnterHandler, IPointerExitHandler
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


    private void OnMouseEnter()
    {
        //Condition to check anything else is in front of it.

        /*if (AdjacencyHandler.NumPlayerCharactersAround(gameObject, 1) >= 1 &&
            !CannonStaticVariables.isCannonSelected && PlayerControls.selectedUnit != null &&
                AdjacencyHandler.CompareAdjacency(gameObject, PlayerControls.selectedUnit.gameObject, 1))
        {
            
        }*/

      

        customCursor.EnableCrossBar();
    }

    private void OnMouseExit()
    {
       
        customCursor.DisableCrossBar();
    }

   /* public void OnPointerEnter(PointerEventData eventData)
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
    }*/
 }
