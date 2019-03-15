using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TEMP
public class HallyAbilityHandler : MonoBehaviour
{
    List<ColorRendererCombo> gridsToHighlight;
    List<Vector3> charactersToHighlight;
    UnitCoordinates gamePiece;
    [SerializeField] Material defaultMaterial;
    [SerializeField] GameObject spotLight;
    List<GameObject> allLights;
    bool dyingLight;
    float timer;

    // Start is called before the first frame update
    void Start()
    {
        dyingLight = false;
        gamePiece = CharacterManager.allAlliedCharacters[1].GetComponent<UnitCoordinates>();
        charactersToHighlight = new List<Vector3>();
        gridsToHighlight = new List<ColorRendererCombo>();
        allLights = new List<GameObject>();
    }

    private void Update()
    {
        if (dyingLight)
        {
            MouseIsClicked();
        }
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
                grid.transform.GetComponent<Renderer>().material.color = Color.white;
                if (grid.transform.GetComponent<GridPiece>().unit)
                {
                    if (grid.transform.GetComponent<GridPiece>().unit.tag == "Player")
                    {
                        Vector3 position = grid.transform.position;
                        position.y += 2.0f;
                        charactersToHighlight.Add(position);
                    }
                }
            }
        }
        foreach (var item in charactersToHighlight)
        {
            allLights.Add(Instantiate(spotLight, item, spotLight.transform.rotation));
        }
    }

    public void OnMouseHoveringExit()
    {
        foreach (var item in gridsToHighlight)
        {
            item.renderer.material.color = item.color;
        }
        for (int i = allLights.Count - 1; i > -1; i--)
        {
            Destroy(allLights[i]);
        }
        allLights.Clear();
    }

    public void OnMouseClickWhenOn()
    {
        foreach (var item in gridsToHighlight)
        {
            item.renderer.material.color = item.color;
        }
        timer = 0.0f;
        dyingLight = true;
    }

    void MouseIsClicked()
    {
        timer += Time.deltaTime;
        foreach (var item in allLights)
        {
            item.GetComponent<VLight>().lightMultiplier /= 1.12f;
        }
        if (timer >= 1.5f)
        {
            dyingLight = false;
            for (int i = allLights.Count -1 ; i > -1 ; i--)
            {
                Destroy(allLights[i]);
            }
            allLights.Clear();
        }
    }
}
