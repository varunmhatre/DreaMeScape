using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

public class PirateAI : ComponentSystem
{
    private struct pirates
    {
        public PirateCrew pieceData;
        public Transform transform;
    }

    protected override void OnUpdate()
    {
        foreach (var pirate in GetEntities<pirates>())
        {

        }
    }
}
