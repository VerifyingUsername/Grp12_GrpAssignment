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

    [SerializeField] Text countdownText;
    
    // Start is called before the first frame update
    void Start()
    {
        currentTime = startingTime;
    }

    // Update is called once per frame
    void Update()
    {
        if(CardText == 1)
        {
            currentTime -= 1 * Time.deltaTime;
            countdownText.text = currentTime.ToString("0.0");

            if (currentTime <= 0)
            {
                currentTime = 0;
                SceneManager.LoadScene("L1");
            }
            else if(currentTime >= 10)
            {
                currentTime = 12;
                currentTime += 1 * Time.deltaTime;
                countdownText.text = currentTime.ToString("0.0");
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Access Card")
        {
            Destroy(other.gameObject);
            CardText++;
        }

        if(other.tag == "NextLevel")
        {
            if (currentTime > 0)
            {
                currentTime = 12;
            }
        }
    }
}
