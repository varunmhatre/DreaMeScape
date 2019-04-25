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
        public CannonBaseColor cannonBase;
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
            if (item.cannonBase.transform.parent.GetComponent<CannonRadius>().CheckIfPlayerAround())
            {
                item.cannonBase.baseVisual.sprite = item.cannonBase.onVisual;
            }
            else
            {
                item.cannonBase.baseVisual.sprite = item.cannonBase.offVisual;
            }
        }
    }
}
