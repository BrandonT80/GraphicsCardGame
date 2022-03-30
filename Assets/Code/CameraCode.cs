using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCode : MonoBehaviour
{

    public GameObject[] cameraLocations = new GameObject[13];
    private int cameraIterator = 1;
    public double distance = 0;
    // Start is called before the first frame update
    void Start()
    {
        this.transform.rotation = cameraLocations[cameraIterator].transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(this.transform.position, cameraLocations[cameraIterator].transform.position);
        if (distance < 1)
        {
            if(cameraIterator < 12)
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
            this.transform.position = Vector3.Lerp(this.transform.position, cameraLocations[cameraIterator].transform.position, (1.0f/((float)distance + 1.5f)) * Time.deltaTime);
        }
    }

}
