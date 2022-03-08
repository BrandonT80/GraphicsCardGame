using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table : MonoBehaviour
{
    public GameObject[] pokerLocations = new GameObject[5];
    public GameObject[] dealerLocations = new GameObject[2];
    public GameObject[] player1Locations = new GameObject[4];
    public GameObject[] player2Locations = new GameObject[4];
    public GameObject[] player3Locations = new GameObject[4];
    public GameObject[] player4Locations = new GameObject[4];
    public int playerNumber = 2;

    // Start is called before the first frame update
    void Start()
    {
        switch(playerNumber)
        {
            case 1:
                GameObject.Find("P1Cam").tag = "MainCamera";
                GameObject.Find("P1Cam").GetComponent<Camera>().enabled = true;
                break;
            case 2:
                GameObject.Find("P2Cam").tag = "MainCamera";
                GameObject.Find("P2Cam").GetComponent<Camera>().enabled = true;
                break;
            case 3:
                GameObject.Find("P3Cam").tag = "MainCamera";
                GameObject.Find("P3Cam").GetComponent<Camera>().enabled = true;
                break;
            case 4:
                GameObject.Find("P4Cam").tag = "MainCamera";
                GameObject.Find("P4Cam").GetComponent<Camera>().enabled = true;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void dealAll()
    {

    }
}
