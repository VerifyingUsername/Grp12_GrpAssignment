using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TimerScript : MonoBehaviour
{   
    float currentTime = 0f;
    float startingTime = 10f;
    float CardText = 0f;

    public static bool TimerPause = false;

    [SerializeField] Text countdownText;

    // Start is called before the first frame update
    void Start()
    {
        currentTime = 10f;
        CardText = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerScript.CardText >= 4)
        {
            currentTime -= 1 * Time.deltaTime;
            countdownText.text = "Timer: " + currentTime.ToString("0.0");

            if (currentTime <= 0)
            {
                currentTime = 0;
                SceneManager.LoadScene("L1");
            }
            //else if (currentTime >= 10)
            //{
            //    currentTime = 12;
            //    currentTime += 1 * Time.deltaTime;
            //    countdownText.text = currentTime.ToString("0.0");
            //}
        }

        //Debug.Log(PlayerScript.CardText);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Access Card")
        {
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.tag == "TrigDoor" && CardText >= 4)
        {
            FixedTime();
            //currentTime = 12;
            //if (currentTime > 0)
            //{
            //    currentTime = 12;
            //}
        }
    }

    void Pause()
    {
        Time.timeScale = 0f;
        TimerPause = true;
    }

    private void FixedTime()
    {
        currentTime = 12f;
        countdownText.text = "Timer: " + currentTime.ToString("--");
    }
}
