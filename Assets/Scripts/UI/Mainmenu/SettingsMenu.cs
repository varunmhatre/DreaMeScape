using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public Dropdown resolutionDropdown;
    private Resolution[] resolutions;

    private int currentResolutionIndex;
    private List<string> options = new List<string>();
    private bool onBoot;

    // Start is called before the first frame update
    void Start()
    {
        resolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions();

        for (int i = 0; i < resolutions.Length; i++)
        { 
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);

            if ((resolutions[i].width == Screen.currentResolution.width) && (resolutions[i].height == Screen.currentResolution.height)) ;
            {
                currentResolutionIndex = i;
            }
         } 

        resolutionDropdown.value = currentResolutionIndex;
      
        resolutionDropdown.RefreshShownValue();

        resolutionDropdown.AddOptions(options);

        onBoot = false;
    }

    // Update is called once per frame
    void Update()
    {
         if(!onBoot)
        {
            resolutionDropdown.value = resolutions.Length - 1;
            resolutionDropdown.AddOptions(options);
        }
    }

    public void SetQuality(int index) 
    {
        onBoot = true;
        QualitySettings.SetQualityLevel(index);
    }

    public void SetFullScreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }
}
