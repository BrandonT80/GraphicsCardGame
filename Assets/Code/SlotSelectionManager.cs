using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class SlotSelectionManager : MonoBehaviour
{
    [SerializeField] private string selectableTag = "Selectable";
    [SerializeField] private float timer;
    [SerializeField] private int cost;
    public GameObject slot1, slot2, slot3, outcome1, outcome2, outcome3, money;
    Random rand = new Random();
    private Transform centerPanel;

    // Start is called before the first frame update
    void Start()
    {
        centerPanel = GameObject.Find("Canvas").transform.Find("CenterPanel").transform;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit; 
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Input.GetMouseButtonUp(0))
        {
            if(Physics.Raycast(ray, out hit))
            {
                var selection = hit.transform;
                //check to see if user clicks the play slot button
                if(selection.CompareTag(selectableTag))
                {
                    //remove player's money as payment
                    money.GetComponent<MainSceneManager>().spendMoney(cost);

                    //spin slots and determine how much the player won
                    StartCoroutine(CalculateScore(timer + 1));
                }
            }
        }
    }

    IEnumerator Spin(GameObject slot, GameObject outcome, float timer, float speed)
    {
        hideStand();
        Debug.Log("You spin me right around baby");
        while(timer > 0)
        {
            slot.transform.Rotate(Vector3.forward, Time.deltaTime * speed);
            timer -= Time.deltaTime;
            yield return null;
        }
    }

    IEnumerator CalculateScore(float timer)
    {
        string result1, result2, result3;
        
        //start spinning the slots
        Coroutine a = StartCoroutine(Spin(slot1, outcome1, timer - 2, rand.Next(500, 1000)));
        Coroutine b = StartCoroutine(Spin(slot2, outcome2, timer - 1, rand.Next(500, 1000)));
        Coroutine c = StartCoroutine(Spin(slot3, outcome3, timer, rand.Next(500, 1000)));    

        //wait until slots are done spinning 
        yield return a;
        yield return b;
        yield return c;
        showStand();
        //get outcome for each slot
        result1 = outcome1.GetComponent<Outcome>().colliderObject.gameObject.tag;
        result2 = outcome2.GetComponent<Outcome>().colliderObject.gameObject.tag;
        result3 = outcome3.GetComponent<Outcome>().colliderObject.gameObject.tag;

        //check to see if the player got three matching
        if(string.Equals(result1, result2) && string.Equals(result2, result3))
        {
            money.GetComponent<MainSceneManager>().addMoney(15);
        }
        //check to see if the user got two matching
        else if(string.Equals(result1, result2) || string.Equals(result1, result3) || string.Equals(result2, result3))
        {
            money.GetComponent<MainSceneManager>().addMoney(10);
        }
    }

    public void standUp()
    {
        hideStand();
    }

    public void showStand()
    {
        centerPanel.Find("StandButton").GetComponent<UnityEngine.UI.Button>().enabled = true;
        centerPanel.Find("StandButton").GetComponent<UnityEngine.UI.Image>().enabled = true;
        centerPanel.Find("StandButton").GetComponentInChildren<UnityEngine.UI.Text>().enabled = true;
    }

    public void hideStand()
    {
        centerPanel.Find("StandButton").GetComponent<UnityEngine.UI.Button>().enabled = false;
        centerPanel.Find("StandButton").GetComponent<UnityEngine.UI.Image>().enabled = false;
        centerPanel.Find("StandButton").GetComponentInChildren<UnityEngine.UI.Text>().enabled = false;
    }
}