using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DreamMeter : MonoBehaviour
{
    [SerializeField] RectTransform[] dreamMeterBarArr;
    private float startingWidth;
    private float startingHeight;
    private int meterValue;

    // Start is called before the first frame update
    void Start()
    {
        if (dreamMeterBarArr[0] != null)
        {
            //startingWidth = dreamMeterBarArr[0].rect.width;
            startingHeight = dreamMeterBarArr[0].rect.height;
        }

        for (int i = 0; i < dreamMeterBarArr.Length; i++)
        {
            //dreamMeterBarArr[i].sizeDelta = new Vector2(0.0f, dreamMeterBarArr[i].rect.height);
            dreamMeterBarArr[i].sizeDelta = new Vector2(dreamMeterBarArr[i].rect.width, 0.0f);
        }
    }
    // Update is called once per frame
    void Update()
    {
        UpdateMeter();
    }
    void UpdateMeter()
    {
        for (int i = 0; i < CharacterManager.allAlliedCharacters.Count; i++)
        {
            meterValue = CharacterManager.allAlliedCharacters[i].GetComponent<Stats>().meterUnitsFilled;
            
           //This is for Horizontal fill meter.
           // dreamMeterBarArr[i].sizeDelta = new Vector2(startingWidth * meterValue / CharacterManager.allAlliedCharacters[i].GetComponent<Stats>().maxMeter, dreamMeterBarArr[i].rect.height);
           // dreamMeterBarArr[i].GetComponent<RectTransform>().SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, 2, dreamMeterBarArr[i].rect.width);

            //This is for vertical fill meter.
            dreamMeterBarArr[i].sizeDelta = new Vector2(dreamMeterBarArr[i].rect.width,startingHeight * meterValue / CharacterManager.allAlliedCharacters[i].GetComponent<Stats>().maxMeter);
            dreamMeterBarArr[i].GetComponent<RectTransform>().SetInsetAndSizeFromParentEdge(RectTransform.Edge.Bottom, 2, dreamMeterBarArr[i].rect.height);
        }
    }
}