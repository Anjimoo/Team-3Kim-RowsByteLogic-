using System;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEngine.UI;

public class PregameGUIHandler : MonoBehaviour
{
    bool busy = false;
    bool error = false;
    bool ifP1u1 = false, ifP1u2 = false, ifP1u3 = false, ifP1u4 = false;
    bool ifP2u1 = false, ifP2u2 = false, ifP2u3 = false, ifP2u4 = false;

    #region Unity components define
    //General Buttons
    [SerializeField]
    Button mapButton, startButton;
    [SerializeField]
    Button p1u1, p1u2, p1u3, p1u4;
    [SerializeField]
    Button p2u1, p2u2, p2u3, p2u4;

    //Player canvas buttons
    [SerializeField]
    Button[] avaUnitsButtonsP1, avaUnitsButtonsP2;
    [SerializeField]
    Button closeUnit1List, closeUnit2List, closeMapList;

    //Image fields labels
    [SerializeField]
    Text[] player1UnitNames, player2UnitNames;

    //Image fields icons
    [SerializeField]
    Image[] player1UnitImages, player2UnitImages;

    //"Unit list" canvas and "Map list" canvas
    [SerializeField]
    GameObject unitList1, unitList2, mapList;


    [SerializeField]
    UnitSpawnInfo spawnData;

    [SerializeField]
    GameObject[] avaUnits = new GameObject[8];

    [SerializeField]
    GameObject noUnitsErrorMsg;
    [SerializeField]
    GameObject noMapErrorMsg;
    [SerializeField]
    Text mapTextCheckForError;
    [SerializeField]
    Button closeErrorButton;
    [SerializeField]
    Button closeMapErrorButton;


    public int placeInData, indexOfUnit;
    public bool isPlayer1; //player 1 = true player2 = false

    [SerializeField]
    GameObject unitDescription1, unitDescription2;
    [SerializeField]
    Text hoverUnit1Name, hoverUnit2Name, hoverUnit1desc, hoverUnit2desc;
    [SerializeField]
    Image hoverUnit1Icon, hoverUnit2Icon;


    [SerializeField]
    Dropdown mapSizeDropdown, mapTypeDropdown;
    [SerializeField]
    MapGeneratorInfo mapData;
    [SerializeField]
    Text mapSizeText, gameDiffText;
    [SerializeField]
    RawImage modeImage;
    [SerializeField]
    Texture noviceModeImg, easyModeImg, hardModeImage;
    public int desiredMapSize;
    public string desiredMapType;
    #endregion
    
    // Start is called before the first frame update
    // Adds OnClick events to each button and initialize the labels inside the units' images to be blank
    void Start()
    {
        //Initialize labels to be blank
        for (int i = 0; i < player1UnitNames.Length; i++)
        {
            player1UnitNames[i].text = "";
        }
        for (int i = 0; i < player2UnitNames.Length; i++)
        {
            player2UnitNames[i].text = "";
        }

        for (int i = 0; i < player1UnitNames.Length; i++)
        {
            player1UnitImages[i].sprite = null;
        }
        for (int i = 0; i < player2UnitNames.Length; i++)
        {
            player2UnitImages[i].sprite = null;
        }

        mapSizeText.text = "Unallocated memory";
        gameDiffText.text = "Mode not selected";

        //Initialize events
        startButton.onClick.AddListener(OnStartButtonClick);
        mapButton.onClick.AddListener(OnmapButtonClick);

        p1u1.onClick.AddListener(Onp1u1Click);
        p1u2.onClick.AddListener(Onp1u2Click);
        p1u3.onClick.AddListener(Onp1u3Click);
        p1u4.onClick.AddListener(Onp1u4Click);
        p2u1.onClick.AddListener(Onp2u1Click);
        p2u2.onClick.AddListener(Onp2u2Click);
        p2u3.onClick.AddListener(Onp2u3Click);
        p2u4.onClick.AddListener(Onp2u4Click);

        closeUnit1List.onClick.AddListener(OnCloseUnit1ListClick);
        closeUnit2List.onClick.AddListener(OnCloseUnit2ListClick);

        closeMapList.onClick.AddListener(OnCloseMapListClick);

        closeErrorButton.onClick.AddListener(OnCloseErrorButton);
        closeMapErrorButton.onClick.AddListener(OnCloseMapErrorButton);

        //clear selected units arrays and creating new one
        spawnData.Player1units = null;
        spawnData.Player1units = new GameObject[4];
        spawnData.Player2units = null;
        spawnData.Player2units = new GameObject[4];
    }


    public bool getIfBusy() { return this.busy; }

    #region General UI methods
    public void OnStartButtonClick()
    {
        if (!busy)
        {

            if ((SpawnListChecker(spawnData.Player1units)!=4) || (SpawnListChecker(spawnData.Player2units) != 4)||mapTextCheckForError.text== "Unallocated memory")
            {
                if(!busy&&!error)
                {
                    if (mapTextCheckForError.text == "Unallocated memory")
                    {
                        noMapErrorMsg.SetActive(true);
                        busy = true;
                        error = true;
                    }
                    else
                    {
                        noUnitsErrorMsg.SetActive(true);
                        busy = true;
                        error = true;
                    }
                }
            }
            else
            {
                SceneManager.UnloadScene(2);
                SceneManager.LoadScene(3);
                
            }
        }
    }

    public void OnCloseErrorButton()
    {
        if(busy&&error)
        {
            noUnitsErrorMsg.SetActive(false);
            busy = false;
            error = false;

        }
    }
    public void OnCloseMapErrorButton()
    {
        if (busy && error)
        {
            noMapErrorMsg.SetActive(false);
            busy = false;
            error = false;

        }
    }
    #endregion

    #region Player 1 unit selection
    //player 1 unit selection buttons
    public void  Onp1u1Click()
    {
        if (!busy)
        {
            unitList1.SetActive(true);

            //ifP1u1 = true;
            busy = true;
        }
    }

    public void  Onp1u2Click()
    {
        if (!busy)
        {
            unitList1.SetActive(true);
            //ifP1u2 = true;
            busy = true;
        }
    }

    public void  Onp1u3Click()
    {
        if (!busy)
        {
            unitList1.SetActive(true);
            //ifP1u3 = true;
            busy = true;

        }

    }

    public void  Onp1u4Click()
    {
        if (!busy)
        {
            unitList1.SetActive(true);
            //ifP1u4 = true;
            busy = true;
        }

    }

    public void ShowP1UnitDescription(int indexUnit)
    {
        this.indexOfUnit = indexUnit;
        unitDescription1.SetActive(true);
        hoverUnit1Name.text=avaUnits[indexUnit].GetComponent<Unit>().getUnitName();
        hoverUnit1desc.text = avaUnits[indexUnit].GetComponent<Unit>().getFlavorText();
        hoverUnit1Icon.sprite= avaUnits[indexOfUnit].GetComponent<Unit>().GetIcon();

    }
    public void HideP1UnitDescription()
    {
        unitDescription1.SetActive(false);

    }


    public void OnCloseUnit1ListClick()
    {
        if (busy)
        {
            unitList1.SetActive(false);
            busy = false;
            ifP1u1 = false;
            ifP1u2 = false;
            ifP1u3 = false;
            ifP1u4 = false;
        }
    }
    #endregion

    #region List Building
    public void ChangeDataPlace(int place)
    {
        this.placeInData = place;
    }
    public void ChangePlayer(bool playerchoose)
    {
        isPlayer1 = playerchoose; //player1 = true player2 = false;
    }
    public void ChangeIndexOfUnit(int indexUnit)
    {
        indexOfUnit = indexUnit;
        
        if (isPlayer1)
        {
            spawnData.Player1units[placeInData] = avaUnits[indexUnit];
            player1UnitNames[placeInData].text = avaUnits[indexOfUnit].GetComponent<Unit>().getUnitName();
            player1UnitImages[placeInData].sprite = avaUnits[indexOfUnit].GetComponent<Unit>().GetIcon();
            player1UnitImages[placeInData].color = Color.white;
            
        }
        else
        {
            spawnData.Player2units[placeInData] = avaUnits[indexUnit];
            player2UnitNames[placeInData].text = avaUnits[indexOfUnit].GetComponent<Unit>().getUnitName();
            player2UnitImages[placeInData].sprite = avaUnits[indexOfUnit].GetComponent<Unit>().GetIcon();
            player2UnitImages[placeInData].color = Color.white;
        }
    }

    public int SpawnListChecker(GameObject[] gameObjects)
    {
        int count = 0;
        for (int i = 0; i < gameObjects.Length; i++)
            if (gameObjects[i] != null)
                count++;
        return count;
    }


    public void OnP1AvaUnitButtonClick()
    {
        //if (busy && ifP1u1)
        //{
        ////this.player1UnitNames[placeInData].text = avaUnits[indexOfUnit].GetComponent<Unit>().getUnitName();
        ////this.player1UnitNames[0].text = "maniak";
        //    unitList1.SetActive(false);
        //    busy = false;
        //    ifP1u1 = false;
        //    ifP1u2 = false;
        //    ifP1u3 = false;
        //    ifP1u4 = false;
        //}
    }

    public void OnP2AvaUnitButtonClick()
    {
        //if (busy && ifP1u1)
        //{
        ////    this.player1UnitNames[placeInData].text = avaUnits[indexOfUnit].GetComponent<Unit>().getUnitName();
        ////    this.player1UnitNames[0].text = "maniak";
        //    unitList1.SetActive(false);
        //    busy = false;
        //    ifP1u1 = false;
        //    ifP1u2 = false;
        //    ifP1u3 = false;
        //    ifP1u4 = false;
        //}

    }
    #endregion

    #region Player 2 unit selection
    //player 2 unit selection buttons

    public void  Onp2u1Click()
    {
        if (!busy)
        {
            unitList2.SetActive(true);
            busy = true;
            ifP2u1 = true;
        }
    }

    public void  Onp2u2Click()
    {
        if (!busy)
        {
            unitList2.SetActive(true);
            busy = true;
            ifP2u2 = true;
        }
    }

    public void  Onp2u3Click()
    {
        if (!busy)
        {
            unitList2.SetActive(true);
            busy = true;
            ifP2u3 = true;

        }
    }

    public void  Onp2u4Click()
    {
        if (!busy)
        {
            unitList2.SetActive(true);
            busy = true;
            ifP2u4 = true;

        }
    }

    public void ShowP2UnitDescription(int indexUnit)
    {
        this.indexOfUnit = indexUnit;
        unitDescription2.SetActive(true);
        hoverUnit2Name.text = avaUnits[indexUnit].GetComponent<Unit>().getUnitName();
        hoverUnit2desc.text = avaUnits[indexUnit].GetComponent<Unit>().getFlavorText();
        hoverUnit2Icon.sprite = avaUnits[indexOfUnit].GetComponent<Unit>().GetIcon();

    }
    public void HideP2UnitDescription()
    {
        unitDescription2.SetActive(false);
    }


    public void OnCloseUnit2ListClick()
    {
        if (busy)
        {
            unitList2.SetActive(false);
            busy = false;
            ifP2u1 = false;
            ifP2u2 = false;
            ifP2u3 = false;
            ifP2u4 = false;

        }
    }

    #endregion

    #region Map selection
    public void OnmapButtonClick()
    {
        if (!busy)
        {
            mapList.SetActive(true);
            busy = true;
        }
    }

    public void ChangeMapSize(int mapSize)
    {
        this.desiredMapSize = mapSize;
    }

    public void OnCloseMapListClick()
    {
        if (busy)
        {

            switch(mapSizeDropdown.value)
            {
                case 0:
                    {
                        mapData.mapSizeX = 12;
                        mapData.mapSizeY = 12;
                        mapSizeText.text = "Size: 12*12";
                    }
                    break;
                case 1:
                    {
                        mapData.mapSizeX = 16;
                        mapData.mapSizeY = 16;
                        mapSizeText.text = "Size: 16*16";
                    }
                    break;
                case 2:
                    {
                        mapData.mapSizeX = 20;
                        mapData.mapSizeY = 20;
                        mapSizeText.text = "Size: 20*20";

                    }
                    break;
                default:
                    {
                        mapData.mapSizeX = 18;
                        mapData.mapSizeY = 18;
                        mapSizeText.text = "Size: 18*18";
                    }
                    break;
            }

            //if (mapSizeDropdown.value == 0)
            //{
            //    mapData.mapSizeX = 10;
            //    mapData.mapSizeY = 10;
            //}
            //else if (mapSizeDropdown.value == 1)
            //{
            //    mapData.mapSizeX = 18;
            //    mapData.mapSizeY = 18;
            //}
            //else if (mapSizeDropdown.value == 2)
            //{
            //    mapData.mapSizeX = 18;
            //    mapData.mapSizeY = 18;
            //}

            switch(mapTypeDropdown.value)
            {
                case 0:
                    {
                        mapData.gameType = "novice";
                        gameDiffText.text = "Novice Mode";
                        modeImage.texture = noviceModeImg;
                    }
                    break;
                case 1:
                    {
                        mapData.gameType = "easy";
                        gameDiffText.text = "Normal Mode";
                        modeImage.texture = easyModeImg;
                    }
                    break;
                case 2:
                    {
                        mapData.gameType = "hard";
                        gameDiffText.text = "Hard Mode";
                        modeImage.texture = hardModeImage;
                    }
                    break;
                default:
                    {
                        mapData.gameType = "novice";
                        gameDiffText.text = "Debug Mode";
                    }
                    break;
            }
            //if (mapTypeDropdown.value == 0)
            //    mapData.gameType = "novice";
            //else if (mapTypeDropdown.value == 1)
            //    mapData.gameType = "easy";

            mapList.SetActive(false);
            busy = false;
        }

    }
    #endregion

    // Update is called once per frame
    void Update()
    {
        if (busy)
        {
            startButton.interactable=false;
        }
        else
        {
            startButton.interactable = true;

        }
    } 
}
