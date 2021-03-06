using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [Tooltip("Shooting rate of the player")]
    public float ShootingRate;

    [Tooltip("Damage on enemy on each hit")]
    public int ShootingDamage;

    [Tooltip("Damage speed with contact with enemy")]
    public int DamageRate;

    [Tooltip("Starting health of the enemy")]
    public int HealthPoint;

    [Tooltip("Starting ammo of the enemy")]
    public int AmmoCount;

    public static int CardCount;
    public static int CardText;

    float currentTime = 0f;

    [Tooltip("Shooting sound effect")]
    public AudioClip ShootingAudioClip;
    public AudioClip LandMineClip;
    public AudioClip HealthPickUpÇlip;

    // Reference to muzzle flash //
    public GameObject MuzzleFlash;

    private Rigidbody rb = null;
    private Vector3 moveDirection = Vector3.zero;
    private bool canShoot;
    private bool canDamage;
    private AudioSource audioSource;
    private GameObject camera = null;

    // Pause Declaration
    public GameObject PauseMenu;
    public GameObject MainUi;
    private bool GamePaused;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();

        canShoot = true;
        canDamage = true;
        audioSource.clip = ShootingAudioClip;
        audioSource.clip = LandMineClip;
        camera = GameObject.FindGameObjectWithTag("MainCamera");

        GameManager.Instance.UpdateAmmo(AmmoCount);
        GameManager.Instance.UpdateHealth(HealthPoint);

        currentTime = 0f;
        CardText = 0;
        CardCount = 0;
        
        PauseMenu.SetActive(false);
        GamePaused = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.isGameWin)
            return;

        Shoot();

        if (Input.GetKeyDown(KeyCode.Escape) && GamePaused == false)
        {
            Pause();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && GamePaused == true)
        {
            Resume();
        }
        
    }

    // Pause Menu
    public void Pause()
    {
        GamePaused = true;
        Cursor.visible = true;
        MainUi.SetActive(false);
        PauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        GamePaused = false;
        Cursor.visible = false;
        MainUi.SetActive(true);
        PauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }


    // Shoot
    private void Shoot()
    {
        if (Input.GetMouseButtonDown(0) && canShoot && AmmoCount > 0)
        {
            // Play muzzle flash //
            StartCoroutine(PlayMuzzleFlash(/*0.05f*/));

            StartCoroutine(SpawnBullet());
        }
    }

    // Play muzzle flash function //
    private IEnumerator PlayMuzzleFlash(/*float duration*/)
    {
        MuzzleFlash.SetActive(true);
        yield return new WaitForSeconds(0.1f/*duration*/);
        MuzzleFlash.SetActive(false);
    }

    private IEnumerator SpawnBullet()
    {
        RaycastHit hit;

        if (Physics.Raycast(camera.transform.position + camera.transform.forward, camera.transform.forward, out hit, Mathf.Infinity))
        {
            if (hit.collider.gameObject.tag.Equals("Enemy"))
            {
                hit.collider.gameObject.GetComponent<EnemyScript>().OnHit(ShootingDamage);              
            }

            else if (hit.collider.gameObject.tag.Equals("EnemyShooting"))
            {
                hit.collider.gameObject.GetComponent<EnemyShooting>().OnHit(ShootingDamage);
            }

            else if (hit.collider.gameObject.tag.Equals("EMPCharge"))
            {
                //Debug.Log(hit);
                hit.collider.gameObject.GetComponent<EMPScript>().OnHit(ShootingDamage);
            }
        }

        audioSource.PlayOneShot(ShootingAudioClip);

        GameManager.Instance.UpdateAmmo(--AmmoCount);

        canShoot = false;
        //wait for some time
        yield return new WaitForSeconds(ShootingRate);

        canShoot = true;
    }

    private void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.tag.Equals("Enemy") && canDamage)
        {
            StartCoroutine(GetDamage(collision));
        }
    }

    private IEnumerator GetDamage(Collider collision)
    {
        EnemyScript enemyScript = collision.gameObject.GetComponent<EnemyScript>();
        HealthPoint -= enemyScript.ContactDamage;
        GameManager.Instance.UpdateHealth(HealthPoint);

        if (HealthPoint <= 0)
        {
            Dead();
        }

        canDamage = false;
        //wait for some time
        yield return new WaitForSeconds(DamageRate);

        canDamage = true;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "EnemyBullet")
        {           
            HealthPoint -= 10;
            GameManager.Instance.UpdateHealth(HealthPoint);
        }

        if(other.gameObject.tag == "EMPCharge")
        {
            HealthPoint -= 30;
            GameManager.Instance.UpdateHealth(HealthPoint);
        }

        if(other.gameObject.tag == "KeyCard")
        {
            CardCount += 1;
            GameManager.Instance.UpdateCardCount(CardCount);
            Destroy(other.gameObject);
        }

        if (other.gameObject.tag == "Access Card")
        {
            CardText+=1;
            GameManager.Instance.UpdateCardText(CardText);
            Destroy(other.gameObject);
        }

        if (other.gameObject.tag == "Mine")
        {
            audioSource.PlayOneShot(LandMineClip);
            HealthPoint -= 50;
            GameManager.Instance.UpdateHealth(HealthPoint);
            Destroy(other.gameObject);
        }

        if (other.gameObject.tag == "TrigDoor")
        {
            if(CardText == 4)
            {
                Destroy(other.gameObject);
            }          
        }

    }
    

    private void Dead()
    {
        Destroy(gameObject);
    }

    public void AddAmmo(int ammo, AudioClip audioClip)
    {
        AmmoCount += ammo;
        GameManager.Instance.UpdateAmmo(AmmoCount);

        audioSource.PlayOneShot(audioClip);
    }

    public void AddHealth(int health, AudioClip audioClip)
    {
        //Debug.log
        HealthPoint += health;
        GameManager.Instance.UpdateHealth(HealthPoint);
        audioSource.PlayOneShot(HealthPickUpÇlip);
    }
}
