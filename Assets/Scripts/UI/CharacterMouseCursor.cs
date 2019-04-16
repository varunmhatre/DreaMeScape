using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMouseCursor : MonoBehaviour
{

    CustomCursorTexture customCursor;
    // Start is called before the first frame update
    void Start()
    {
        customCursor = GetComponent<CustomCursorTexture>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnMouseEnter()
    {   
        customCursor.EnableCrossBar();    
    }

    private void OnMouseExit()
    {
        customCursor.DisableCrossBar();
    } 
}
