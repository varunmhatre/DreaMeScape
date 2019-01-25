using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonScript : MonoBehaviour
{
    public int charge = 1;
    public bool isThisCannonSelected;
    Quaternion rotation;

    private void Start()
    {
        rotation = transform.rotation;
    }

    public bool isChargeLeft
    {
        get { return charge > 0; }
    }

    public void Disengage()
    {
        isThisCannonSelected = false;
        transform.rotation = rotation;
    }

    private void Update()
    {
        if (isThisCannonSelected)
        {
            FaceMouse();
        }
    }

    void FaceMouse()
    {
        Vector3 cannnonVec = Camera.main.WorldToScreenPoint(transform.position);
        cannnonVec = Input.mousePosition - cannnonVec;
        float angle = Mathf.Atan2(cannnonVec.y, cannnonVec.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.down);
    }
}
