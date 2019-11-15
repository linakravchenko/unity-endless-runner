using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class DeathMenu : MonoBehaviour
{

    public Text scoreText;
    public Text coinText;
    public Text newBestText;
    public Image backImg;
    public Color backColor;

    private bool isShowned = false;
    private float transition = 0.0f;
    private GameObject musicObj;
    private int coins;

    public HealthBarManager healthManager;
    public HedgeManager hedges;


    void Start()
    {
        //coins = PlayerPrefs.GetInt("Coins");
        //Debug.Log(coins);
        gameObject.SetActive(false);
    }

    void Update()
    {
        if (!isShowned)
            return;

        transition += Time.unscaledDeltaTime;
        backImg.color = Color.Lerp(new Color(0, 0, 0, 0), backColor, transition);
    }

    public void ToggleEndMenu(float score, int coin)
    {
        musicObj = GameObject.FindGameObjectWithTag("Music");
        musicObj.GetComponent<AudioLowPassFilter>().cutoffFrequency = 700;
        gameObject.SetActive(true);
        scoreText.text = ((int)score).ToString();
        coinText.text = coin.ToString();
        isShowned = true;
    }

    public void Resume()
    {
        coins = PlayerPrefs.GetInt("Coins");
        Debug.Log(coins);

        if (coins > 100)
        {
            coins = coins - 100;
            PlayerPrefs.SetInt("Coins", coins);
            Debug.Log(coins);

            int i = 0;
            GameObject[] allChildren = new GameObject[hedges.transform.childCount];

            foreach (Transform child in hedges.transform)
            {
                allChildren[i] = child.gameObject;
                i += 1;
            }

            foreach (GameObject child in allChildren)
            {
                child.gameObject.SetActive(false);
            }

            healthManager.fillAmount = 100;
            healthManager.healthText.text = (healthManager.fillAmount).ToString() + "/100";
            musicObj = GameObject.FindGameObjectWithTag("Music");
            musicObj.GetComponent<AudioLowPassFilter>().cutoffFrequency = 5000;

            gameObject.SetActive(false);
            Time.timeScale = 1;
        }


    }

    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ToMenu()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<Score>().SaveResult();
        SceneManager.LoadScene("Menu");
        Time.timeScale = 1;
    }

}
