using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DreamMeter : MonoBehaviour
{
    [SerializeField] RectTransform[] dreamMeterBarArr;
    private float startingWidth;
    private int meterValue;

    // Start is called before the first frame update
    void Start()
    {
        if (dreamMeterBarArr[0] != null)
        {
            startingWidth = dreamMeterBarArr[0].rect.width;
        }

        for (int i = 0; i < dreamMeterBarArr.Length; i++)
        {
            dreamMeterBarArr[i].sizeDelta = new Vector2(0.0f, dreamMeterBarArr[i].rect.height);
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

            dreamMeterBarArr[i].sizeDelta = new Vector2(startingWidth * meterValue / CharacterManager.allAlliedCharacters[i].GetComponent<Stats>().maxMeter, dreamMeterBarArr[i].rect.height);
            dreamMeterBarArr[i].GetComponent<RectTransform>().SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, 2, dreamMeterBarArr[i].rect.width);
        }
    }
}