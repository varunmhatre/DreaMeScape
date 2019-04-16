using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class AbilityMouseHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private GameObject tooltip;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
 
    public void OnPointerEnter(PointerEventData eventData)
    {
        tooltip.SetActive(true);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        tooltip.SetActive(false);
    }
}