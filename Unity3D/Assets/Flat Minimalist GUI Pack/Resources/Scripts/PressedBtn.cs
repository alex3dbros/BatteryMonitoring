using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PressedBtn : MonoBehaviour , IPointerDownHandler ,IPointerEnterHandler,IPointerExitHandler,IPointerUpHandler
{

    Button btn;


    Image[] myIcons;
    Text[] myTexts;

    private void Start()
    {
        btn = GetComponent<Button>();

        if (transform.childCount > 0) {
            myIcons = transform.GetComponentsInChildren<Image>();

            myTexts = transform.GetComponentsInChildren<Text>();
        }
            
    }
 
 

    public void OnPointerDown(PointerEventData eventData)
    {
       
        
  

        if (myIcons != null )
        {
            foreach (var item in myIcons)
            {
                item.color = btn.colors.pressedColor;
            }
        }

        if (myTexts!=null)
        {
            foreach (var item in myTexts)
            {
                item.color = btn.colors.pressedColor;
            }
        }

    
    }
 

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (myIcons !=null)
        {
            foreach (var item in myIcons)
            {
                item.color = btn.colors.highlightedColor;
            }
        }

        if (myTexts != null)
        {
            foreach (var item in myTexts)
            {
                item.color = btn.colors.highlightedColor;
            }
        }

  

   

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (myIcons != null)
        {
            foreach (var item in myIcons)
            {
                item.color = btn.colors.normalColor;
            }
        }

        if (myTexts != null)
        {

            foreach (var item in myTexts)
            {
                item.color = btn.colors.normalColor;
            }
        }
 

    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (myIcons != null)
         {
            foreach (var item in myIcons)
            {
                item.color = btn.colors.highlightedColor;
            }
        }

        if (myTexts != null)
        {
            foreach (var item in myTexts)
            {
                item.color = btn.colors.highlightedColor;
            }
        }


     
    }
}
