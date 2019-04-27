using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyUsedAnimation : MonoBehaviour
{
    public static bool enableAnim;
    // Use this for initialization
    void Start()
    {
        enableAnim = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (enableAnim)
        {
            gameObject.GetComponent<Image>().enabled = true;
        }
        else
        {
            gameObject.GetComponent<Image>().enabled = false;
        }
    }
}
