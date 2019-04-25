using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonRotation : MonoBehaviour
{
    public Vector3 initialRot;
    public Vector3 initialPos;
    // Start is called before the first frame update
    void Start()
    {
        initialRot = transform.eulerAngles;
        initialPos = transform.position;
    }
}
