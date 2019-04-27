using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMouseCursor : MonoBehaviour
{
    public bool cursorChanged;
    //public Texture2D mouseTarget;
    Stats selectedUnit;
    CursorTexture customCursor;
    bool isCursorOn;

    // Start is called before the first frame update
    void Start()
    {
        customCursor = GetComponent<CursorTexture>();
    }
    // Update is called once per frame
    void Update()
    {
        if (RaycastManager.GetRaycastHitForTag("Enemy").transform == transform)
        {
            if (!isCursorOn)
                OnEnter();
            OnOver();
        }

        else if (isCursorOn)
        {
            OnExit();
        }
    }

    private void OnEnter()
    {
        isCursorOn = true;
        if (AdjacencyHandler.NumPlayerCharactersAround(gameObject, 1) >= 1 &&
            !CannonStaticVariables.isCannonSelected && PlayerControls.selectedUnit != null &&
                AdjacencyHandler.CompareAdjacency(gameObject, PlayerControls.selectedUnit.gameObject, 1))
        {
            selectedUnit = PlayerControls.selectedUnit.GetComponent<Stats>();
            cursorChanged = true;
            //Cursor.SetCursor(mouseTarget, Vector2.zero, CursorMode.ForceSoftware);
            if (customCursor)
            {
                customCursor.ForceMode();
            }
        }
    }
    
    private void OnExit()
    {
        if (cursorChanged && customCursor)
        {
            cursorChanged = false;
            //Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
            customCursor.DisableCrossBar();
        }
        isCursorOn = false;
    }

    private void OnOver()
    {
        if (cursorChanged && selectedUnit && selectedUnit.hasAttacked && customCursor)
        {
            cursorChanged = false;
            //Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
            customCursor.DisableCrossBar();
        }
    }

    private void OnDestroy()
    {
        if (cursorChanged && customCursor)
        {
            cursorChanged = false;
            //Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
            customCursor.DisableCrossBar();
        }
    }
}
