using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsHandler : MonoBehaviour
{
    [SerializeField] private Button rightButton;
    [SerializeField] private Button leftButton;
    [SerializeField] private GameObject[] slidingImage; 

    [SerializeField] private int max = 6;
    [SerializeField] private int min = 0;
    public int currentIndex = 0;
    private int count = 1;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 1; i < slidingImage.Length; i++)
        {
            slidingImage[i].SetActive(false);
        }
        
    }
    void Update()
    { 

    }
    public void ChangePanel(bool increase)
    {
        currentIndex = Mathf.Clamp(currentIndex + (increase ? count : -count), min, max);
        slidingImage[currentIndex].SetActive(true);
        if(increase)
        {
            slidingImage[currentIndex - 1].SetActive(false);
        }
        if(!increase)
        {
            slidingImage[currentIndex + 1].SetActive(false);
        }
    }
}
