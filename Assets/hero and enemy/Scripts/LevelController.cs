using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public class LevelController : MonoBehaviour
{
    public static LevelController instance = null;
    int sceneIndex;
    int levelComplete;
    bool isPaused=false;
    AudioSource Audio;
    bool isMusic = true;
    public Text TextPause;

    void Start()
    {
        Audio = GameObject.Find("Music").GetComponent<AudioSource>();
        if (Advertisement.isSupported)
        {
            Advertisement.Initialize("3642710", false);
        }
        if (instance==null)
        {
            instance = this;
        }
        sceneIndex = SceneManager.GetActiveScene().buildIndex;
        levelComplete = PlayerPrefs.GetInt("LevelComplete");
    }
    public void isEndGame()
    {
        if (sceneIndex == 21)
        {
            if (Advertisement.IsReady("rewardedVideo"))
            {
                Advertisement.Show("rewardedVideo");
            }
            Invoke("LoadMainMenu", 1f);
        }
        else
        {
            if (levelComplete < sceneIndex)
                PlayerPrefs.SetInt("LevelComplete", sceneIndex);
            Invoke("NextLevel", 0f);
        }
    }
    void NextLevel()
    {
        SceneManager.LoadScene(sceneIndex + 1);
    }
    void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void Pause()
    {
        if(isPaused)
        {
            Time.timeScale = 1;
            isPaused = false;
            TextPause.text = null;
        }
        else
        {
            Time.timeScale = 0;
            isPaused = true;
            TextPause.text = "Pause";
        }
    }

    public void Mute()
    {
        if(isMusic)
        {
            Audio.mute = true;
            isMusic = false;
        }
        else if(!isMusic)
        {
            Audio.mute = false;
            isMusic = true;
        }
        else
        {

        }

    }
    public void LevelsButton()
    {
        if (Advertisement.IsReady("rewardedVideo"))
        {
            Advertisement.Show("rewardedVideo");
        }
        SceneManager.LoadScene(1);
    }
}
