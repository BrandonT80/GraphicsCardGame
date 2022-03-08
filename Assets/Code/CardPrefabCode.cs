using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardPrefabCode : MonoBehaviour
{
    public string cardValue = "";      //String tha will hold the cards value
    public char cardSuit;							//Char that will hold the cards suit
    //public GameObject cardPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setCard(Card c)
    {
        this.cardValue = c.getCardValue();
        this.cardSuit = c.getCardSuit();
        Material temp = Resources.Load<Material>(cardSuit + "" + cardValue);
        this.GetComponent<MeshRenderer>().material = temp;
        //Debug.Log(temp);


        

        //Debug.Log(this.GetComponent<Renderer>().materials[0]);

        //renderer.material = Resources.Load( "mymaterial_mtl") as Material;
        //Debug.Log(this.GetComponent<Renderer>().material = (Material)Resources.Load("Playing Cards/Resource/Materials/BackColor_Red/Red_PlayingCards_Blank_00"));
    }


}
