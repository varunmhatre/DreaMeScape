using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIText : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Renderer renderer = gameObject.GetComponent<Renderer>();

        if (renderer == null)
        {
            renderer = gameObject.GetComponent<MeshRenderer>();
        }

        if (renderer != null)
        {
            renderer.sortingOrder = 3;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
