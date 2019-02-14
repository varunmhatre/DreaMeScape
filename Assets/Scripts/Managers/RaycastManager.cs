using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastManager : MonoBehaviour
{

    private RaycastHit[] hitTargets;

    // Start is called before the first frame update
    void Start()
    {
    }

    // FixedUpdate so that it occurs before Updates that require Raycast to be completed
    void Update()
    {        
        //Update hit when you click the primary mouse button
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
            hitTargets = Physics.RaycastAll(ray, Mathf.Infinity);

            GetComponent<PlayerControls>().mouseClickToggle();
            GameObject.Find("CannonHandler").GetComponent<CannonSystem>().mouseClickToggle();
        }         
    }

    //Get the last hit Raycast for provided tag
    public RaycastHit getRaycastHitForTag(string tag)
    {
        RaycastHit hit = new RaycastHit();

        for (int i = 0; i < hitTargets.Length; i++)
        {
            if (hitTargets[i].transform.tag == tag)
            {
                hit = hitTargets[i];
            }
        }

        //If tag doesn't exist, will return new RaycastHit() and have 'null' hit.transform
        return hit;
    }
}
