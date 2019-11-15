using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    private float score = 0.0f;
    private int coin = 0;
    private int coins;
    private int difficultyLevel = 1;
    private int maxDifficultyLevel = 10;
    private int scoreToNextLevel = 10;

    private bool isDead = false;

    public Text scoreText;
    public Text CoinText;
    public Text MultiText;

    public DeathMenu deathMenu;
    public PauseMenu pauseMenu;

    public AudioSource gameMusic;
    public AudioSource sfxMusic;

    void Start()
    {
        coins = PlayerPrefs.GetInt("Coins", coin);
        gameMusic.volume = MainMenuManager.gameMusicVolume;
        sfxMusic.volume = MainMenuManager.sfxMusicVolume;
    }

    void Update()
    {
        if (isDead)
            return;

        if (score >= scoreToNextLevel)
            LevelUP();

        score += Time.deltaTime * difficultyLevel * 2;
        scoreText.text = ((int)score).ToString();
        CoinText.text = coin.ToString();
        MultiText.text = "lvl" + difficultyLevel.ToString();

        gameMusic.volume = MainMenuManager.gameMusicVolume;
        sfxMusic.volume = MainMenuManager.sfxMusicVolume;
    }

    void LevelUP()
    {
        if (difficultyLevel == maxDifficultyLevel)
            return;

        scoreToNextLevel *= 2;
        difficultyLevel++;

        GetComponent<PlayerMotor>().SetSpeed(difficultyLevel);
    }

    public void OnDeath()
    {
        /*isDead = true;
        if (PlayerPrefs.GetFloat("Highscore") < score)
        {
            PlayerPrefs.SetFloat("Highscore", score);
            deathMenu.newBestText.enabled = true;
        }
        coins += coin;
        PlayerPrefs.SetInt("Coins", coins);*/
        deathMenu.ToggleEndMenu(score, coin);
    }

    public void SaveResult()
    {
        if (PlayerPrefs.GetFloat("Highscore") < score)
        {
            PlayerPrefs.SetFloat("Highscore", score);
            deathMenu.newBestText.enabled = true;
        }
        coins = PlayerPrefs.GetInt("Coins", coin);
        coins += coin;
        PlayerPrefs.SetInt("Coins", coins);
    }

    public void OnPause()
    {
        pauseMenu.TogglePauseMenu(score, coin);
    }

    public void CoinIncrement()
    {
        coin++;
    }
}
