using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonSystem : MonoBehaviour
{
    CannonCrossbarController crossbarController;
    Transform selectedCannon;

    public void CannonSelected(Transform cannon)
    {
        selectedCannon = cannon;
        crossbarController.EnableCrossBar();
        cannon.GetComponent<CannonRadius>().HighlightGrids();
        CannonStaticVariables.isCannonSelected = true;
    }

    public void CannonUnSelected()
    {
        crossbarController.EnableCrossBar();
        CannonStaticVariables.isCannonSelected = false;
        selectedCannon.GetComponent<CannonRadius>().RemoveHighlights();
        selectedCannon = null;
    }
}