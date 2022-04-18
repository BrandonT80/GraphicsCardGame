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
                if(selection.CompareTag(selectableTag))
                {
                    money.GetComponent<MainSceneManager>().spendMoney(cost);
                    StartCoroutine(Spin(slot1, timer - 2, rand.Next(500, 1000)));
                    StartCoroutine(Spin(slot2, timer - 1, rand.Next(500, 1000)));
                    StartCoroutine(Spin(slot3, timer, rand.Next(500, 1000)));
                    StartCoroutine(CalculateScore(timer + 1));
                }
            }
        }
    }

    IEnumerator Spin(GameObject slot, float timer, float speed)
    {
        while(timer > 0 || outcome1.GetComponent<Outcome>().colliderList.Count > 2 || outcome2.GetComponent<Outcome>().colliderList.Count > 2 || outcome3.GetComponent<Outcome>().colliderList.Count > 2)
        {
            slot.transform.Rotate(Vector3.forward, Time.deltaTime * speed);
            timer -= Time.deltaTime;
            yield return null;
        }
    }

    IEnumerator CalculateScore(float time)
    {
        Debug.Log("Entered CalculateScore. Waiting...");
        //wait until slots are done spinning 
        yield return new WaitForSeconds(time + 1);

        string result1, result2, result3;

        result1 = outcome1.GetComponent<Outcome>().colliderList[0].gameObject.tag;
        outcome1.GetComponent<Outcome>().colliderList.RemoveAt(0);
        Debug.Log("Result 1: " + result1);

        result2 = outcome2.GetComponent<Outcome>().colliderList[0].gameObject.tag;
        outcome2.GetComponent<Outcome>().colliderList.RemoveAt(0);
        Debug.Log("Result 2: " + result2);

        result3 = outcome3.GetComponent<Outcome>().colliderList[0].gameObject.tag;
        outcome3.GetComponent<Outcome>().colliderList.RemoveAt(0);
        Debug.Log("Result 3: " + result3);

        if(string.Equals(result1, result2) && string.Equals(result2, result3))
        {
            money.GetComponent<MainSceneManager>().addMoney(15);
        }
        else if(string.Equals(result1, result2) || string.Equals(result1, result3) || string.Equals(result2, result3))
        {
            money.GetComponent<MainSceneManager>().addMoney(12);
        }
    }
}