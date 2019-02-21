using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonStaticVariables : MonoBehaviour
{
    public static bool isCannonSelected;
    public static int cannonRadius;
    public static CannonCrossbarController crossbarController;
    public static CannonScript selectedCannon;
    public static RaycastManager raycastManager;

    // Start is called before the first frame update
    void Start()
    {
        cannonRadius = 3;
        crossbarController = GetComponent<CannonCrossbarController>() ;
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
        CannonStaticVariables.isCannonSelected = false;
        selectedCannon.isThisCannonSelected = false;
        selectedCannon.GetComponent<CannonRadius>().RemoveHighlights();
        selectedCannon = null;
    }
}
