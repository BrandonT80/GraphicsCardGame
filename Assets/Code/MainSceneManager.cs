using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSceneManager : MonoBehaviour
{
    public Camera mainCamera;
    public Canvas mainCanvas;
    private Transform centerPanel;
    public int money = 1000;
    // Start is called before the first frame update
    void Start()
    {
        addMoney(0);
        centerPanel = GameObject.Find("Canvas").transform.Find("CenterPanel").transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void startBlackJack()
    {
        removePick();
        centerPanel.Find("TopText").GetComponent<UnityEngine.UI.Text>().text = "BlackJack Rules:";
        centerPanel.Find("BlackJackRules").GetComponent<UnityEngine.UI.Text>().enabled = true;
    }

    public void playGame()
    {
        removePlay();
        centerPanel.Find("BlackJackRules").GetComponent<UnityEngine.UI.Text>().enabled = false;
        centerPanel.Find("SlotRules").GetComponent<UnityEngine.UI.Text>().enabled = false;
    }

    public void startSlots()
    {
        removePick();
        centerPanel.Find("TopText").GetComponent<UnityEngine.UI.Text>().text = "Slot Rules:";
        centerPanel.Find("SlotRules").GetComponent<UnityEngine.UI.Text>().enabled = true;
    }

    private void removePick()
    {
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
        mainCanvas.transform.Find("PlayButton").GetComponent<UnityEngine.UI.Image>().enabled = false;
        mainCanvas.transform.Find("PlayButton").GetComponent<UnityEngine.UI.Button>().enabled = false;
        mainCanvas.transform.Find("PlayButton").GetComponentInChildren<UnityEngine.UI.Text>().enabled = false;


        centerPanel.Find("TopText").GetComponent<UnityEngine.UI.Text>().enabled = false;
        centerPanel.Find("TopTextPanel").GetComponent<UnityEngine.UI.Image>().enabled = false;
        mainCanvas.transform.Find("CenterPanel").GetComponent<UnityEngine.UI.Image>().enabled = false;
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
}
