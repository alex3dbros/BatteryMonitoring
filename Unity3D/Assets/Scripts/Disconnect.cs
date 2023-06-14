using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Disconnect : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler, IPointerUpHandler
{
    public GameObject ScriptManager;
    Button btn;

    Image[] myIcons;
    Text[] myTexts;

    private void Start()
    {
        btn = GetComponent<Button>();

        if (transform.childCount > 0)
        {
            myIcons = transform.GetComponentsInChildren<Image>();

            myTexts = transform.GetComponentsInChildren<Text>();
        }

    }


    public void OnPointerDown(PointerEventData eventData)
    {

    }


    public void OnPointerEnter(PointerEventData eventData)
    {

    }

    public void OnPointerExit(PointerEventData eventData)
    {

    }

    public void OnPointerUp(PointerEventData eventData)
    {

        BatteryMonitor batMon = ScriptManager.GetComponent<BatteryMonitor>();
        batMon.Action = "Disconnect";
    }
}