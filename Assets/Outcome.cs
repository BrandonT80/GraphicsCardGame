using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Outcome : MonoBehaviour
{
    public List<GameObject> colliderList = new List<GameObject>();

    public void OnTriggerEnter(Collider collider)
    {
        if(!colliderList.Contains(collider.gameObject))
        {
            colliderList.Add(collider.gameObject);
            //Debug.Log("Added " + gameObject.name);
            //Debug.Log("GameObjects in list: " + colliderList.Count);
        }
    }

    public void OnTriggerExit(Collider collider)
    {
        if(colliderList.Contains(collider.gameObject))
        {
            colliderList.Remove(collider.gameObject);
            //Debug.Log("Removed " + gameObject.name);
            //Debug.Log("GameObjects in list: " + colliderList.Count);
        }
    }
}
