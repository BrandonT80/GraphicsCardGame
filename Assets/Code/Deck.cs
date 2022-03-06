using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    //Iterator for use knowing where the current card is - when pulling from the deck
    private int topCard = 0;

    //Deck Object Array List of Cards
    private List<Card> cardDeck = new List<Card>();

    //13 Card Values/Types - String because of 10
    private readonly string[] CARD_VALUES = {"A","K","Q","J","10","9","8","7","6","5","4","3","2"};
	
	//4 Card Suits
	private readonly char[] CARD_SUITS = { 'S', 'H', 'D', 'C' };

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /**
	* Description: No arg Constructor - Creates the deck
	* @param None
	* @return Nothing
	* @throws Nothing is implemented
	*/
    public Deck()
    {
        //Create the Deck in order by card value
        for (int i = 0; i < CARD_VALUES.Length; i++)
        {
            //Assign 4 different suits to each of the 13 card values
            for (int j = 0; j < CARD_SUITS.Length; j++)
            {
                cardDeck.Add(new Card(CARD_VALUES[i], CARD_SUITS[j]));  //For each card value, create 4 cards with different suits
            }
        }
    }

    /**
	* Description: Copy Constructor - Copies another Deck object's values to this object
	* @param Deck object to copy as object2
	* @return Nothing
	* @throws Nothing is implemented
	*/
    public Deck(Deck deck2)
    {
        for (int i = 0; i < 52; i++)
        {
            cardDeck.Add(new Card(deck2.cardDeck[i]));    //Copies parameter Deck object's values to this one
        }
    }

    /**
	* Description: Retrieves a copy of this deck
	* @param None
	* @return Copy of this Deck object
	* @throws Nothing is implemented
	*/
    public Deck getDeck()
    {
        return new Deck(this);  //Returns a copy of this deck instead of this deck itself
    }
}
