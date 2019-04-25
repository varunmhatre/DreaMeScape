using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RaycastManager : MonoBehaviour
{
    private static RaycastHit[] hitTargets;
    public static bool leftClicked;
    public static bool rightClicked;

    // Start is called before the first frame update
    void Start()
    {
    }

    // FixedUpdate so that it occurs before Updates that require Raycast to be completed
    void Update()
    {
        leftClicked = false;
        rightClicked = false;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        hitTargets = Physics.RaycastAll(ray, Mathf.Infinity);

        CleanseFlags();

        if (Input.GetMouseButtonDown(0))
        {
            leftClicked = true;
        }
        if (Input.GetMouseButtonDown(1))
        {
            rightClicked = true;
        }
    }

    void CleanseFlags()
    {
        if (CharacterAbility.cleanSelectionMode)
        {
            CharacterAbility.cleanSelectionMode = false;
            CharacterAbility.inSelectionMode = false;
        }
        if (CannonStaticVariables.clearCannonSelection)
        {
            CannonStaticVariables.clearCannonSelection = false;
            CannonStaticVariables.isCannonSelected = false;
        }
        if (PlayerControls.clearSelectedUnit)
        {
            PlayerControls.clearSelectedUnit = false;
            PlayerControls.selectedUnit = null;
        }
    }

    //Get the last hit Raycast for provided tag
    public static RaycastHit GetRaycastHitForTag(string tag)
    {
        RaycastHit hit = new RaycastHit();

        if (hitTargets != null && !GameManager.tutorialBlockClick)
        {
            for (int i = 0; i < hitTargets.Length; i++)
            {
                if (hitTargets[i].transform != null && hitTargets[i].transform.tag == tag)
                {
                    hit = hitTargets[i];
                }
            }
        }

        //If tag doesn't exist, will return new RaycastHit() and have 'null' hit.transform
        return hit;
    }

    public static void EmptyRaycastTargets()
    {
        hitTargets = null;
    }

    public static bool IsRaycastTargetsEmpty()
    {
        if (hitTargets == null)
        {
            return true;
        }
        return false;
    }
}
