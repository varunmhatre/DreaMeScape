using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CharacterManager
{
    public static List<GameObject> allAlliedCharacters;
    public static List<GameObject> allEnemyCharacters;
    public static List<GameObject> allCharacters;

    public static void RemoveFromEnemies(GameObject unit)
    {
        for (int i = 0; i < allEnemyCharacters.Count; i++)
        {
            if (allEnemyCharacters[i] == unit)
            {
                allEnemyCharacters.RemoveAt(i);
                break;
            }
        }
        ReCalculateAllCharacters();
    }

    public static void ReCalculateAllCharacters()
    {
        allCharacters.Clear();
        foreach (var item in allAlliedCharacters)
        {
            allCharacters.Add(item);
        }
        foreach (var item in allEnemyCharacters)
        {
            allCharacters.Add(item);
        }
    }
}
