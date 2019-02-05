using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridPieceHighlight : MonoBehaviour
{
    [SerializeField] Material moveHighlight;
    [SerializeField] Material moveLowdark;
    bool isHighlighted;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void highlightPiece()
    {
        isHighlighted = true;
        GetComponent<Renderer>().material = moveHighlight;
    }

    public void lowdarkPiece()
    {
        isHighlighted = false;
        GetComponent<Renderer>().material = moveLowdark;
    }
}
