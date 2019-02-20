using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonSystem : MonoBehaviour
{

    private bool mouseClick;

    private void Update()
    {
        if (mouseClick)
        {
            if (CannonStaticVariables.isCannonSelected)
            {
                AttackEnemy();
                CannonStaticVariables.CannonUnSelected();
            }
            SelectCannon();

            MouseClickToggleCannon();
        }
    }

    public void MouseClickToggleCannon()
    {
        mouseClick = !mouseClick;
    }

    void SelectCannon()
    {
        RaycastHit hit = RaycastManager.GetRaycastHitForTag("Cannon");
        if (hit.transform != null && hit.transform.GetComponent<CannonScript>().isChargeLeft)
        {
            if (hit.transform.GetComponent<CannonRadius>().CheckIfPlayerAround())
            {
                CannonStaticVariables.CannonSelected(hit.transform);
            }
        }
    }

    void AttackEnemy()
    {
        RaycastHit hit = RaycastManager.GetRaycastHitForTag("Enemy");
        if (hit.transform != null)
        {
            UnitCoordinates enemySpot = hit.transform.GetComponent<UnitCoordinates>();
            UnitCoordinates cannonSpot = CannonStaticVariables.selectedCannon.GetComponent<UnitCoordinates>();
            if ((enemySpot.x >= (cannonSpot.x - CannonStaticVariables.cannonRadius) &&
                    enemySpot.x <= (cannonSpot.x + CannonStaticVariables.cannonRadius)) &&
                (enemySpot.y >= (cannonSpot.y - CannonStaticVariables.cannonRadius) &&
                    enemySpot.y <= (cannonSpot.y + CannonStaticVariables.cannonRadius)))
            {
                CannonStaticVariables.selectedCannon.Attack();
                Destroy(hit.transform.gameObject);
            }
        }
    }
}