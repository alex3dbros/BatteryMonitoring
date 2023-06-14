using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ConnectToDevice : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler, IPointerUpHandler
{

    public GameObject ScriptManager;
    public string DeviceAddress;
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

        Debug.Log("Button Pressed");
        //scriptmanager.getcomponent(typeof(batterymonitor)).devicename = "somename";
        BatteryMonitor batmon = ScriptManager.GetComponent<BatteryMonitor>();
        batmon.DeviceName = myTexts[0].text;
        batmon.Action = "Connect";
        batmon.DeviceAddress = DeviceAddress;


        //BluetoothLEHardwareInterface.Log("This is the address: " + DeviceAddress);
        //BluetoothLEHardwareInterface.ConnectToPeripheral(DeviceAddress, (address) => { }, null, (address, service, characteristic) => {
        //    BluetoothLEHardwareInterface.Log("This is the address: " + DeviceAddress);


        //    myTexts[0].text = "Connected";

        //    //if (string.IsNullOrEmpty(Services[buttonID]) && string.IsNullOrEmpty(Characteristics[buttonID]))
        //    //{
        //    //    Services[buttonID] = FullUUID(service);
        //    //    Characteristics[buttonID] = FullUUID(characteristic);
        //    //    button.text = device.Name + " connected";
        //    //}

        //}, null);




    }
}