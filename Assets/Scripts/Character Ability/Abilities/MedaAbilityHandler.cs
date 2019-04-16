using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedaAbilityHandler : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject fireballPrefab;
    [SerializeField] Material highlightMaterial;
    [SerializeField] Material gridMaterial;
    GameObject fireball;

    Vector3 initialPosition;
    Vector3 finalPosition;

    float timer;
    bool isFireballActive;
    bool isClickedOn;
    bool startCheckingForEnemy;

    List<ColorRendererCombo> gridsToHighlight;
    List<SpriteRenderer> charactersToHighlight;

    [SerializeField] public Texture2D fireballMouse;

    private void Start()
    {
        charactersToHighlight = new List<SpriteRenderer>();
        gridsToHighlight = new List<ColorRendererCombo>();
        isClickedOn = false;
    }

    void AttackWithFireball(Vector3 piratePosition)
    {
        if (!isFireballActive)
        {
            finalPosition = piratePosition;
            fireballPrefab.transform.position = finalPosition + new Vector3(0.0f, 5.0f, 0.0f);
            initialPosition = fireballPrefab.transform.position;
            fireball = Instantiate(fireballPrefab, fireballPrefab.transform.position, Quaternion.identity);
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

        if (startCheckingForEnemy && RaycastManager.leftClicked)
        {
            isClickedOn = false;
            startCheckingForEnemy = false;
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
            RaycastHit hit = RaycastManager.GetRaycastHitForTag("Enemy");
            if (hit.transform)
            {
                AttackWithFireball(hit.transform.position);
            }
            RevertToNormal();
        }
    }

    public void OnMouseHoveringStart()
    {
        UnitCoordinates gamePiece = CharacterManager.allAlliedCharacters[3].GetComponent<UnitCoordinates>();
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
        if (!isClickedOn)
        {
            UnCheckEverything();
            isClickedOn = false;
        }
        else
        {
            startCheckingForEnemy = true;
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
        startCheckingForEnemy = false;
        Cursor.SetCursor(fireballMouse, Vector2.zero, CursorMode.Auto);
    }

    void UnCheckEverything()
    {
        foreach (var item in gridsToHighlight)
        {
            item.renderer.material.color = item.color;
        }
        foreach (var item in charactersToHighlight)
        {
            if (item)
            {
                item.color = Color.white;
            }
        }
    }

    void RevertToNormal()
    {
        foreach (var item in gridsToHighlight)
        {
            item.renderer.material = gridMaterial;
        }
        foreach (var item in charactersToHighlight)
        {
            if (item)
            {
                item.color = Color.white;
            }
        }
    }
        
}
