using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Advertisements;

public class Levels : MonoBehaviour
{

    public Button[] buttons = new Button[20];
    int levelComplete;
    void Start()
    {
        if (Advertisement.isSupported)
        {
            Advertisement.Initialize("3642710", false);
        }
        levelComplete = PlayerPrefs.GetInt("LevelComplete");
        for (int i = 1; i < buttons.Length; i++)
        {
            buttons[i].interactable = false;
            buttons[i].GetComponent<Image>().color = Color.white;

        }
        for (int i = 1; i < buttons.Length; i++)
        {
            if (levelComplete > i)
            {
                buttons[i].interactable = true;
                if (i <= 4)
                {
                    buttons[i].GetComponent<Image>().color = Color.red;
                }
                else if (i <= 9)
                {
                    buttons[i].GetComponent<Image>().color = Color.green;
                }
                else if (i <= 14)
                {
                    buttons[i].GetComponent<Image>().color = Color.yellow;
                }
                else if (i <= 19)
                {
                    buttons[i].GetComponent<Image>().color = Color.blue;
                }
            }
        }
    }


    public void LoadTo(int index)
    {

        if (Health.totalHealth >= 1)
        {
            if (index - 2 == 5 || index - 2 == 10 || index - 2 == 17)
            {
                if (Advertisement.IsReady("video"))
                {
                    Advertisement.Show("video");
                }
            }
            if (buttons[index - 2] == true)
            {
                SceneManager.LoadScene(index);
            }
        }
    }
    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void Reset()
    {
        if (Advertisement.IsReady("rewardedVideo"))
        {
            Advertisement.Show("rewardedVideo");
        }
        for (int i = 1; i < buttons.Length; i++)
        {
            buttons[i].interactable = false;
            buttons[i].GetComponent<Image>().color = Color.white;

        }
        PlayerPrefs.DeleteAll();
    }
}
