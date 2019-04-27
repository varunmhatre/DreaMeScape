using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyUsedAnimation : MonoBehaviour
{
    public static bool enableAnim;
    Animator animator;
    // Use this for initialization
    void Start()
    {
        enableAnim = false;
        animator = transform.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (enableAnim)
        {
            animator.SetBool("activate", true);
            gameObject.GetComponent<Image>().enabled = true;
        }
        else
        {
            gameObject.GetComponent<Image>().enabled = false;
        }
    }
}
