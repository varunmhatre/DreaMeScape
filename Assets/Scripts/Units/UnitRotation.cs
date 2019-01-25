using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitRotation : MonoBehaviour
{
    Quaternion rotation;

    // Start is called before the first frame update
    void Start()
    {
        rotation = transform.rotation;   
    }

    public void SetRotation (Quaternion newRotation)
    {
        rotation = newRotation;
        transform.rotation = newRotation;
    }

    public void SetLocalRotation(Quaternion newRotation)
    {
        rotation = newRotation;
        transform.localRotation = newRotation;
    }
}
