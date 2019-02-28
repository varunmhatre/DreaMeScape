using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

public class ConfusedSystem : ComponentSystem
{
    private struct ConfusedEnemies
    {
        public Transform transform;
        public ContinuousRotation continuousRotation;
    }

    protected override void OnUpdate()
    {
        foreach (var item in GetEntities<ConfusedEnemies>())
        {
            item.transform.Rotate(0, 0, 60.0f * Time.deltaTime);
        }
    }
}