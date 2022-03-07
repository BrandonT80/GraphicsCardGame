using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    //Iterator for use knowing where the current card is - when pulling from the deck
    public int topCard = 0;

    //Deck Object Array List of Cards
    public List<Card> cardDeck = new List<Card>();

    //13 Card Values/Types - String because of 10
    public readonly string[] CARD_VALUES = {"A","K","Q","J","10","9","8","7","6","5","4","3","2"};
	
	//4 Card Suits
	public readonly char[] CARD_SUITS = { 'S', 'H', 'D', 'C' };

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
                //Debug.Log("Before Add");
                cardDeck.Add(new Card(CARD_VALUES[i], CARD_SUITS[j]));  //For each card value, create 4 cards with different suits
                //Debug.Log("After Add");
                //Debug.Log(cardDeck[i].getCardValue());
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
        //Added note - This may fail if the deck copied is not full (52 cards)
        //Beware if you use this! Copied deck needs to be full! :)
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

    /**
	* Description: Shuffle Deck Method - Moves cards around inside the deck into a random order
	* @param None
	* @return Nothing
	* @throws Nothing is implemented
	*/
    public void shuffleDeck()
    {
        System.Random ran = new System.Random(); //Create a random object to use for random locations
        int randomLocation = 0;       //Randomize a number between 0 and 52 for the location 
        //For each element/card in the deck/arraylist, generate a random location/number, then remove the card and place it at the new location/number
        //Stores a copy of the card before removing
        Card tempCard = new Card();  //Make a copy of the current card - Used to temporarily store for use inserting later
        for (int i = 0; i < cardDeck.Count; i++)   //For each card in the deck
        {
            //tempCard;  //Make a copy of the current card - Used to temporarily store for use inserting later
            //System.Random rnd = new System.Random(); <- Random from C# not unity! - This is just the template to use
            //System.Random ran = new System.Random(); //Create a random object to use for random locations
            randomLocation = ran.Next(53);       //Randomize a number between 0 and 52 for the location 
            tempCard = cardDeck[i];
            cardDeck.Remove(cardDeck[i]);               //Remove the current card from the deck/array
            if (randomLocation >= cardDeck.Count)
            {
                //If its inserting at the back
                cardDeck.Add(tempCard);     //Insert the stored temorary card at the random location
            }
            else
            {
                cardDeck.Insert(randomLocation, tempCard);     //Insert the stored temorary card at the random location
            }
            setTopLoc(0);                               //Resets the top card to location 0 
        }
    }

    /**
	* Description: setTopLoc Method - Changes the iterator for the topCard to the specified location
	* @param resetLocation as int - Location user wants the topCard iterator to be at
	* @return Nothing
	* @throws Nothing is implemented
	*/
    public void setTopLoc(int resetLocation)
    {
        topCard = resetLocation;                //Reset the topCard iterator to the specified location
    }

    /**
	* Description: Checks to see if another Deck object is identical to this Deck object
	* @param Deck object to compare as object2
	* @return True if objects are identical, False if objects are not identical
	* @throws Nothing is implemented
	*/
    public bool equals(Deck object2)
    {
        for (int i = 0; i <= cardDeck.Count; i++)
        {
            if (i != cardDeck.Count)   //Checks to see if i is not at the full size of the deck yet
            {
                if (cardDeck[i].equals(object2.cardDeck[i]))    //Checks to see if the card at location i is equal to the card at the same location of the second deck
                {
                    continue;   //If the same, reiterate the loop
                }
                else
                {
                    return false;   //If not, end the method and return that the decks are not equal - False
                }
            }
            else
            {
                return true;        //If i is at the end of the deck and all cards are equal - return true - the decks are equal
            }
        }
        return false;               //If something goes wrong, return false
    }

    /**
	* Description: Prints the infomation of each card in the deck and its order. Also prints total cards
	* @param None
	* @return String including all cards and positions
	* @throws Nothing is implemented
	*/
    public string toString()
    {
        string returnString = "";
        //Print each card
        for (int i = 0; i < cardDeck.Count; i++)       //Loop through the deck and print each card
        {
            returnString += "\n" + (cardDeck[i].toString());
        }
        return returnString + "\nTotal Cards: " + cardDeck.Count;  //Print the total amount of cards, for debugging the shuffle() method
    }

    public Card[] drawCards(int x, char mode)
    {
        Card[] returnCards = new Card[x];
        switch(mode)
        {
            case 'r':
                //Remove from deck mode
                for(int i = 0; i < x; i++)
                {
                    returnCards[i] = cardDeck[0];
                    cardDeck.Remove(cardDeck[0]);
                }
                break;
            case 's':
                //Soft Use mode - Does not remove from the deck
                for (int i = 0; i < x; i++)
                {
                    returnCards[i] = cardDeck[topCard];
                    topCard++;
                }
                break;
        }
        return returnCards;
    }

    public Card drawCard(char mode)
    {
        Card returnCard = cardDeck[0];
        switch (mode)
        {
            case 'r':
                //Remove from deck mode
                cardDeck.Remove(cardDeck[0]);
                break;
            case 's':
                //Soft Use mode - Does not remove from the deck
                topCard++;
                break;
        }
        return returnCard;
    }

    public Card searchDrawRemove(Card c)
    {
        for(int i = 0; i < cardDeck.Count; i++)
        {
            if(cardDeck[i].equals(c))
            {
                cardDeck.Remove(cardDeck[i]);
                return c;
            }
        }
        return null;
    }

    public int searchIndex(Card c)
    {
        for (int i = 0; i < cardDeck.Count; i++)
        {
            if (cardDeck[i].equals(c))
            {
                return i;
            }
        }
        return -1;
    }

    public bool insertCard(Card c, char location)
    {
        if (location == 'b')
        {
            cardDeck.Add(c);
            return true;
        }
        else if (location == 'f')
        {
            cardDeck.Insert(0, c);
            return true;
        }
        else
            return false;
    }

    public bool insertCard(Card c, int location)
    {
        if(location < cardDeck.Count)
        {
            cardDeck.Insert(location, c);
            return true;
        }
        else
        {
            cardDeck.Add(c);
            return true;
        }
    }
}

