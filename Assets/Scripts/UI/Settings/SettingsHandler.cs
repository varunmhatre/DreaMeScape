using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsHandler : MonoBehaviour
{
    [SerializeField] private Button rightButton;
    [SerializeField] private Button leftButton;
    [SerializeField] private GameObject[] slidingImage; 

    [SerializeField] private int max;
    [SerializeField] private int min;
    public static int currentIndex;
    private int count = 1;

    // Start is called before the first frame update
    void Start()
    {
        max = 6;
        min = 0;
        currentIndex = 0;

        for (int i = 1; i < slidingImage.Length; i++)
        {
            slidingImage[i].SetActive(false);
        }  
    }
    void Update()
    {
        DisplayButton();
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


    private void DisplayButton()
    { 
        if (currentIndex == 0)
        {
            leftButton.gameObject.SetActive(false);
            rightButton.gameObject.SetActive(true);
            slidingImage[0].SetActive(true);
            for (int i = 1; i < slidingImage.Length; i++)
            {
                slidingImage[i].SetActive(false);
            }
        }
        else if (currentIndex == 5)
        {
            rightButton.gameObject.SetActive(false);
        }
        else
        {
            rightButton.gameObject.SetActive(true);
            leftButton.gameObject.SetActive(true);
        } 
    }
}
