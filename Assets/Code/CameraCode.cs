using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCode : MonoBehaviour
{

    public GameObject[] cameraLocations = new GameObject[13];
    private int cameraIterator = 1;
    public double distance = 0;
    public GameObject blackJackLoc;
    public GameObject blackJackLookLoc;
    public GameObject slotsLoc;
    public GameObject slotsLookLoc;
    private bool blackJack = false;
    private bool slots = false;
    private GameObject gameLoc;
    private GameObject gameLookLoc;
    private bool doMovement = true;
    

    // Start is called before the first frame update
    void Start()
    {
        this.transform.rotation = cameraLocations[cameraIterator].transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (doMovement)
        {
            if (!blackJack && !slots)
            {
                distance = Vector3.Distance(this.transform.position, cameraLocations[cameraIterator].transform.position);
                if (distance < 1)
                {
                    if (cameraIterator < 12)
                    {
                        cameraIterator++;
                    }
                    else
                    {
                        cameraIterator = 0;
                    }
                    this.transform.rotation = cameraLocations[cameraIterator].transform.rotation;
                }
                else
                {
                    this.transform.position = Vector3.Lerp(this.transform.position, cameraLocations[cameraIterator].transform.position, (1.0f / ((float)distance + 1.5f)) * Time.deltaTime);
                }
            }
            else
            {
                distance = Vector3.Distance(this.transform.position, gameLoc.transform.position);
                if (distance < 0.01)
                {
                    doMovement = false;
                    Debug.Log("Done");
                    //this.transform.rotation = gameLoc.transform.rotation;
                }
                else
                {
                    //this.transform.LookAt(gameLoc.transform);
                    this.transform.LookAt(gameLookLoc.transform);
                    //this.transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(this.transform.position, blackJackTableLoc.transform.position, 0.01f, 0.01f));
                    //this.transform.position = Vector3.RotateTowards(transform.forward, gameLoc.transform.position, 1.0f, 1.0f);
                    this.transform.position = Vector3.Lerp(this.transform.position, gameLoc.transform.position, (1.0f * Time.deltaTime));
                    
                }
            }
        }
    }

    public void goToBlackJack()
    {
        blackJack = true;
        gameLoc = blackJackLoc;
        gameLookLoc = blackJackLookLoc;

        //this.transform.LookAt(gameLoc.transform);
        //this.transform.rotation = gameLoc.transform.rotation;
    }

    public void goToSlots()
    {
        slots = true;
        gameLoc = slotsLoc;
        gameLookLoc = slotsLookLoc;
        //this.transform.LookAt(gameLoc.transform);
    }

    public void spinAroundRoom()
    {
        //No longer playing games :c
        slots = false;
        blackJack = false;
        doMovement = true;
        this.transform.rotation = cameraLocations[cameraIterator].transform.rotation;
    }
}
