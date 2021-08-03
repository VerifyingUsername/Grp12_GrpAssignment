using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveMineScript : MonoBehaviour
{
    public ParticleSystem Explosion;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Instantiate(Explosion, transform.position, Quaternion.identity);
            //Destroy(gameObject);
        }
    }
}
