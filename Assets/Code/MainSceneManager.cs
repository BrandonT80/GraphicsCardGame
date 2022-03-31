using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSceneManager : MonoBehaviour
{
    public Camera mainCamera;
    public Canvas mainCanvas;
    private Transform centerPanel;
    public int money = 1000;

    //public GameObject deckPrefab;
    public GameObject cardPrefab;
    public GameObject deckObject;
    public Deck deck;
    public GameObject table;
    public Table tableCode;

    public bool nextCard = true;

    public bool dealing = false;
    public int dealNumber = 0;

    public GameObject[] playerGroups = new GameObject[6];

    // Start is called before the first frame update
    void Start()
    {
        addMoney(0);
        centerPanel = GameObject.Find("Canvas").transform.Find("CenterPanel").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (dealing)
        {
            if (nextCard)
            {
                switch (dealNumber)
                {
                    case 0:
                        createPhysicalCard(tableCode.dealerLocations[0], deck.drawCards(1, 's')[0], 0);
                        dealNumber++;
                        break;
                    case 1:
                        createPhysicalCard(tableCode.dealerLocations[1], deck.drawCards(1, 's')[0], 0);
                        dealNumber++;
                        break;
                    case 2:
                        createPhysicalCard(tableCode.player1Locations[1], deck.drawCards(1, 's')[0], 1);
                        dealNumber++;
                        break;
                    case 3:
                        createPhysicalCard(tableCode.player1Locations[0], deck.drawCards(1, 's')[0], 1);
                        dealNumber++;
                        break;
                    case 4:
                        createPhysicalCard(tableCode.player2Locations[1], deck.drawCards(1, 's')[0], 2);
                        dealNumber++;
                        break;
                    case 5:
                        createPhysicalCard(tableCode.player2Locations[0], deck.drawCards(1, 's')[0], 2);
                        dealNumber++;
                        break;
                    case 6:
                        createPhysicalCard(tableCode.playerLocations[1], deck.drawCards(1, 's')[0], -1);
                        dealNumber++;
                        break;
                    case 7:
                        createPhysicalCard(tableCode.playerLocations[0], deck.drawCards(1, 's')[0], -1);
                        dealNumber++;
                        break;
                    case 8:
                        createPhysicalCard(tableCode.player3Locations[0], deck.drawCards(1, 's')[0], 3);
                        dealNumber++;
                        break;
                    case 9:
                        createPhysicalCard(tableCode.player3Locations[1], deck.drawCards(1, 's')[0], 3);
                        dealNumber++;
                        break;
                    case 10:
                        createPhysicalCard(tableCode.player4Locations[0], deck.drawCards(1, 's')[0], 4);
                        dealNumber++;
                        break;
                    case 11:
                        createPhysicalCard(tableCode.player4Locations[1], deck.drawCards(1, 's')[0], 4);
                        dealNumber = 0;
                        dealing = false;
                        break;
                }
            }
        }
    }

    public void startBlackJack()
    {
        //Main Menu stuff
        removePick();
        centerPanel.Find("TopText").GetComponent<UnityEngine.UI.Text>().text = "BlackJack Rules:";
        centerPanel.Find("BlackJackRules").GetComponent<UnityEngine.UI.Text>().enabled = true;
    }

    public void playGame()
    {
        //Main Menu stuff
        removePlay();
        centerPanel.Find("BlackJackRules").GetComponent<UnityEngine.UI.Text>().enabled = false;
        centerPanel.Find("SlotRules").GetComponent<UnityEngine.UI.Text>().enabled = false;
    }

    public void startSlots()
    {
        //Main Menu stuff
        removePick();
        centerPanel.Find("TopText").GetComponent<UnityEngine.UI.Text>().text = "Slot Rules:";
        centerPanel.Find("SlotRules").GetComponent<UnityEngine.UI.Text>().enabled = true;
    }

    private void removePick()
    {
        //Main Menu stuff
        centerPanel.Find("PickPanel").GetComponent<UnityEngine.UI.Image>().enabled = false;
        centerPanel.Find("PickPanel").Find("PickText").GetComponent<UnityEngine.UI.Text>().enabled = false;
        centerPanel.Find("PickPanel").Find("BlackJackButton").GetComponent<UnityEngine.UI.Button>().enabled = false;
        centerPanel.Find("PickPanel").Find("BlackJackButton").GetComponent<UnityEngine.UI.Image>().enabled = false;
        centerPanel.Find("PickPanel").Find("BlackJackButton").GetComponentInChildren<UnityEngine.UI.Text>().enabled = false;
        centerPanel.Find("PickPanel").Find("SlotsButton").GetComponent<UnityEngine.UI.Button>().enabled = false;
        centerPanel.Find("PickPanel").Find("SlotsButton").GetComponent<UnityEngine.UI.Image>().enabled = false;
        centerPanel.Find("PickPanel").Find("SlotsButton").GetComponentInChildren<UnityEngine.UI.Text>().enabled = false;

        mainCanvas.transform.Find("PlayButton").GetComponent<UnityEngine.UI.Image>().enabled = true;
        mainCanvas.transform.Find("PlayButton").GetComponent<UnityEngine.UI.Button>().enabled = true;
        mainCanvas.transform.Find("PlayButton").GetComponentInChildren<UnityEngine.UI.Text>().enabled = true;

        Color c = mainCanvas.transform.Find("CenterPanel").GetComponent<UnityEngine.UI.Image>().color;
        c.a = 0.60f;
        mainCanvas.transform.Find("CenterPanel").GetComponent<UnityEngine.UI.Image>().color = c;
        //mainCanvas.transform.Find("TopText").GetComponent<UnityEngine.UI.Text>().text = "BlackJack Rules:";
    }

    private void removePlay()
    {
        //Main Menu stuff
        mainCanvas.transform.Find("PlayButton").GetComponent<UnityEngine.UI.Image>().enabled = false;
        mainCanvas.transform.Find("PlayButton").GetComponent<UnityEngine.UI.Button>().enabled = false;
        mainCanvas.transform.Find("PlayButton").GetComponentInChildren<UnityEngine.UI.Text>().enabled = false;


        centerPanel.Find("TopText").GetComponent<UnityEngine.UI.Text>().enabled = false;
        centerPanel.Find("TopTextPanel").GetComponent<UnityEngine.UI.Image>().enabled = false;
        mainCanvas.transform.Find("CenterPanel").GetComponent<UnityEngine.UI.Image>().enabled = false;

        beginBlackJack();
    }

    public void addMoney(int amount)
    {
        money += amount;
        mainCanvas.transform.Find("TopRightPanel").Find("Money").GetComponent<UnityEngine.UI.Text>().text = "Money: $" + money;
    }

    public void spendMoney(int amount)
    {
        money -= amount;
        mainCanvas.transform.Find("TopRightPanel").Find("Money").GetComponent<UnityEngine.UI.Text>().text = "Money: $" + money;
    }

    public void dealAll()
    {
        dealing = true;
    }

    public void beginBlackJack()
    {
        //deckObject = Instantiate(deckPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        deck = deckObject.GetComponent(typeof(Deck)) as Deck;
        //table = GameObject.Find("Table");
        tableCode = table.GetComponent(typeof(Table)) as Table;
        deck.shuffleDeck();
        deck.shuffleDeck();
        deck.shuffleDeck();
        deck.shuffleDeck();
        dealAll();
    }

    public void createPhysicalCard(GameObject location, Card c, int player)
    {
        nextCard = false;
        GameObject cardObj;
        //Instantiate(Object original, Vector3 position, Quaternion rotation, Transform parent);
        switch (player)
        {
            case -1:
                cardObj = Instantiate(cardPrefab, deckObject.transform.position, location.transform.rotation, playerGroups[1].transform);
                cardObj.GetComponent<CardPrefabCode>().setManager(this.gameObject);
                cardObj.GetComponent<CardPrefabCode>().setCard(c);
                cardObj.GetComponent<CardPrefabCode>().setLocation(location.transform);
                break;
            case 0:
                cardObj = Instantiate(cardPrefab, deckObject.transform.position, location.transform.rotation, playerGroups[0].transform);
                cardObj.GetComponent<CardPrefabCode>().setManager(this.gameObject);
                cardObj.GetComponent<CardPrefabCode>().setCard(c);
                cardObj.GetComponent<CardPrefabCode>().setLocation(location.transform);
                break;
            case 1:
                cardObj = Instantiate(cardPrefab, deckObject.transform.position, location.transform.rotation, playerGroups[2].transform);
                cardObj.GetComponent<CardPrefabCode>().setManager(this.gameObject);
                cardObj.GetComponent<CardPrefabCode>().setCard(c);
                cardObj.GetComponent<CardPrefabCode>().setLocation(location.transform);
                break;
            case 2:
                cardObj = Instantiate(cardPrefab, deckObject.transform.position, location.transform.rotation, playerGroups[3].transform);
                cardObj.GetComponent<CardPrefabCode>().setManager(this.gameObject);
                cardObj.GetComponent<CardPrefabCode>().setCard(c);
                cardObj.GetComponent<CardPrefabCode>().setLocation(location.transform);
                break;
            case 3:
                cardObj = Instantiate(cardPrefab, deckObject.transform.position, location.transform.rotation, playerGroups[4].transform);
                cardObj.GetComponent<CardPrefabCode>().setManager(this.gameObject);
                cardObj.GetComponent<CardPrefabCode>().setCard(c);
                cardObj.GetComponent<CardPrefabCode>().setLocation(location.transform);
                break;
            case 4:
                cardObj = Instantiate(cardPrefab, deckObject.transform.position, location.transform.rotation, playerGroups[5].transform);
                cardObj.GetComponent<CardPrefabCode>().setManager(this.gameObject);
                cardObj.GetComponent<CardPrefabCode>().setCard(c);
                cardObj.GetComponent<CardPrefabCode>().setLocation(location.transform);
                break;
        }
    }
}
