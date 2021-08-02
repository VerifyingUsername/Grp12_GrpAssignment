using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EMPSpawner : MonoBehaviour
{
    public GameObject EMPCharge;
    public float timeBetweenSpawn;
    bool alreadySpawned;

    // Start is called before the first frame update
    void Start()
    {
        //GameObject.Instantiate(EMPCharge, transform.position, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        //StartCoroutine(Spawn());
        //Instantiate(EMPCharge, transform.position, Quaternion.identity);
        SpawningEMP();
    }

    //IEnumerator Spawn()
    //{
    //    while(true)
    //    {
    //        yield return new WaitForSeconds(5.0f);
    //        Instantiate(EMPCharge, transform.position, Quaternion.identity);

    //    }
    //}

    private void SpawningEMP()
    {
        if (!alreadySpawned)
        {
            Instantiate(EMPCharge, transform.position, Quaternion.identity);

            alreadySpawned = true;
            Invoke(nameof(ResetSpawn), timeBetweenSpawn);
        }
    }

    private void ResetSpawn()
    {
        alreadySpawned = false;
    }
}
