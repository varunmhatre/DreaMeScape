using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

public class FaceCamera : ComponentSystem
{
    Transform camera;
    private struct units
    {
        public UnitRotation unitRotation;
    }

    // Start is called before the first frame update
    void Start()
    {
        camera = GameObject.Find("Main Camera").transform; ;
    }

    protected override void OnUpdate()
    {
        foreach (var item in GetEntities<units>())
        {
            item.unitRotation.SetRotation(camera.rotation);
        }
    }
}
