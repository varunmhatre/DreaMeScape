using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerMoveSpaces 
{    
    public static readonly Dictionary<string, int[,]> Player_Movements = new Dictionary<string, int[,]>()
    {
        {
            "Kent",
            new[,] {
                        { 0, 2, 1, 2, 0 },
                        { 2, 1, -1, 1, 2 },
                        { 0, 2, 1, 2, 0 }
                    }
        },
        {
            "Ed",
            new[,] {
                        { 0, 2, 1, 2, 0 },
                        { 2, 1, -1, 1, 2 },
                        { 0, 2, 1, 2, 0 }
                    }
        },
        {
            "Meda",
            new[,] {
                        { 0, 2, 1, 2, 0 },
                        { 2, 1, -1, 1, 2 },
                        { 0, 2, 1, 2, 0 }
                    }
        },
        {
            "Jade",
            new[,] {
                        { 0, 2, 1, 2, 0 },
                        { 2, 1, -1, 1, 2 },
                        { 0, 2, 1, 2, 0 }
                    }
        },
        {
            "Hally",
            new[,] {
                        { 0, 2, 1, 2, 0 },
                        { 2, 1, -1, 1, 2 },
                        { 0, 2, 1, 2, 0 }
                    }
        }
    };

}

