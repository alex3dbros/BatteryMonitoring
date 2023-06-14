using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BatteryMonitor : MonoBehaviour
{

    public string DeviceName = "test";
    public string ServiceUUID = "FFF0";
    public string firstCharName = "FFF1";

    private Dictionary<string, int> charDict = new Dictionary<string, int>() {
        {"FFF1", 0 }, {"FFF2", 1}, {"FFF3", 2}, {"FFF4", 3}, {"FFF5", 4}, {"FFF6", 5}, {"FFF7", 6}, {"FFF8", 7}, {"FF01", 8}, {"FF02", 9}
    };


    public string Action = "Wait";

    public Text ConnectionStatus;
    public GameObject PanelMiddle;
    public Text Temp1;
    public Text Temp2;

    public GameObject Page1;
    public GameObject Page2;

    public List<GameObject> cellSliders;

    enum States
    {
        None,
        Scan,
        Connect,
        Subscribe,
        ReadChar,
        Unsubscribe,
        Disconnect,
        Communication,
    }

    private bool connectionInProgress = false;
    private bool connected = false;
    private bool shouldConnect = false;
    private float timeout = 0f;
    private States state = States.None;


    public string DeviceAddress;


    void Reset()
    {
        connectionInProgress = false;    // used to guard against trying to connect to a second device while still connecting to the first
        connected = false;
        timeout = 0f;
        state = States.None;
        //_batMon = null;
        PanelMiddle.SetActive(false);
    }


    void SetState(States newState, float timeout)
    {
        state = newState;
        this.timeout = timeout;
    }


    void ConnectToDevice(string devAddress)
    {
        BluetoothLEHardwareInterface.Log("This is the address: " + DeviceAddress);
        BluetoothLEHardwareInterface.ConnectToPeripheral(DeviceAddress, (address) => { }, null, (address, service, characteristic) => {
            ConnectionStatus.text = "Got Inside connect to peripheral";

            connected = true;
            connectionInProgress = false;
            SetState(States.ReadChar, 2f);
            ConnectionStatus.text = "Subscribing to characteristic";

        }, null);

    }


    void ReadCharacteristics(string devAddress)
    {

        foreach (KeyValuePair<string, int> charD in charDict)
        {

            BluetoothLEHardwareInterface.ReadCharacteristic(devAddress, ServiceUUID, charD.Key, (characteristic, bytes) =>
            {
                if (!System.BitConverter.IsLittleEndian) System.Array.Reverse(bytes);
                ushort val = System.BitConverter.ToUInt16(bytes, 0);

                if (charD.Value < 8)
                {
                    Slider sliderScript = cellSliders[charD.Value].GetComponent<Slider>();
                    sliderScript.value = val;
                }

                if (charD.Value == 8)
                {
                    Temp1.text = val.ToString();
                    BluetoothLEHardwareInterface.Log(string.Format("Temp: {0}, Value: {1}", charD.Key, val));

                }

                if (charD.Value == 9)
                {
                    Temp2.text = val.ToString();
                    BluetoothLEHardwareInterface.Log(string.Format("Temp: {0}, Value: {1}", charD.Key, val));

                }


                //BluetoothLEHardwareInterface.Log(string.Format("Characteristic: {0}, Value: {1}", charD.Key, val));
            });
            System.Threading.Thread.Sleep(100);

        }

            




    }


    void Disconnect(string devAddress)
    {

        if (connected)
        {
            BluetoothLEHardwareInterface.DisconnectPeripheral(DeviceAddress, (address) =>
            {
                BluetoothLEHardwareInterface.DeInitialize(() =>
                {

                    connected = false;
                    state = States.None;
                    Page1.SetActive(true);
                    Page2.SetActive(false);
                });
            });
        }
        else
        {
            BluetoothLEHardwareInterface.DeInitialize(() =>
            {

                state = States.None;
                Page1.SetActive(true);
                Page2.SetActive(false);
            });
        }


    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (Action == "Connect")
        {
            if (!shouldConnect && !connectionInProgress)
            {
                shouldConnect = true;
                connectionInProgress = true;
                Action = "ConnectionStarted";

                ConnectToDevice(DeviceAddress);
                Page1.SetActive(false);
                Page2.SetActive(true);
            }

        }

        if (Action == "Disconnect")
        {
            ConnectionStatus.text = "Disconnecting";
            Action = "Disconnecting";
            SetState(States.Disconnect, 4f);
            
            shouldConnect = false;

        }


        if (timeout > 0f)
        {

            timeout -= Time.deltaTime;
            if (timeout <= 0f)
            {

                timeout = 0f;

                switch (state)
                {

                    case States.None:
                        break;


                    case States.ReadChar:

                        ReadCharacteristics(DeviceAddress);

                        timeout = 0.0001f;

                        BluetoothLEHardwareInterface.Log("Exiting Read Char");

                        break;


                    case States.Disconnect:
                        Disconnect(DeviceAddress);

                        SetState(States.None, 0f);

                        break;

                }


            }


        }



    }


    string FullUUID(string uuid)
    {
        return "0000" + uuid + "-0000-1000-8000-00805F9B34FB";
    }

    bool IsEqual(string uuid1, string uuid2)
    {
        if (uuid1.Length == 4)
            uuid1 = FullUUID(uuid1);
        if (uuid2.Length == 4)
            uuid2 = FullUUID(uuid2);

        return (uuid1.ToUpper().Equals(uuid2.ToUpper()));
    }
}
