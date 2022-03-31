using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleSceneCode : MonoBehaviour
{
    public GameObject deckPrefab;
    public GameObject cardPrefab;
    public GameObject deckObject;
    public Deck deck;
    public GameObject table;
    public Table tableCode; 
    

    

    // Start is called before the first frame update
    void Start()
    {
        deckObject = Instantiate(deckPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        deck = deckObject.GetComponent(typeof(Deck)) as Deck;
        table = GameObject.Find("Table");
        tableCode = table.GetComponent(typeof(Table)) as Table;
        deck.shuffleDeck();
        deck.shuffleDeck();
        deck.shuffleDeck();
        deck.shuffleDeck();
        dealAll();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void dealAll()
    {
        for (int i = 0; i < tableCode.dealerLocations.Length; i++)
        {
            //Debug.Log(i);
            createPhysicalCard(tableCode.dealerLocations[i], deck.drawCards(1, 's')[0]);
        }
        for (int i = 0; i < tableCode.player1Locations.Length; i++)
        {
            //Debug.Log(i);
            createPhysicalCard(tableCode.player1Locations[i], deck.drawCards(1, 's')[0]);
        }
        for (int i = 0; i < tableCode.player2Locations.Length; i++)
        {
            //Debug.Log(i);
            createPhysicalCard(tableCode.player2Locations[i], deck.drawCards(1, 's')[0]);
        }
        for (int i = 0; i < tableCode.player3Locations.Length; i++)
        {
            //Debug.Log(i);
            createPhysicalCard(tableCode.player3Locations[i], deck.drawCards(1, 's')[0]);
        }
        for (int i = 0; i < tableCode.player4Locations.Length; i++)
        {
            //Debug.Log(i);
            createPhysicalCard(tableCode.player4Locations[i], deck.drawCards(1, 's')[0]);
        }
    }

    public void createPhysicalCard(GameObject location, Card c)
    {
        GameObject cardObj = Instantiate(cardPrefab, location.transform.position, location.transform.rotation);
        cardObj.GetComponent<CardPrefabCode>().setCard(c);
    }
}
