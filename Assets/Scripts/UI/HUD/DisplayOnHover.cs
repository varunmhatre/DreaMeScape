using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayOnHover : MonoBehaviour
{
    [SerializeField] private GameObject objToBubblePres;
    [SerializeField] private GameObject objToBubbleResist;
    private GameObject bubblingObj;
    [SerializeField] public bool bubblesUpOnAction;
    public bool valueChanged;
    private bool firstTime;
    private int prevPresence;
    private int prevResist;
    private Vector3 startScale;
    private float distBetweenBubbling;
    private float bubblingRate;
    private float bloatRate;
    private float amountBloat;
    private float timer;

    void Start()
    {
        timer = 0.0f;
        if (bubblesUpOnAction)
        {
            amountBloat = 0.0f;
            bubblingRate = 2.25f;
            bloatRate = 0.5f;
            startScale = objToBubblePres.transform.localScale;
            for (int i = 0; i < CharacterManager.allAlliedCharacters.Count; i++)
            {
                if (CharacterManager.allAlliedCharacters[i] == gameObject)
                {
                    prevPresence = CharacterManager.allAlliedCharacters[i].GetComponent<Stats>().health;
                    prevResist = CharacterManager.allAlliedCharacters[i].GetComponent<Stats>().damage;
                }
            }
        }
        valueChanged = false;
        firstTime = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (bubblesUpOnAction && timer >= 0.5f)
        {
            if (!valueChanged)
            {
                CheckForBubbling();
            }

            if (valueChanged)
            {
                Bubble(bubblingObj);
            }
            else
            {
                objToBubblePres.transform.localScale = startScale;
                objToBubbleResist.transform.localScale = startScale;
            }
        }
        timer += Time.deltaTime;
    }

    public void CheckForBubbling()
    {
        int presenceValue = 0;
        int resistValue = 0;

        for (int i = 0; i < CharacterManager.allAlliedCharacters.Count; i++)
        {
            if (CharacterManager.allAlliedCharacters[i] == gameObject)
            {
                presenceValue = CharacterManager.allAlliedCharacters[i].GetComponent<Stats>().damage;
                resistValue = CharacterManager.allAlliedCharacters[i].GetComponent<Stats>().health;
            }
        }
        for (int i = 0; i < CharacterManager.allEnemyCharacters.Count; i++)
        {
            if (CharacterManager.allEnemyCharacters[i] == gameObject)
            {
                presenceValue = CharacterManager.allEnemyCharacters[i].GetComponent<Stats>().damage;
                resistValue = CharacterManager.allEnemyCharacters[i].GetComponent<Stats>().health;
            }
        }

        if (firstTime)
        {
            firstTime = false;
        }
        else
        {
            if (prevPresence != presenceValue)
            {
                objToBubblePres.SetActive(true);
                objToBubblePres.transform.GetChild(0).gameObject.SetActive(true);
                valueChanged = true;
                bubblingObj = objToBubblePres;
                distBetweenBubbling = 0.0f;
                amountBloat = 0.0f;
            }
            if (prevResist != resistValue)
            {
                objToBubbleResist.SetActive(true);
                objToBubbleResist.transform.GetChild(0).gameObject.SetActive(true);
                valueChanged = true;
                bubblingObj = objToBubbleResist;
                distBetweenBubbling = 0.0f;
                amountBloat = 0.0f;
            }
        }


        prevPresence = presenceValue;
        prevResist = resistValue;


    }

    public void Bubble(GameObject bubObj)
    {
        bubObj.transform.localScale = new Vector3(startScale.x + amountBloat, startScale.y + amountBloat, startScale.z + amountBloat);
        amountBloat += bloatRate * Time.deltaTime;
        distBetweenBubbling += bubblingRate * Time.deltaTime;

        if (distBetweenBubbling >= 1.0f)
        {
            distBetweenBubbling = 0.0f;
            valueChanged = false;
            if (!objToBubblePres.activeSelf || !objToBubbleResist.activeSelf)
            {
                bubObj.SetActive(false);
                bubObj.transform.GetChild(0).gameObject.SetActive(false);
            }
        }
    }
}
