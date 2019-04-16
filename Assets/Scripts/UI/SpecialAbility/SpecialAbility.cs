using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class SpecialAbility : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    List<Transform> gridsToHighlight;

    [SerializeField] Material highlightMaterial;
    [SerializeField] Material initialMaterial;
    // Start is called before the first frame update
    void Start()
    {
        gridsToHighlight = new List<Transform>();
        GetGridRadius();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void GetGridRadius()
    {
        //Temp. Need a separate cannon highlight
        UnitCoordinates gamePiece = gameObject.GetComponent<UnitCoordinates>();

        foreach (var grid in GridMatrix.gameGrid)
        {
            if ((grid.x >= (gamePiece.x - CannonStaticVariables.cannonRadius) && grid.x <= (gamePiece.x + CannonStaticVariables.cannonRadius)) &&
                (grid.y >= (gamePiece.y - CannonStaticVariables.cannonRadius) && grid.y <= (gamePiece.y + CannonStaticVariables.cannonRadius)))
            {
                gridsToHighlight.Add(grid.transform);
            }
        }
    }
    public void HighlightGrids()
    {
        foreach (var item in gridsToHighlight)
        {
            //Temp. Need a separate cannon highlight
            //item.GetComponent<GridPieceHighlight>().isHighlighted = true;

            item.GetComponent<Renderer>().material = highlightMaterial;
        }
    }

    public void RemoveHighlights()
    {
        foreach (var item in gridsToHighlight)
        {
            //Temp. Need a separate cannon highlight
            item.GetComponent<Renderer>().material = initialMaterial;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        HighlightGrids();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        RemoveHighlights();
    }
}
