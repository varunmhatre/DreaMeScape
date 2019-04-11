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

   // [SerializeField] private GameObject volumeObj;

    public int currentIndex;

    private float volumeControl;
    private float maxVolume;
    private float minVolume;
    private int count;
    private int indexCount;
    [SerializeField] private AudioSource[] audioSrc;
    private float musicVolume = 1f;

    // Start is called before the first frame update
    void Start()
    {
        max = 4;
        min = 0;
        currentIndex = 0;
        maxVolume = 1;
        minVolume = 0;
        count = 1;
        indexCount = 4;
        volumeControl = 1;
    }
    void Update()
    {
        if (audioSrc != null)
        {
            for (int i = 0; i < audioSrc.Length; i++)
            {
                audioSrc[i].volume = volumeControl;
            }
        }
    }
    public void ChangeVolume(bool isPressed)
    {
        currentIndex = Mathf.Clamp(currentIndex + (isPressed ? count : -count), min, max);
        
        if(isPressed)
        {
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
    }

    public void SetVolume(int val)
    {
        indexCount = val;
    }

    public void SetVolume(float vol)
    {
        musicVolume = vol;
    }
}
