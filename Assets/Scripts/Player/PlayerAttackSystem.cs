using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

public class PlayerAttackSystem : ComponentSystem
{
    public struct AttackComponents
    {
        public Stats stats;
        public PlayerBase playerBase;
    }

    protected override void OnUpdate()
    {
        foreach (var item in GetEntities<AttackComponents>())
        {
            if (item.stats.hasAttacked)
            {
                item.playerBase.baseSprite.sprite = item.playerBase.offVisual;
            }
            else
            {
                item.playerBase.baseSprite.sprite = item.playerBase.onVisual;
            }
        }
    }
}
