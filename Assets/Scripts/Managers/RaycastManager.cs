using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastManager : MonoBehaviour
{

    private RaycastHit hit;

    // Start is called before the first frame update
    void Start()
    {
        hit = new RaycastHit();
    }

    // Update is called once per frame
    void Update()
    {        
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
            Physics.Raycast(ray, out hit, Mathf.Infinity);
        }         
    }

    public string getRaycastHitTag()
    {
        Debug.Log(hit.transform.tag);
        return hit.transform.tag;
    }
}
