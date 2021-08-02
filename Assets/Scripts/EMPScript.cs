using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EMPScript : MonoBehaviour
{
    public int HealthPoint;

    private Transform Player;
    public Vector3 Target;

    public float speed;
   
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        Target = new Vector3(Player.transform.position.x, Player.transform.position.y, Player.transform.position.z);

        Destroy(gameObject, 5);

    }

    // Update is called once per frame
    void Update()
    {
        //GameObject.FindGameObjectsWithTag("Player");
        transform.position = Vector3.MoveTowards(transform.position, Target,  speed * Time.deltaTime);
    }

    public void OnHit(int damage)
    {
        HealthPoint -= damage;

        if (HealthPoint <= 0)
        {
            Dead();
        }
    }

    private void Dead()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }

        if (other.gameObject.tag == "Bullet")
        {
            Destroy(gameObject);
        }
    }
}
