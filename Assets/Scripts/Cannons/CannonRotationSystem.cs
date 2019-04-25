using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

public class CannonRotationSystem : ComponentSystem
{
    private struct CannonBases
    {
        public Transform transform;
        public CannonRotation cannonRotation;
    }

    private struct CannonBaseCol
    {
        public CannonRadius cannonRadius;
        public CannonScript cannonScript;
        public CannonBaseColor cannonBaseColor;
    }

    protected override void OnUpdate()
    {
        Vector3 subVec = Vector3.zero;
        foreach (var item in GetEntities<CannonBases>())
        {
            item.transform.eulerAngles = item.cannonRotation.initialRot;
            item.transform.position = item.cannonRotation.initialPos;
        }

        foreach (var item in GetEntities<CannonBaseCol>())
        {
            if (item.cannonRadius.CheckIfPlayerAround() && item.cannonScript.charge > 0)
            {
                item.cannonBaseColor.baseVisual.sprite = item.cannonBaseColor.onVisual;
            }
            else
            {
                item.cannonBaseColor.baseVisual.sprite = item.cannonBaseColor.offVisual;
            }
        }
    }
}
