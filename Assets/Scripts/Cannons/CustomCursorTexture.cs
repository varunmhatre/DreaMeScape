using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomCursorTexture : MonoBehaviour
{
    [SerializeField] public Texture2D mouseTarget;
    [SerializeField] public Texture2D mainMouse;

    public void EnableCrossBar()
    {
        Cursor.SetCursor(mouseTarget, Vector2.zero, CursorMode.Auto);
    }

    public void DisableCrossBar()
    {
        Cursor.SetCursor(mainMouse, Vector2.zero, CursorMode.Auto);
    }

    public void ForceMode()
    {
        Cursor.SetCursor(mouseTarget, Vector2.zero, CursorMode.ForceSoftware);
    }
}
