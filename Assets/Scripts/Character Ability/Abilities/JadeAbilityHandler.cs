using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JadeAbilityHandler : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Material highlightMaterial;
    [SerializeField] Material gridMaterial;
    GameObject fireball;

    Vector3 initialPosition;
    Vector3 finalPosition;

    float timer;
    bool isFireballActive;
    bool isClickedOn;
    bool startCheckingForClick;

    List<ColorRendererCombo> gridsToHighlight;

    private void Start()
    {
        gridsToHighlight = new List<ColorRendererCombo>();
        isClickedOn = false;
    }

    void AttackWithFireball(Vector3 piratePosition)
    {
        if (!isFireballActive)
        {
            finalPosition = piratePosition;
            isFireballActive = true;
            timer = 0.0f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isFireballActive)
        {
            timer += Time.deltaTime;
            fireball.transform.position = Vector3.Lerp(initialPosition, finalPosition, timer);
            if (timer >= 1.0f)
            {
                Destroy(fireball);
                isFireballActive = false;
            }
        }

        if (startCheckingForClick && Input.GetMouseButtonDown(0))
        {
            isClickedOn = false;
            startCheckingForClick = false;
            RevertToNormal();
        }
    }

    public void OnMouseHoveringStart()
    {
        if (!CharacterManager.allAlliedCharacters[2])
            return;
        UnitCoordinates gamePiece = CharacterManager.allAlliedCharacters[2].GetComponent<UnitCoordinates>();
        gridsToHighlight.Clear();
        foreach (var grid in GridMatrix.gameGrid)
        {
            if ((grid.x >= (gamePiece.x - 2) && grid.x <= (gamePiece.x + 2)) &&
                (grid.y >= (gamePiece.y - 2) && grid.y <= (gamePiece.y + 2)))
            {
                gridsToHighlight.Add(new ColorRendererCombo(grid.transform.GetComponent<Renderer>()));
                grid.transform.GetComponent<Renderer>().material.color = Color.white;
            }
        }
    }

    public void OnMouseHoveringExit()
    {
        if (!isClickedOn)
        {
            UnCheckEverything();
            isClickedOn = false;
        }
        else
        {
            startCheckingForClick = true;
        }
    }

    public void OnMouseClickWhenOn()
    {
        StartCoroutine(MouseIsClicked());
    }

    IEnumerator MouseIsClicked()
    {
        yield return new WaitForEndOfFrame();
        foreach (var item in gridsToHighlight)
        {
            item.renderer.material = highlightMaterial;
        }

        isClickedOn = true;
        startCheckingForClick = false;
    }

    void UnCheckEverything()
    {
        foreach (var item in gridsToHighlight)
        {
            item.renderer.material.color = item.color;
        }
    }

    void RevertToNormal()
    {
        foreach (var item in gridsToHighlight)
        {
            item.renderer.material = gridMaterial;
        }
    }
}
