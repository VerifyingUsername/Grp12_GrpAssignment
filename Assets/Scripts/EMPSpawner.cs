using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EMPSpawner : MonoBehaviour
{
    public GameObject EMPCharge;

    // Start is called before the first frame update
    void Start()
    {
        GameObject.Instantiate(EMPCharge, transform.position, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Spawn()
    {
        while(true)
        {
            GameObject.Instantiate(EMPCharge, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(2.0f);
        }
    }
}
