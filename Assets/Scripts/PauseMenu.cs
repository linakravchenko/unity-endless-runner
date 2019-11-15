using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    public Text scoreText;
    public Text coinText;
    public Image backImg;
    public Color backColor;

    private bool isShowned = false;
    private float transition = 0.0f;
    private GameObject musicObj;

    void Start()
    {
        gameObject.SetActive(false);
    }

    void Update()
    {
        if (!isShowned)
            return;

        transition += Time.unscaledDeltaTime;
        backImg.color = Color.Lerp(new Color(0, 0, 0, 0), backColor, transition);
    }

    public void TogglePauseMenu(float score, int coin)
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
        gameObject.SetActive(false);
        isShowned = false;
        Time.timeScale = 1;
        musicObj = GameObject.FindGameObjectWithTag("Music");
        musicObj.GetComponent<AudioLowPassFilter>().cutoffFrequency = 5000;
    }

    public void ToMenu()
    {
        
        GameObject.FindGameObjectWithTag("Player").GetComponent<Score>().SaveResult();
        SceneManager.LoadScene("Menu");
        Time.timeScale = 1;
    }

    public void Pause()
    {
        if (Time.timeScale == 1)
        {
            Time.timeScale = 0;
            GameObject.FindGameObjectWithTag("Player").GetComponent<Score>().OnPause();
        }
    }
}
