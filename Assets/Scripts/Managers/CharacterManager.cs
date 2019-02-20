using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CharacterManager
{
    public static List<GameObject> allAlliedCharacters;
    public static List<GameObject> allEnemyCharacters;
    public static List<GameObject> allCharacters;

    public static void ReCalculateAlliedCharacters()
    {
        for (int i = allAlliedCharacters.Count - 1; i > -1; i--)
        {
            if (allAlliedCharacters[i] == null)
            {
                allAlliedCharacters.RemoveAt(i);
            }
        }
    }

    public static void ReCalculateEnemyCharacters()
    {
        for (int i = allEnemyCharacters.Count - 1; i > -1; i--)
        {
            if (allEnemyCharacters[i] == null)
            {
                allEnemyCharacters.RemoveAt(i);
            }
        }
    }

    public static void ReCalculateAllCharacters()
    {
        for (int i = allCharacters.Count - 1; i > -1; i--)
        {
            if (allCharacters[i] == null)
            {
                allCharacters.RemoveAt(i);
            }
        }
    }
}
