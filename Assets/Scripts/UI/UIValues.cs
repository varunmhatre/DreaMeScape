using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIValues : MonoBehaviour
{
    private int value;
    [SerializeField]
    public int initialValue;
    private TextMesh textValue;

	// Use this for initialization
	void Start()
    {
        textValue = gameObject.GetComponent<TextMesh>();
        value = initialValue;       
        textValue.text = value.ToString();
    }
    void Update()
    {
        textValue.text = value.ToString();  
    }
    public void SetValue(int vl)
    {
        value = vl;
    }
    public int GetValue()
    {
        return value;
    }
}
