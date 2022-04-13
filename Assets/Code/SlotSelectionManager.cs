using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class SlotSelectionManager : MonoBehaviour
{
    [SerializeField] private string selectableTag = "Selectable";
    [SerializeField] private float timer;
    //[SerializeField] private int speed;
    public GameObject slot1, slot2, slot3;
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
                    //Debug.Log("You selected the " + hit.transform.name);
                    StartCoroutine(Spin(slot1, timer - 2, rand.Next(500, 1000)));
                    StartCoroutine(Spin(slot2, timer - 1, rand.Next(500, 1000)));
                    StartCoroutine(Spin(slot3, timer, rand.Next(500, 1000)));
                }
            }
        }
    }

    IEnumerator Spin(GameObject slot, float timer, float speed)
    {
        while(timer > 0)
        {
            Debug.Log("Spinning: " + timer);
            //slot1.transform.Rotate(0, 0, Time.deltaTime * speed);
            slot.transform.Rotate(Vector3.forward, Time.deltaTime * speed);
            //slot2.transform.Rotate(Vector3.forward, Time.deltaTime * speed);
            //slot3.transform.Rotate(Vector3.forward, Time.deltaTime * speed);
            timer -= Time.deltaTime;
            yield return null;
        }
    }
}