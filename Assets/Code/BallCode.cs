using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCode : MonoBehaviour
{
    bool down = false;
    bool up = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(up);
        if(transform.localPosition.z < 0.13 && up)
        {
            transform.Translate(Vector3.up * Time.deltaTime * 0.05f, Space.World);
        }
        else if (transform.localPosition.z >= 0.13 && up)
        {
            up = false;
            down = true;
        }
        else if (transform.localPosition.z > 0.058 && down)
        {
            transform.Translate(Vector3.down * Time.deltaTime * 0.05f, Space.World);
        }
        else if(transform.localPosition.z < 0.058 && down)
        {
            down = false;
            up = true;
        }
    }
}

