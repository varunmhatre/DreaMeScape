using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMouseCursor : MonoBehaviour
{
    public bool cursorChanged;
    public Texture2D mouseTarget;
    Stats selectedUnit;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnMouseEnter()
    {
        if (AdjacencyHandler.NumPlayerCharactersAround(gameObject, 1) >= 1 &&
            !CannonStaticVariables.isCannonSelected && PlayerControls.selectedUnit != null &&
                AdjacencyHandler.CompareAdjacency(gameObject, PlayerControls.selectedUnit.gameObject, 1))
        {
            selectedUnit = PlayerControls.selectedUnit.GetComponent<Stats>();
            cursorChanged = true;
            Cursor.SetCursor(mouseTarget, Vector2.zero, CursorMode.Auto);
        }
    }
    
    private void OnMouseExit()
    {
        if (cursorChanged)
        {
            cursorChanged = false;
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        }
    }

    private void OnMouseOver()
    {
        if (cursorChanged && selectedUnit.hasAttacked)
        {
            cursorChanged = false;
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        }
    }

    private void OnDestroy()
    {
        if (cursorChanged)
        {
            cursorChanged = false;
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        }
    }
}
