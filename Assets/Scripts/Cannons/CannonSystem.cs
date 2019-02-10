using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonSystem : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (CannonStaticVariables.isCannonSelected)
            {
                AttackEnemy();
                CannonStaticVariables.CannonUnSelected();
            }
            SelectCannon();
        }
    }

    void SelectCannon()
    {
        RaycastHit hit = CannonStaticVariables.raycastManager.getRaycastHitForTag("Cannon");
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
        RaycastHit hit = CannonStaticVariables.raycastManager.getRaycastHitForTag("Enemy");
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