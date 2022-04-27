using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardPrefabCode : MonoBehaviour
{
    public string cardValue = "";      //String tha will hold the cards value
    public char cardSuit;							//Char that will hold the cards suit
    public Transform location;
    public double distance;
    public GameObject manager;
    public bool nextC = false;
    //public GameObject cardPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (distance > 0.01)
        {
            this.transform.position = Vector3.Lerp(this.transform.position, location.position, (1.0f / ((float)distance)) * Time.deltaTime);
            distance = Vector3.Distance(this.transform.position, location.position);
        }
        if (nextC == false && distance < 0.05)
        {
            nextC = true;
            manager.GetComponent<MainSceneManager>().nextCard = true;
        }
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

    public void setLocation(Transform loc)
    {
        location = loc;                //Set the location of the card
        distance = Vector3.Distance(this.transform.position, location.position);
    }

    public void upsideDownCard(){
        this.transform.rotation = Quaternion.Euler(0, 0, 180);
    }

    public void rightSideUpCard(){
        this.transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    public void setManager(GameObject man)
    {
        manager = man;
    }
}
