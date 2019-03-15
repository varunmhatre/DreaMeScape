using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KentAbilityHandler : MonoBehaviour
{
    List<ColorRendererCombo> gridsToHighlight;
    List<SpriteRenderer> charactersToHighlight;
    UnitCoordinates gamePiece;
    [SerializeField] Material defaultMaterial;

    // Start is called before the first frame update
    void Start()
    {
        gamePiece = CharacterManager.allAlliedCharacters[3].GetComponent<UnitCoordinates>();
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
                    if (grid.transform.GetComponent<GridPiece>().unit.tag == "Enemy")
                    {
                        charactersToHighlight.Add(grid.transform.GetComponent<GridPiece>().unit.transform.GetChild(0).GetComponent<SpriteRenderer>());
                    }
                }
            }
        }
        foreach (var item in charactersToHighlight)
        {
            item.color = Color.red;
        }
    }

    public void OnMouseHoveringExit()
    {
        foreach (var item in gridsToHighlight)
        {
            item.renderer.material.color = item.color;
        }
        foreach (var item in charactersToHighlight)
        {
            item.color = Color.white;
        }
    }

    public void OnMouseClickWhenOn()
    {
        foreach (var item in gridsToHighlight)
        {
            item.renderer.material.color = item.color;
        }
        foreach (var item in charactersToHighlight)
        {
            item.color = Color.white;
        }
    }
}
