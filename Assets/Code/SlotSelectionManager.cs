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

    // Start is called before the first frame update
    void Start()
    {
        
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
}