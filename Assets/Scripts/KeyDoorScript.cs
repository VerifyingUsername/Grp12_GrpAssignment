using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyDoorScript : MonoBehaviour
{
    public GameObject KeyDoor;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" )
        {
            //Debug.Log("Door works");
            if (PlayerScript.CardCount == 2)
            {
                if (KeyDoor.transform.position.y <= 100)
                {
                    transform.Translate(Vector3.up * Time.deltaTime * speed);
                }
            }
            else
            {
                Debug.Log("Not enough keys!");
            }

        }
    }
    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.tag == "Player")
    //    {
    //        Debug.Log("Door works");
    //        if (PlayerScript.CardCount == 2)
    //        {
    //            if (KeyDoor.transform.position.y <= 10)
    //            {
    //                transform.Translate(Vector3.up * Time.deltaTime * speed);
    //            }
    //        }

    //    }
    //}
}
