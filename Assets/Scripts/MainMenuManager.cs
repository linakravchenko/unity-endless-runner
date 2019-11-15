using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{

    public Text scoreText;
    public Text coinText;

    public Slider musicVolume;
    public Slider sfxVolume;

    public AudioSource menuMusic;
    public AudioSource sfxMusic;

    public static float gameMusicVolume;
    public static float sfxMusicVolume;


    void Start()
    {
        scoreText.text = "Score: " + (int)PlayerPrefs.GetFloat("Highscore");
        coinText.text = "" + PlayerPrefs.GetInt("Coins");

        menuMusic.volume = PlayerPrefs.GetFloat("MenuMusic");
        sfxMusic.volume = PlayerPrefs.GetFloat("SFXMusic");
        
        musicVolume.value = PlayerPrefs.GetFloat("MenuMusic");
        sfxVolume.value = PlayerPrefs.GetFloat("SFXMusic");

        gameMusicVolume = PlayerPrefs.GetFloat("GameMusic");
        sfxMusicVolume = PlayerPrefs.GetFloat("SFXMusic");

    }

    private void Update()
    {
        menuMusic.volume = musicVolume.value;
        sfxMusic.volume = sfxVolume.value;

        gameMusicVolume = musicVolume.value;
        sfxMusicVolume = sfxVolume.value;

        PlayerPrefs.SetFloat("MenuMusic", menuMusic.volume);
        PlayerPrefs.SetFloat("GameMusic", gameMusicVolume);
        PlayerPrefs.SetFloat("SFXMusic", sfxMusic.volume);

    }

    public void ToGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void ToExit()
    {
        Application.Quit();
    }
}
