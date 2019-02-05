using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridPieceHighlight : MonoBehaviour
{

    [SerializeField] Material moveHighlight;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Renderer>().material = moveHighlight;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
