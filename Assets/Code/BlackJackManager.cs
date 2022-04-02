using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackJackManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject MainSceneManager;
    private MainSceneManager MSM;
    private Transform BlackJackMenu;
    private UnityEngine.UI.Text NotificationText;
    private UnityEngine.UI.Image BetInputBackground;
    private UnityEngine.UI.Text BetInputText;
    private UnityEngine.UI.Image BetButton;
    private UnityEngine.UI.Text BetButtonText;
    public GameObject player;
    public GameObject dealer;
    private UnityEngine.UI.Image hitButton;
    private UnityEngine.UI.Image standButton;
    private UnityEngine.UI.Text hitText;
    private UnityEngine.UI.Text standText;

    void Start(){
        //Get script from main scene manager
        MSM = MainSceneManager.GetComponent<MainSceneManager>();
        // MSM.addMoney(100);
        // MSM.spendMoney(100);
        //MSM.dealAll();
        // MSM.beginBlackjack()
        BlackJackMenu = GameObject.Find("Canvas").transform.Find("BlackJackMenu").transform;
        NotificationText = BlackJackMenu.Find("NotificationText").GetComponent<UnityEngine.UI.Text>();
        BetInputBackground = BlackJackMenu.Find("BetInput").GetComponent<UnityEngine.UI.Image>();
        BetInputText = BlackJackMenu.Find("BetInput").Find("Text").GetComponent<UnityEngine.UI.Text>();
        BetButton = BlackJackMenu.Find("Button").GetComponent<UnityEngine.UI.Image>();
        BetButtonText = BlackJackMenu.Find("Button").Find("Text").GetComponent<UnityEngine.UI.Text>();

        hitButton = BlackJackMenu.Find("PickPanel").Find("HitButton").GetComponent<UnityEngine.UI.Image>();
        hitText = BlackJackMenu.Find("PickPanel").Find("HitButton").Find("Text").GetComponent<UnityEngine.UI.Text>();

        standButton = BlackJackMenu.Find("PickPanel").Find("StandButton").GetComponent<UnityEngine.UI.Image>();
        standText = BlackJackMenu.Find("PickPanel").Find("StandButton").Find("Text").GetComponent<UnityEngine.UI.Text>();
    }

    public void getUserBet(){
        int usersBet;
        try{
            usersBet = int.Parse(BetInputText.text);
        }catch{
            usersBet = 0;
        }

        if(usersBet >= 1 && usersBet <= 500){
            disableBetUI();
            NotificationText.text = "Thank you. Your bet is $" + usersBet + ".";
            StartCoroutine(delay());
        }else{
            NotificationText.text = "Please enter a valid bet";
        }
    }

    IEnumerator delay(){
        yield return new WaitForSeconds(3.0f);
        NotificationText.text = "Shuffling Cards...";

        yield return new WaitForSeconds(2.0f);
        NotificationText.text = "Dealing cards...";
        MSM.dealAll();

        yield return new WaitForSeconds(11.0f);
        NotificationText.text = "Review your cards and choose a move..";
        // Gets all Cards in Players hand
        chooseMove();
    }

    private void chooseMove(){
        CardPrefabCode[] cards = player.GetComponentsInChildren<CardPrefabCode>();
        CardPrefabCode[] dealerCards = dealer.GetComponentsInChildren<CardPrefabCode>();


        Debug.Log(cards[0].cardSuit + " " + cards[0].cardValue);
        Debug.Log(cards[1].cardSuit + " " + cards[1].cardValue);
        dealerCards[0].rightSideUpCard();

        enabledMenuUI();
    }

    private void enableBetUI(){
        NotificationText.text = "Please enter your bet";
        BetInputBackground.enabled = true;
        BetInputText.enabled = true;
        BetButton.enabled = true;
        BetButtonText.enabled = true;
    }

    public void playerHit(){
        disabledMenuUI();
        NotificationText.text = "You chose to hit.";
    }

    public void playerStand(){
        disabledMenuUI();
        NotificationText.text = "You chose to stand.";
    }
    private void disableBetUI(){
        BetInputBackground.enabled = false;
        BetInputText.enabled = false;
        BetButton.enabled = false;
        BetButtonText.enabled = false;
    }

    private void enabledMenuUI(){
        hitButton.enabled = true;
        standButton.enabled = true;
        hitText.enabled = true;
        standText.enabled = true;
    }

    private void disabledMenuUI(){
        hitButton.enabled = false;
        standButton.enabled = false;
        hitText.enabled = false;
        standText.enabled = false;
    }

    public void beginBlackJack(){
        NotificationText.text = "Welcome to Black Jack!";
        enableBetUI();
    }

    // Update is called once per frame
    void Update(){
        
    }
}
