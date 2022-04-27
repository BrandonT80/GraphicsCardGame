using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Outcome : MonoBehaviour
{
    //public List<GameObject> colliderList = new List<GameObject>();
    public GameObject colliderObject;

    public void OnTriggerEnter(Collider collider)
    {
        // if(!colliderList.Contains(collider.gameObject))
        // {
        //     colliderList.Add(collider.gameObject);
        // }
        colliderObject = collider.gameObject;
    }

    // public void OnTriggerExit(Collider collider)
    // {
    //     if(colliderList.Contains(collider.gameObject))
    //     {
    //         colliderList.Remove(collider.gameObject);
    //     }
    // }
}
