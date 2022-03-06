using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    private string cardValue = "";      //String tha will hold the cards value
    private char cardSuit;							//Char that will hold the cards suit

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Card()
    {
        cardValue = "";     //Set default values
        cardSuit = 'E';		//Set default values - E for suits for "error"
    }

    public Card(string value, char suit)
    {
        this.cardValue = value;     //Set cardValue to user defined value
        this.cardSuit = suit;		//Set cardSuit to user defined suit
    }

    /**
	* Description: Copy Constructor - Creates a single card by copying another
	* @param Card object as object2
	* @return Nothing
	* @throws Nothing is implemented
	*/
    public Card(Card card2)
    {
        this.cardValue = card2.getCardValue();     //Copy cardValue from the parameter Card object
        this.cardSuit = card2.getCardSuit();       //Copy cardSuit from the parameter Card object
    }

    /**
	* Description: Retrives card value
	* @param None
	* @return cardValue as String
	* @throws Nothing is implemented
	*/
    public string getCardValue()
    {
        return cardValue;               //Return the value of the card
    }

    /**
	* Description: Retrives card suit
	* @param None
	* @return cardSuit as char
	* @throws Nothing is implemented
	*/
    public char getCardSuit()
    {
        return cardSuit;                //Return the suit of the card
    }

    /**
	* Description: Retrives the Card as an object
	* @param None
	* @return copy of this card object
	* @throws Nothing is implemented
	*/
    public Card getCard()
    {
        return new Card(this);          //Return a new instance of this card
    }

    /**
	* Description: Checks to see if two cards are equal
	* @param Card object to compare as object2
	* @return True if both cards are identical, False if both cards are not identical
	* @throws Nothing is implemented
	*/
    public bool equals(Card object2)
    {
        if (this.cardValue == object2.cardValue && this.cardSuit == object2.cardSuit)   //Verify is another cards suit and value is equal to this one
        {
            return true;    //If they are, return true
        }
        else
        {
            return false;   //If they are not, return false
        }
    }

    /**
	* Description: Prints the information of the card
	* @param None
	* @return String with the information of the card
	* @throws Nothing is implemented
	*/
    public string toString()
    {
        return "Card - Value: " + cardValue + " Suit: " + cardSuit;     //Return the card value and suit
    }
}
