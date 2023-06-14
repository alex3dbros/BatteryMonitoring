using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ScanForDevices : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler, IPointerUpHandler
{

    Button btn;
    public Text DebugInfo;
    public GameObject device_button;
    public GameObject parent_list;
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




        if (myIcons != null)
        {
            foreach (var item in myIcons)
            {
                item.color = btn.colors.pressedColor;
            }
        }

        if (myTexts != null)
        {
            foreach (var item in myTexts)
            {
                item.color = btn.colors.pressedColor;
            }
        }


    }


    public void OnPointerEnter(PointerEventData eventData)
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


    public void ClearChildren()
    {
        Debug.Log(parent_list.transform.childCount);
        int i = 0;

        //Array to hold all child obj
        GameObject[] allChildren = new GameObject[parent_list.transform.childCount];

        //Find all child obj and store to that array
        foreach (Transform child in parent_list.transform)
        {
            allChildren[i] = child.gameObject;
            i += 1;
        }

        //Now destroy them
        foreach (GameObject child in allChildren)
        {
            DestroyImmediate(child.gameObject);
        }

        Debug.Log(transform.childCount);
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

        ClearChildren();


        GameObject newButton2 = Instantiate(device_button, transform.position, Quaternion.identity, parent_list.transform);
        newButton2.GetComponentInChildren<Text>().text = "test2";

        BluetoothLEHardwareInterface.Initialize(true, false, () => {

            FoundDeviceListScript.DeviceAddressList = new List<DeviceObject>();

            BluetoothLEHardwareInterface.ScanForPeripheralsWithServices(null, (address, name) => {

                FoundDeviceListScript.DeviceAddressList.Add(new DeviceObject(address, name));

                GameObject newButton = Instantiate(device_button, transform.position, Quaternion.identity, parent_list.transform);
                newButton.GetComponentInChildren<Text>().text = name;

                ConnectToDevice ConnectScript = newButton.GetComponent<ConnectToDevice>();
                ConnectScript.DeviceAddress = address;
            }, null);

        }, (error) => {

            BluetoothLEHardwareInterface.Log("BLE Error: " + error);

        });

        DebugInfo.text = "Exited scanning";


    }





}
