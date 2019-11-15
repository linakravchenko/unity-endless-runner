using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    Score myScore;
    AudioSource mySource;
    HealthBarManager myHealth;
    public AudioClip coinClip;
    public AudioClip heartClip;

    void Start()
    {
        myScore = GameObject.FindObjectOfType<Score>();
        mySource = GameObject.FindObjectOfType<AudioSource>();
        myHealth = GameObject.FindObjectOfType<HealthBarManager>();
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player" && gameObject.tag == "Coin")
        {
            mySource.PlayOneShot(coinClip);
            myScore.CoinIncrement();
            Destroy(gameObject);
            //Debug.Log(gameObject.tag);
        }

        if (col.tag == "Player" && gameObject.tag == "Heart")
        {
            //Debug.Log(gameObject.tag);
            mySource.PlayOneShot(heartClip);
            if (myHealth.fillAmount < 100)
            {
                myHealth.fillAmount += 5;
                myHealth.healthText.text = (myHealth.fillAmount).ToString() + "/100";
            }
            Destroy(gameObject);
        }
    }
}
