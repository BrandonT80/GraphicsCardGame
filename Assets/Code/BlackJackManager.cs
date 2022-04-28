using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackJackManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject MainSceneManager;
    public MainSceneManager MSM;
    public Transform BlackJackMenu;
    public UnityEngine.UI.Text NotificationText;
    public UnityEngine.UI.Image BetInputBackground;
    public UnityEngine.UI.Text BetInputText;
    public UnityEngine.UI.Image BetButton;
    public UnityEngine.UI.Text BetButtonText;
    public GameObject player; 
    public GameObject ai1;
    public GameObject ai2;
    public GameObject dealer;
    public GameObject gamer;
    private UnityEngine.UI.Image hitButton;
    private UnityEngine.UI.Image standButton;
    private UnityEngine.UI.Text hitText;
    private UnityEngine.UI.Text standText;

    private UnityEngine.UI.Text quitText;

    private UnityEngine.UI.Text replayText;

    private UnityEngine.UI.Image replayButton;

    private UnityEngine.UI.Image quitButton;
    public  int usersBet;

    private Vector3 originalPosition;

    private Vector3 originalDealerPos;

    private Vector3 originalRightPos;

    private Vector3 originalLeftPos;

    private List<GameObject> tempPositions = new List<GameObject>();

    public Transform deckPos;
    public Dictionary<string, int>My_dict=new Dictionary<string, int>(){ 
            {"K", 10}, {"J", 10}, {"Q", 10}, {"10", 10}, {"9", 9}, {"8", 8}, {"7", 7}, {"6", 6}, {"5", 5}, {"4", 4}, {"3", 3}, {"2", 2} };

    void Start(){
        MSM = MainSceneManager.GetComponent<MainSceneManager>();
        BlackJackMenu = GameObject.Find("Canvas").transform.Find("BlackJackMenu").transform;
        NotificationText = BlackJackMenu.Find("NotificationText").GetComponent<UnityEngine.UI.Text>();
        BetInputBackground = BlackJackMenu.Find("BetInput").GetComponent<UnityEngine.UI.Image>();
        BetInputText = BlackJackMenu.Find("BetInput").Find("Text").GetComponent<UnityEngine.UI.Text>();
        Debug.Log("1BET: " + BetInputText);
        BetButton = BlackJackMenu.Find("Button").GetComponent<UnityEngine.UI.Image>();
        BetButtonText = BlackJackMenu.Find("Button").Find("Text").GetComponent<UnityEngine.UI.Text>();

        hitButton = BlackJackMenu.Find("PickPanel").Find("HitButton").GetComponent<UnityEngine.UI.Image>();
        hitText = BlackJackMenu.Find("PickPanel").Find("HitButton").Find("Text").GetComponent<UnityEngine.UI.Text>();

        standButton = BlackJackMenu.Find("PickPanel").Find("StandButton").GetComponent<UnityEngine.UI.Image>();
        standText = BlackJackMenu.Find("PickPanel").Find("StandButton").Find("Text").GetComponent<UnityEngine.UI.Text>();

        quitButton = BlackJackMenu.Find("QuitButton").GetComponent<UnityEngine.UI.Image>();
        quitText = BlackJackMenu.Find("QuitButton").Find("Text").GetComponent<UnityEngine.UI.Text>();

        replayButton = BlackJackMenu.Find("ReplayButton").GetComponent<UnityEngine.UI.Image>();
        replayText = BlackJackMenu.Find("ReplayButton").Find("Text").GetComponent<UnityEngine.UI.Text>();
    }

    public void getUserBet(){
        Debug.Log("BET: " + BetInputText);
        try
        {
            Debug.Log("BET: " + BetInputText.text);
            usersBet = int.Parse(BetInputText.text);
        }catch{
            Debug.Log("BET: " + usersBet);
            usersBet = 0;
        }
        if(usersBet >= 1 && usersBet <= 1001){
            disableBetUI();
            NotificationText.text = "Thank you. Your bet is $" + usersBet + ".";
            MSM.spendMoney(usersBet);
            StartCoroutine(delay());
        }else{
            Debug.Log("BET: " + usersBet);
            NotificationText.text = "Please enter a valid bet";
        }
    }


    void QuickRoll(GameObject card){
        card.transform.rotation = Quaternion.Euler(card.transform.rotation.eulerAngles.x,
                                                    card.transform.rotation.eulerAngles.y,
                                                    card.transform.rotation.eulerAngles.z + 180);
    }
    IEnumerator Roll(GameObject card){
        float rotSpeed = 300.0f;
        float remainingAngle = 180.0f;

        Vector3 right = new Vector3(0.03f, 0, 0);

        right = Vector3.right * 0.03f;

        Vector3 rotationCenter = card.transform.position + right;

        while(remainingAngle > 0){
            float rotationAngle = Mathf.Min(Time.deltaTime*rotSpeed, remainingAngle);
            
            card.transform.RotateAround(rotationCenter, Vector3.back, rotationAngle);

            remainingAngle -= rotationAngle;
            yield return null;
        }

        float transSpeed = 0.25f;
        float remainingOffset = 0.05f;

        while(remainingOffset > 0){
            float offset = Mathf.Min(Time.deltaTime*transSpeed, remainingOffset);
            Vector3 rt = new Vector3(0,0,0);
            card.transform.position -= new Vector3(offset, 0, 0);
            remainingOffset -= offset;
            yield return null;
        }
    }

    IEnumerator delay(){
        yield return new WaitForSeconds(1.0f);
        NotificationText.text = "Shuffling Cards...";
        
        yield return new WaitForSeconds(1.0f);
        NotificationText.text = "Dealing cards...";
        MSM.dealAll();
        yield return new WaitForSeconds(9.0f);
        originalPosition = MSM.tableCode.playerLocations[1].transform.position;
        originalDealerPos = MSM.tableCode.dealerLocations[0].transform.position;
        originalRightPos = MSM.tableCode.player3Locations[1].transform.position;
        originalLeftPos = MSM.tableCode.player2Locations[1].transform.position;


        CardPrefabCode[] dealerCards = dealer.GetComponentsInChildren<CardPrefabCode>();
        StartCoroutine(Roll(dealerCards[0].gameObject));
        flipAllUserCards();

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

        if(gamer != dealer){
            for(int i=0;i<cards.Length;i++){
                //StartCoroutine(Roll(cards[i].gameObject));
                QuickRoll(cards[i].gameObject);
            }
        }else{
            StartCoroutine(Roll(cards[1].gameObject));
        }

        while(showing<17){
            //Here you will draw the AI a new card. You are going to need make it where the cards are being dealt to the right position.
            //It should just be the same as count though.
            //You might want to do something to denote that the AI is hitting.
            if(gamer==dealer){
                
                Vector3 temp = MSM.tableCode.dealerLocations[0].transform.position + new Vector3(-0.09f,0,0);
                GameObject tempGO = new GameObject();
                tempGO.transform.position = temp;
                tempGO.transform.rotation = MSM.tableCode.dealerLocations[0].transform.rotation;


                MSM.createPhysicalCard(tempGO, MSM.deck.drawCards(1, 's')[0], 0);
                cards = gamer.GetComponentsInChildren<CardPrefabCode>();
                StartCoroutine(HitCardRoll(cards[cards.Length-1].gameObject));

                MSM.tableCode.dealerLocations[0].transform.position = temp;

                tempPositions.Add(tempGO);
            }
            if(gamer==ai1){
                Vector3 temp = MSM.tableCode.player2Locations[1].transform.position + new Vector3(0.025f,0.001f,-0.04f);
                GameObject tempGO = new GameObject();
                tempGO.transform.position = temp;
                tempGO.transform.rotation = MSM.tableCode.player2Locations[1].transform.rotation;

                
                MSM.createPhysicalCard(tempGO, MSM.deck.drawCards(1, 's')[0], 2);
                cards = gamer.GetComponentsInChildren<CardPrefabCode>();
                StartCoroutine(HitAIRoll(cards[cards.Length-1].gameObject));

                MSM.tableCode.player2Locations[1].transform.position = temp;
                tempPositions.Add(tempGO);
            }
            if(gamer==ai2){
                Vector3 temp = MSM.tableCode.player3Locations[1].transform.position + new Vector3(-0.025f,0.001f,-0.04f);
                GameObject tempGO = new GameObject();
                tempGO.transform.position = temp;
                tempGO.transform.rotation = MSM.tableCode.player3Locations[1].transform.rotation;

                MSM.createPhysicalCard(tempGO, MSM.deck.drawCards(1, 's')[0], 3);
                cards = gamer.GetComponentsInChildren<CardPrefabCode>();
                StartCoroutine(HitAIRoll(cards[cards.Length-1].gameObject));

                MSM.tableCode.player3Locations[1].transform.position = temp;
                tempPositions.Add(tempGO);
            }
            
            showing=calchand(cards);
            count++;
        }

        if(showing>21){
            //Something to denote that this AI busted.
            Debug.Log("AI Busted "+showing);
        }

        if(gamer==dealer){
            CardPrefabCode[] pcards = player.GetComponentsInChildren<CardPrefabCode>();
            int handval=calchand(pcards);
            if(handval>21){
                //Something to denote that the player lost because they busted on their turn.
                NotificationText.text = "You busted. You lose.";
            }
            else if(handval==21 && pcards.Length==2){
                if(showing==21 && cards.Length==2){
                    //Something to denote the players BlackJack and the dealers BlackJack cancel out.
                    NotificationText.text = "You both got BlackJack. It's a tie.";
                    MSM.addMoney(usersBet);
                }
                else{
                    //Something to denote that the player won because of a BlackJack.
                    NotificationText.text = "You won $" + usersBet + "!";
                    MSM.addMoney((usersBet*3)/2);
                }
            }
            else if(showing==21 && cards.Length==2){
                //Something to denote that the dealers BlackJack won.
                NotificationText.text = "Dealer got BlackJack. You lost $" + usersBet + ".";
            }
            else if(handval==showing){
                //Something to denote that the player tied with the dealer.
                MSM.addMoney(usersBet);
                NotificationText.text = "You tied with the dealer. You get your money back.";
                Debug.Log("Tied with the dealer.");
            }
            else if(showing>21){
                //Something to denote that the dealer busted, and the player wins.
                MSM.addMoney(2*usersBet);
                NotificationText.text = "The dealer busted. You win!";
            }
            else if(showing<handval){
                //Something  to denote that the player beat the dealer.
                NotificationText.text = "You beat the dealer. You win!";
                MSM.addMoney(2*usersBet);
            }else if(handval<showing){
                //Something to denote that the player lost to the dealer.
                NotificationText.text = "You lost to the dealer. You lost $" + usersBet + ".";
                Debug.Log("New route");
            }
            //You will probalbly set up a screen here to ask the player if he wants to play again.


            StartCoroutine(delayAskPlayAgain());
            
        }
    }

    IEnumerator delayAskPlayAgain(){
        yield return new WaitForSeconds(5.0f);
        // Play again
        NotificationText.text = "Would you like to play again?";

        MSM.tableCode.playerLocations[1].transform.position = originalPosition; // reset the card position 
        MSM.tableCode.dealerLocations[0].transform.position = originalDealerPos; // reset the card position
        MSM.tableCode.player3Locations[1].transform.position = originalRightPos;
        MSM.tableCode.player2Locations[1].transform.position = originalLeftPos;
        foreach(GameObject i in tempPositions){
            Destroy(i);
        }


        removeOldCards(player);
        removeOldCards(dealer);
        removeOldCards(ai1);
        removeOldCards(ai2);

        StartCoroutine(enableQuitReplay());
    }

    void removeOldCards(GameObject plr){
        CardPrefabCode[] pcards = plr.GetComponentsInChildren<CardPrefabCode>();

        for(int i = 0; i < pcards.Length; i++){
            //Destroy(pcards[i].gameObject);
            QuickRoll(pcards[i].gameObject);
            StartCoroutine(removeAnimation(pcards[i].gameObject));
        }
    }

    IEnumerator removeAnimation(GameObject c){
        yield return new WaitForSeconds(0.5f);
        Vector3 targetPos = deckPos.position;
        Vector3 targetRot = deckPos.rotation.eulerAngles;
    
        float transSpeed = 1f;

        float distX = targetPos.x - c.transform.position.x;
        float distY = targetPos.y - c.transform.position.y;
        float distZ = targetPos.z - c.transform.position.z;

        float d = Vector3.Distance(c.transform.position, targetPos);

        while(d > 0.0001f){

            distX = targetPos.x - c.transform.position.x;
            distY = targetPos.y - c.transform.position.y;
            distZ = targetPos.z - c.transform.position.z;

            float offsetX = Mathf.Min(Time.deltaTime*transSpeed, Mathf.Abs(distX));
            float offsetY = Mathf.Min(Time.deltaTime*transSpeed, Mathf.Abs(distY));
            float offsetZ = Mathf.Min(Time.deltaTime*transSpeed, Mathf.Abs(distZ));

            if(distX < 0){
                offsetX *= -1.0f;
            }
            if(distY < 0){
                offsetY *= -1.0f;
            }
            if(distZ < 0){
                offsetZ *= -1.0f;
            }

            c.transform.position += new Vector3(offsetX, offsetY, offsetZ);
            d = Vector3.Distance(c.transform.position, targetPos);
            
            yield return null;
        }
        yield return new WaitForSeconds(1f);
        Destroy(c);
    }

    void flipAllUserCards(){
        CardPrefabCode[] cards = player.GetComponentsInChildren<CardPrefabCode>();

        for(int i = 0; i < cards.Length; i++){
           StartCoroutine(Roll(cards[i].gameObject));
        }
    }

    private void chooseMove(){
        enabledMenuUI();
    }

    IEnumerator HitCardRoll(GameObject c){
        yield return new WaitForSeconds(1.1f);
        StartCoroutine(Roll(c));
    }

    IEnumerator HitAIRoll(GameObject c){
        yield return new WaitForSeconds(1.1f);
        QuickRoll(c);
    }

    public void playerHit(){
        disabledMenuUI();
        NotificationText.text = "You chose to hit.";
        //You will need to make more placements for the cards to be put into as well as the animation.
        //So that this part will always place the new card in the next location.

        Vector3 offset = new Vector3(0.025f,0.0001f,-0.035f); // Diagonal offset of card
        MSM.tableCode.playerLocations[1].transform.position = MSM.tableCode.playerLocations[1].transform.position + offset;

        MSM.createPhysicalCard(MSM.tableCode.playerLocations[1], MSM.deck.drawCards(1, 's')[0], -1);
        CardPrefabCode[] cards = player.GetComponentsInChildren<CardPrefabCode>();

        StartCoroutine(HitCardRoll(cards[cards.Length-1].gameObject));
        

        int handval=calchand(cards);

        if(handval>21){
            //Something to denote that the player busted.
            NotificationText.text = "Bust. You lose.";
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

    private IEnumerator enableQuitReplay(){
        yield return new WaitForSeconds(2f);
        quitButton.enabled = true;
        quitText.enabled = true;
        replayButton.enabled = true;
        replayText.enabled = true;
    }

    public void disableQuitReplay(){
        NotificationText.enabled = false;
        quitButton.enabled = false;
        quitText.enabled = false;
        replayButton.enabled = false;
        replayText.enabled = false;
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
        disableQuitReplay();
        NotificationText.enabled = true;
        NotificationText.text = "Welcome to Black Jack!";
        enableBetUI();

        MSM.deck.shuffleDeck();
        MSM.deck.shuffleDeck();
        MSM.deck.shuffleDeck();
        MSM.deck.shuffleDeck();
    }

    public void replaySelected(){
        //beginBlackJack();
        MSM.beginBlackJack();
    }

    public void quitSelected(){
        Debug.Log("QUIT");
    }

    // Update is called once per frame
    void Update(){
        
    }
}
