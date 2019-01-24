using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonCrossbarController : MonoBehaviour
{
    [SerializeField] public Texture2D mouseTarget;

    void EnableCrossBar()
    {
        Cursor.SetCursor(mouseTarget, Vector2.zero, CursorMode.Auto);
    }

    void DisableCrossBar()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }
}
