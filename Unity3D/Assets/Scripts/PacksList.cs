using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class PacksList : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler, IPointerUpHandler
{

    //local data
    string randomText = "1";
    bool showVideo;
    float musicVolume;
    int totalCoins;
    string[] cars = { "Volvo", "BMW", "Ford", "Mazda" };

    Button btn;
    public Text DebugInfo;
    public GameObject Packs_button;
    public GameObject parent_list;
    Image[] myIcons;
    Text[] myTexts;

    //the object you want to serialize
    GameValues gameValues = new GameValues();

    string fullPath;

    //if true data will be encripted with an XOR function
    bool encrypt = false;

    //for debug purpose only
    static string logText = "";

    void Awake()
    {
        //the filename where saved data will be stored
        fullPath = Application.persistentDataPath + "/" + "LocalFileName";
    }


    //called from scene using built in unity events
    public void LoadGameValues()
    {
        #if JSONSerializationFileSave || BinarySerializationFileSave
                logText += "\nLoad Started (File): " + fullPath;
        #else
                logText += "\nLoad Started (PlayerPrefs): " + fullPath;
        #endif
                SaveManager.Instance.Load<GameValues>(fullPath, DataWasLoaded, encrypt);
    }

    private void DataWasLoaded(GameValues data, SaveResult result, string message)
    {
        logText += "\nData Was Loaded";
        logText += "\nresult: " + result + ", message: " + message;

        if (result == SaveResult.EmptyData || result == SaveResult.Error)
        {
            logText += "\nNo Data File Found -> Creating new data...";
            gameValues = new GameValues();
        }

        if (result == SaveResult.Success)
        {
            gameValues = data;
        }
        randomText = gameValues.randomText;
        showVideo = gameValues.showVideo;
        musicVolume = gameValues.musicVolume;
        totalCoins = gameValues.totalCoins;
    }


    public void SaveGameValues()
    {
        Debug.Log("Saving");
        logText += "\nSave Started";
        gameValues.randomText = "SomeText";
        gameValues.showVideo = showVideo;
        gameValues.musicVolume = musicVolume;
        gameValues.totalCoins = totalCoins;
        gameValues.cars = cars;
        SaveManager.Instance.Save(gameValues, fullPath, DataWasSaved, encrypt);
    }

    private void DataWasSaved(SaveResult result, string message)
    {
        logText += "\nData Was Saved";
        logText += "\nresult: " + result + ", message: " + message;
        if (result == SaveResult.Error)
        {
            logText += "\nError saving data";
        }
    }


    private void Start()
    {
        btn = GetComponent<Button>();

        if (transform.childCount > 0)
        {
            myIcons = transform.GetComponentsInChildren<Image>();

            myTexts = transform.GetComponentsInChildren<Text>();
        }
        //Debug.Log("Starting The App");
        //Debug.Log(randomText);
        LoadGameValues();
        //Debug.Log("Loading");
        //Debug.Log(randomText);
        //Debug.Log(fullPath);

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

        //ClearChildren();


        GameObject newButton2 = Instantiate(Packs_button, transform.position, Quaternion.identity, parent_list.transform);
        newButton2.GetComponentInChildren<Text>().text = "test2";

        SaveGameValues();

        //Serialize();
        //Deserialize();


    }





}
