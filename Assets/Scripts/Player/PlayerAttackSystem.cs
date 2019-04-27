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
        public PlayerAttackUI attackUI;
    }

    protected override void OnUpdate()
    {
        foreach (var item in GetEntities<AttackComponents>())
        {
            if (item.stats.hasAttacked)
            {
                item.playerBase.baseSprite.sprite = item.playerBase.offVisual;
                item.attackUI.baseVisual.sprite = item.attackUI.offVisual;
            }
            else
            {
                item.playerBase.baseSprite.sprite = item.playerBase.onVisual;
                item.attackUI.baseVisual.sprite = item.attackUI.onVisual;
            }
        }
    }
}
