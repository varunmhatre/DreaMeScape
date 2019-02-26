using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EdAbilityHandler : MonoBehaviour
{

    EdAbilityParticle poison;
    // Start is called before the first frame update
    void Start()
    {
        poison = CharacterManager.allAlliedCharacters[0].GetComponent<EdAbilityParticle>();
    }

    public void OnMouseHoveringStart()
    {
        poison.IsHovering();
    }

    public void OnMouseHoveringExit()
    {
        poison.StoppedHovering();
    }

    public void OnMouseClickWhenOn()
    {
        poison.Clicked();
    }
}
