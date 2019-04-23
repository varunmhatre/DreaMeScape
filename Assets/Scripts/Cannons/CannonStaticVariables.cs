using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonStaticVariables : MonoBehaviour
{
    public static bool isCannonSelected;
    public static bool clearCannonSelection;
    public static int cannonRadius;
    public static CustomCursorTexture crossbarController;
    public static CannonScript selectedCannon;
    public static RaycastManager raycastManager;

    // Start is called before the first frame update
    void Start()
    {
        cannonRadius = 3;
        crossbarController = GetComponent<CustomCursorTexture>() ;
        raycastManager = GameObject.Find("Managers").GetComponent<RaycastManager>();
    }

    public static void CannonSelected(Transform cannon)
    {
        selectedCannon = cannon.GetComponent<CannonScript>();
        selectedCannon.isThisCannonSelected = true;
        crossbarController.EnableCrossBar();
        cannon.GetComponent<CannonRadius>().HighlightGrids();
        CannonStaticVariables.isCannonSelected = true;
    }

    public static void CannonUnSelected()
    {
        selectedCannon.Disengage();
        crossbarController.DisableCrossBar();
        clearCannonSelection = true;
        selectedCannon.isThisCannonSelected = false;
        selectedCannon.GetComponent<CannonRadius>().RemoveHighlights();
        selectedCannon = null;
    }
}
