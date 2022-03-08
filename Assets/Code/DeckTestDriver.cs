using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckTestDriver : MonoBehaviour
{
    public GameObject deckPrefab;
    public bool userTesting = true;                 //Boolean for while loop
    public string userChoice = "D";         //String object for user input
    // Start is called before the first frame update
    void Start()
    {
        //Scanner keyboard = new Scanner(System.in);  //Scanner Object for user input

        //Deck deck = new Deck();                     //Deck object that will create 52 Card Objects
        //Deck deck = this.AddComponent<Deck>();
        //SphereCollider sc = gameObject.AddComponent<SphereCollider>() as SphereCollider;

        GameObject deckObject = Instantiate(deckPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        Deck deck = deckObject.GetComponent(typeof(Deck)) as Deck;

        while (userTesting)     //Loop for Testing
        {
            //System.out.println("\nPlease chose an option to test the Deck Class:" +
            //Debug.Log("\nP: Print Deck\nS: Shuffle Deck\nE: Exit");       //Ask user to pick an option
            //userChoice = keyboard.nextLine();                                           //Obtain user input
            //if (VerifyInput.verifyLetter(userChoice))                                   //Validate user input was a valid choice
            //{
                //switch (Character.toUpperCase(userChoice.charAt(0)))                        //Switch to users choice
                switch(userChoice.ToCharArray().GetValue(0))
                {
                    case 'P':                                   //User chose to print
                        //System.out.println(deck);               //Print the deck object that will print the 52 Card Objects
                        Debug.Log("P");
                        Debug.Log(deck.toString());
                        userTesting = false;
                        continue;                               //Reiterate the loop
                    case 'S':                                       //User chose to Shuffle the Deck
                        deck.shuffleDeck();                         //Shuffle the deck using the Deck Object Shuffle method
                        //System.out.println("\nShuffling...");       //Give user feedback that ths shuffling is happening
                        Debug.Log("\nShuffling...");
                        userChoice = "P";
                        continue;                                   //Reiterate the loop
                    case 'E':                                   //User chose to Exit
                        //System.out.println("\nExiting...");     //Give user feedback that the application is closing
                        Debug.Log("\nExiting...");
                        userTesting = false;                    //Change the userTesting boolean for the while loop to false
                        continue;                               //Reiterate the loop witht the false boolean that will terminate the loop
                    case 'D':                                       //User chose to Shuffle the Deck
                        deck.shuffleDeck();                         //Shuffle the deck using the Deck Object Shuffle method
                        //System.out.println("\nShuffling...");       //Give user feedback that ths shuffling is happening
                        Debug.Log("\nShuffling...");
                        
                        Debug.Log("\nRemoving 5...");
                    Debug.Log(deck.drawCards(5, 's'));
                    userChoice = "P";
                        continue;
                    default:
                        //System.out.println("\nYou entered the wrong letter, please try again.");    //Give user feedback that something went wrong
                        Debug.Log("\nYou entered the wrong letter, please try again.");
                        continue;                                                                   //Reiterate the loop
                }
            //}
            /*else
            {
                //System.out.println("\nYou entry was invalid, please try again.");   //Give user feedback that their entry was invalid
                Debug.Log("\nYou entered the wrong letter, please try again.");
                continue;                                                           //Reiterate the loop
            }*/
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
