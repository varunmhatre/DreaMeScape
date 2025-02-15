﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonSystem : MonoBehaviour
{
    private void Update()
    {
        if (RaycastManager.leftClicked)
        {
            if (CannonStaticVariables.isCannonSelected)
            {
                AttackEnemy();
                CannonStaticVariables.CannonUnSelected();
            }
            else
            {
                SelectCannon();
            }
        }
    }

    void SelectCannon()
    {
        RaycastHit hit = RaycastManager.GetRaycastHitForTag("Cannon");
        if (hit.transform != null && hit.transform.GetComponent<CannonScript>().isChargeLeft)
        {
            if (hit.transform.GetComponent<CannonRadius>().CheckIfPlayerAround() && !PlayerControls.selectedUnit && !CharacterAbility.inSelectionMode)
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

                //We can make this a number
                hit.transform.GetComponent<Stats>().TakeDamage(6);
                hit.transform.GetComponent<Stats>().CheckDeath();
            }
        }
    }
}