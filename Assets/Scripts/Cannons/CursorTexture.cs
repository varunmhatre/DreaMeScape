﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorTexture : MonoBehaviour
{
    [SerializeField] public Texture2D mouseTarget;

    public void EnableCrossBar()
    {
        Cursor.SetCursor(mouseTarget, Vector2.zero, CursorMode.Auto);
    }

    public void DisableCrossBar()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }

    public void ForceMode()
    {
        Cursor.SetCursor(mouseTarget, Vector2.zero, CursorMode.ForceSoftware);
    }
}
