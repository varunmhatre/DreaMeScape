using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField] RectTransform[] healthBarArr;
    private float startingWidth;
    private float startingHeight;
    private int healthValue;

    // Start is called before the first frame update
    void Start()
    {
        if (healthBarArr[0] != null)
        { 
            startingHeight = healthBarArr[0].rect.height;
        }

        for (int i = 0; i < healthBarArr.Length; i++)
        { 
            healthBarArr[i].sizeDelta = new Vector2(healthBarArr[i].rect.width, 0.0f);
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
            if (!CharacterManager.allAlliedCharacters[i])
                continue;
            healthValue = CharacterManager.allAlliedCharacters[i].GetComponent<Stats>().health;
            //This is for vertical fill meter.
            healthBarArr[i].sizeDelta = new Vector2(healthBarArr[i].rect.width, (startingHeight * healthValue / CharacterManager.allAlliedCharacters[i].GetComponent<Stats>().maxHealth));
            healthBarArr[i].GetComponent<RectTransform>().SetInsetAndSizeFromParentEdge(RectTransform.Edge.Bottom, 2, healthBarArr[i].rect.height);
        }
    }
}