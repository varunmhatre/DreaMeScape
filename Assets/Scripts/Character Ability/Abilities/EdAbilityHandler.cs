using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EdAbilityHandler : MonoBehaviour
{
    EdAbilityParticle poison;
    List<Renderer> gridsToHighlight;
    List<SpriteRenderer> charactersToHighlight;
    UnitCoordinates gamePiece;

    // Start is called before the first frame update
    void Start()
    {
        poison = CharacterManager.allAlliedCharacters[0].GetComponent<EdAbilityParticle>();
        gamePiece = CharacterManager.allAlliedCharacters[0].GetComponent<UnitCoordinates>();
        charactersToHighlight = new List<SpriteRenderer>();
        gridsToHighlight = new List<Renderer>();
    }

    public void OnMouseHoveringStart()
    {
        poison.IsHovering();
        gridsToHighlight.Clear();
        charactersToHighlight.Clear();
        foreach (var grid in GridMatrix.gameGrid)
        {
            if ((grid.x >= (gamePiece.x - 2) && grid.x <= (gamePiece.x + 2)) &&
                (grid.y >= (gamePiece.y - 2) && grid.y <= (gamePiece.y + 2)))
            {
                gridsToHighlight.Add(grid.transform.GetComponent<Renderer>());
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
        poison.StoppedHovering();
        foreach (var item in gridsToHighlight)
        {
            item.material.color = Color.white;
        }
        foreach (var item in charactersToHighlight)
        {
            item.color = Color.white;
        }
    }

    public void OnMouseClickWhenOn()
    {
        poison.Clicked();
        foreach (var item in gridsToHighlight)
        {
            item.material.color = Color.white;
        }
        foreach (var item in charactersToHighlight)
        {
            item.color = Color.white;
        }
    }
}
