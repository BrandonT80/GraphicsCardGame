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
    public GameObject ai1;
    public GameObject ai2;
    public GameObject dealer;
    public GameObject gamer;
    private UnityEngine.UI.Image hitButton;
    private UnityEngine.UI.Image standButton;
    private UnityEngine.UI.Text hitText;
    private UnityEngine.UI.Text standText;
    public  int usersBet;
    public Dictionary<string, int>My_dict=new Dictionary<string, int>(){ 
            {"K", 10}, {"J", 10}, {"Q", 10}, {"10", 10}, {"9", 9}, {"8", 8}, {"7", 7}, {"6", 6}, {"5", 5}, {"4", 4}, {"3", 3}, {"2", 2} };

    void Start(){
        MSM = MainSceneManager.GetComponent<MainSceneManager>();
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
        try{
            usersBet = int.Parse(BetInputText.text);
        }catch{
            usersBet = 0;
        }
        if(usersBet >= 1 && usersBet <= 500){
            disableBetUI();
            NotificationText.text = "Thank you. Your bet is $" + usersBet + ".";
            MSM.spendMoney(usersBet);
            StartCoroutine(delay());
        }else{
            NotificationText.text = "Please enter a valid bet";
        }
    }

    IEnumerator delay(){
        yield return new WaitForSeconds(1.0f);
        NotificationText.text = "Shuffling Cards...";

        yield return new WaitForSeconds(1.0f);
        NotificationText.text = "Dealing cards...";
        MSM.dealAll();

        yield return new WaitForSeconds(11.0f);
        NotificationText.text = "Review your cards and choose a move.."; 
        aiplay(ai1);
        CardPrefabCode[] cards = player.GetComponentsInChildren<CardPrefabCode>();
        Debug.Log(calchand(cards));
        if(calchand(cards)==21){
            //Something to denote the player got a blackjack.
            Debug.Log("You got BlackJack");
            aiplay(ai2);
            aiplay(dealer);
        }
        else{
            chooseMove();
        }
    }

    private void aiplay(GameObject gamer){
        CardPrefabCode[] cards = gamer.GetComponentsInChildren<CardPrefabCode>();
        int showing=calchand(cards);
        if(showing==21){
            //Something to denote that this AI got a BlackJack.
        }
        int count=2;
        while(showing<17){
            //Here you will draw the AI a new card. You are going to need make it where the cards are being dealt to the right position.
            //It should just be the same as count though.
            //You might want to do something to denote that the AI is hitting.
            if(gamer==dealer){
                MSM.createPhysicalCard(MSM.tableCode.dealerLocations[0], MSM.deck.drawCards(1, 's')[0], 0);
            }
            if(gamer==ai1){
                MSM.createPhysicalCard(MSM.tableCode.player2Locations[0], MSM.deck.drawCards(1, 's')[0], 2);
            }
            if(gamer==ai2){
                MSM.createPhysicalCard(MSM.tableCode.player3Locations[0], MSM.deck.drawCards(1, 's')[0], 3);
            }
            cards=gamer.GetComponentsInChildren<CardPrefabCode>();
            showing=calchand(cards);
            count++;
        }
        if(showing>21){
            //Something to denote that this AI busted.
            Debug.Log("Busted "+showing);
        }
        if(gamer==dealer){
            CardPrefabCode[] pcards = player.GetComponentsInChildren<CardPrefabCode>();
            int handval=calchand(pcards);
            if(handval>21){
                //Something to denote that the player lost because they busted on their turn.
            }
            else if(handval==21 && pcards.Length==2){
                if(showing==21 && cards.Length==2){
                    //Something to denote the players BlackJack and the dealers BlackJack cancel out.
                    MSM.addMoney(usersBet);
                }
                else{
                    //Something to denote that the player won because of a BlackJack.
                    MSM.addMoney((usersBet*3)/2);
                }
            }
            else if(showing==21 && cards.Length==2){
                //Something to denote that the dealers BlackJack won.
            }
            else if(handval==showing){
                //Something to denote that the player tied with the dealer.
                MSM.addMoney(usersBet);
                Debug.Log("Tied with the dealer.");
            }
            else if(showing>21){
                //Something to denote that the dealer busted, and the player wins.
                MSM.addMoney(2*usersBet);
            }
            else if(showing<handval){
                //Something  to denote that the player beat the dealer.
                MSM.addMoney(2*usersBet);
            }
            //You will probalbly set up a screen here to ask the player if he wants to play again.
        }
    }

    private void chooseMove(){
        enabledMenuUI();
    }

    public void playerHit(){
        disabledMenuUI();
        NotificationText.text = "You chose to hit.";
        //You will need to make more placements for the cards to be put into as well as the animation.
        //So that this part will always place the new card in the next location.
        MSM.createPhysicalCard(MSM.tableCode.playerLocations[1], MSM.deck.drawCards(1, 's')[0], -1);
        CardPrefabCode[] cards = player.GetComponentsInChildren<CardPrefabCode>();
        int handval=calchand(cards);
        if(handval>21){
            //Something to denote that the player busted.
            Debug.Log("You Busted.");
            disabledMenuUI();
            aiplay(ai2);
            aiplay(dealer);
            return;
        }
        chooseMove();
    }

    public void playerStand(){
        disabledMenuUI();
        NotificationText.text = "You chose to stand.";
        aiplay(ai2);
        aiplay(dealer);
    }

    public int calchand(CardPrefabCode[] cards){
        int ac=0;
        int handval=0;
        for(int i=0; i<cards.Length; i++){
            if(cards[i].cardValue=="A"){
                if(ac==1){
                    handval++;
                }
                else{
                    ac++;
                    handval+=11;
                }
            }
            else{
                handval+=My_dict[cards[i].cardValue];
            }
        }
        if(handval>21 && ac>0){
            ac--;
            handval-=10;
        }
        return handval;
    }

    private void enableBetUI(){
        NotificationText.text = "Please enter your bet";
        BetInputBackground.enabled = true;
        BetInputText.enabled = true;
        BetButton.enabled = true;
        BetButtonText.enabled = true;
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
