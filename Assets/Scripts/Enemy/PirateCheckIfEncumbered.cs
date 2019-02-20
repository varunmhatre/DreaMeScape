using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

public class PirateCheckIfEncumbered : ComponentSystem
{
    // Update is called once per frame
    private struct allCharacters
    {
        public UnitCoordinates coordinates;
        public Stats stat;
    }

    protected override void OnUpdate()
    {
        if (GameManager.isPlayerTurn)
        {
            int count = 0;
            foreach (var enemy in GetEntities<allCharacters>())
            {
                count = 0;
                if (!enemy.stat.isEnemy || enemy.stat.isEncumbered)
                {
                    continue;
                }
                foreach (var allies in GetEntities<allCharacters>())
                {
                    if (!allies.stat.isEnemy)
                    {
                        if (Compare(enemy.coordinates, allies.coordinates))
                        {
                            count++;
                        }
                    }
                }
                if (count >= enemy.stat.presence)
                {
                    enemy.stat.Encumber();
                    break;
                }
            }
        }
    }

    bool Compare(UnitCoordinates unit, UnitCoordinates target)
    {
        if ((unit.x <= target.x + 1 && unit.x >= target.x - 1) &&
            (unit.y <= target.y + 1 && unit.y >= target.y - 1))
            {
                return true;
            }

        return false;
    }
}