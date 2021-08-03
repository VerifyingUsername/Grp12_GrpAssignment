using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBullet : MonoBehaviour
{
    public int HealthPoint;

    private Transform player;
    public Vector3 Target;

    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        Target = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);

        Destroy(gameObject, 3);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, Target, speed * Time.deltaTime);
    }

    public void OnHit(int damage)
    {
        HealthPoint -= damage;

        if (HealthPoint <= 0)
        {
            Dead();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Wall")
        {
            Destroy(gameObject);
        }

        if (other.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
    }

    private void Dead()
    {
        Destroy(gameObject);
    }
}
