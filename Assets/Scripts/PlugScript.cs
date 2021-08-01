﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlugScript : MonoBehaviour
{
    public GameObject PlugDoor;
    public GameObject PlugDoor2;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Socket")
        {
            PlugDoor = GameObject.FindGameObjectWithTag("PlugDoor");
            

            if (PlugDoor.transform.position.y <= 10f)
            {
                PlugDoor.transform.Translate(Vector3.up * Time.deltaTime * speed);               
            }

           
        }


        if (collision.gameObject.tag == "Socket2")
        {          
            PlugDoor2 = GameObject.FindGameObjectWithTag("PlugDoor2");

            
            if (PlugDoor2.transform.position.y <= 10f)
            {
                PlugDoor2.transform.Translate(Vector3.up * Time.deltaTime * speed);
            }

        }
    }
}
