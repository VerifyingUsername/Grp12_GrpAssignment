using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneTurret : MonoBehaviour
{
    private Transform player;
    private float distance;
    public float howclose;

    public GameObject TurretBullet;
    public float timeBetweenSpawn;
    bool alreadySpawned;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(player);
        distance = Vector3.Distance(player.position, transform.position);
        if (distance <= howclose)
        {
            transform.LookAt(player);
        }

        if (distance <= 20f)
        {
            SpawningTurretBullet();
        }
    }

    private void SpawningTurretBullet()
    {
        if (!alreadySpawned)
        {
            Instantiate(TurretBullet, transform.position, Quaternion.identity);

            alreadySpawned = true;
            Invoke(nameof(ResetSpawn), timeBetweenSpawn);
        }
    }

    private void ResetSpawn()
    {
        alreadySpawned = false;
    }
}
