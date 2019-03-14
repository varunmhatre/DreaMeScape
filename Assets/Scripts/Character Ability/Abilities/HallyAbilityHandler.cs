using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TEMP
public class HallyAbilityHandler : MonoBehaviour
{
    List<ColorRendererCombo> gridsToHighlight;
    List<SpriteRenderer> charactersToHighlight;
    UnitCoordinates gamePiece;
    [SerializeField] Material defaultMaterial;

    // Start is called before the first frame update
    void Start()
    {
        gamePiece = CharacterManager.allAlliedCharacters[1].GetComponent<UnitCoordinates>();
        charactersToHighlight = new List<SpriteRenderer>();
        gridsToHighlight = new List<ColorRendererCombo>();
    }

    public void OnMouseHoveringStart()
    {
        gridsToHighlight.Clear();
        charactersToHighlight.Clear();
        foreach (var grid in GridMatrix.gameGrid)
        {
            if ((grid.x >= (gamePiece.x - 2) && grid.x <= (gamePiece.x + 2)) &&
                (grid.y >= (gamePiece.y - 2) && grid.y <= (gamePiece.y + 2)))
            {
                gridsToHighlight.Add(new ColorRendererCombo(grid.transform.GetComponent<Renderer>()));
                grid.transform.GetComponent<Renderer>().material.color = Color.red;
                if (grid.transform.GetComponent<GridPiece>().unit)
                {
                    if (grid.transform.GetComponent<GridPiece>().unit.tag == "Player")
                    {
                        charactersToHighlight.Add(grid.transform.GetComponent<GridPiece>().unit.transform.GetChild(0).GetComponent<SpriteRenderer>());
                    }
                }
            }
        }
    }

    public void OnMouseHoveringExit()
    {
        foreach (var item in gridsToHighlight)
        {
            item.renderer.material.color = item.color;
        }
    }

    public void OnMouseClickWhenOn()
    {
        foreach (var item in gridsToHighlight)
        {
            item.renderer.material.color = item.color;
        }
    }
}
