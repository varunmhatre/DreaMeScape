using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeControls : MonoBehaviour
{
    private int volumeControl;
    public Slider volumeSlider;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    } 

    public void ChangeVolume()
    {
        transform.GetComponent<Slider>().value = volumeControl;
    }
}
