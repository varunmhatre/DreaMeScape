using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

public class FaceCamera : ComponentSystem
{
    private struct units
    {
        public UnitRotation unitRotation;
    }

    protected override void OnUpdate()
    {
        Quaternion cameraRotation = Camera.main.transform.rotation;
        foreach (var item in GetEntities<units>())
        {
            item.unitRotation.SetLocalRotation(cameraRotation);
        }
    }
}
