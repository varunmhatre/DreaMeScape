using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeHandler : MonoBehaviour
{ 
    [SerializeField] private Button decreaseButton;
    [SerializeField] private Button increaseButton;
    [SerializeField] private GameObject[] fillVolume;
    [SerializeField] private Sprite[] sprites;

    [SerializeField] private int max;
    [SerializeField] private int min;
    public int currentIndex;

    private float volumeControl;
    private float maxVolume;
    private float minVolume;
    private int count;

    private int indexCount;

    public static float masterVolume;
    private bool onLoad;
    // Start is called before the first frame update
    void Start()
    {
        max = 5;
        min = 0;
        currentIndex = 0;
        maxVolume = 1;
        minVolume = 0;
        count = 1;
        indexCount = 5;
        onLoad = false;
        //if(!onLoad)
        //{
        //    indexCount = 5;
        //}
    }
    void Update()
    {
        //SetVolume(indexCount);
        //masterVolume = indexCount;
        //AudioListener.volume = masterVolume;
    }
    public void ChangeVolume(bool isPressed)
    {
        currentIndex = Mathf.Clamp(currentIndex + (isPressed ? count : -count), min, max);
        onLoad = true;
        if(isPressed)
        {
            //Debug.Log("Increase the volume");
            volumeControl += 0.2f;
            
            if (volumeControl >= maxVolume)
            {
                volumeControl = maxVolume;
            }
            if (indexCount >= max)
            {
                indexCount = max;
                fillVolume[indexCount].GetComponent<Image>().sprite = sprites[0];
            }
            else
            {
                indexCount++;
                fillVolume[indexCount - 1].GetComponent<Image>().sprite = sprites[0];

            }
            
        }
        if(!isPressed)
        {
            //Debug.Log("Decrease the volume");
            volumeControl -= 0.2f;
          
            if (volumeControl <= minVolume)
            {
                volumeControl = minVolume;
            }
            if (indexCount <= min)
            {
                indexCount = min;
                fillVolume[indexCount].GetComponent<Image>().sprite = sprites[1];
            }
            else
            {
                indexCount--;

                fillVolume[indexCount + 1].GetComponent<Image>().sprite = sprites[1];
            }
            
        }
        //Debug.Log("indexCount:    " + indexCount);

        AudioListener.volume = indexCount;
        masterVolume = indexCount;


        float setVolume = PlayerPrefs.GetFloat("setVolume", indexCount);
    }

    //public void SetVolume(int val)
    //{
    //    indexCount = val;
    //}
    
    //public int GetVolume()
    //{
    //    return indexCount;
    //}
}
