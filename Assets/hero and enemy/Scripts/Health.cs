using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public class Health : MonoBehaviour
{
    public Text textHealth;
    public Text textTimer;
    public int maxHealth = 5;
    public static int totalHealth = 0;
    GameObject tmp;
    private DateTime nextHealthTime;
    private DateTime lastAddedTime;
    private bool restoring = false;
    private int restoreDuration = 15;



    void Start()
    {
        tmp= GameObject.FindGameObjectWithTag("Player");
        Load();      
        StartCoroutine(RestoreRoutine());
       
     
    }
    void FixedUpdate()
    {
       if(SceneManager.GetActiveScene().buildIndex!=1)
        {
            FullHealth();
        }
    }
    void FullHealth()
    {
         HeroScript s = tmp.GetComponent<HeroScript>();
        if (s.isEnemy==true)
        {
            totalHealth--;
            UpdateHealth();
            if (totalHealth <= 0)
            {
                totalHealth = 0;
                SceneManager.LoadScene(1);
            }
            else
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
            if(restoring)
            {
                if(totalHealth+1==maxHealth)
                {
                    nextHealthTime = AddDuration(DateTime.Now, restoreDuration);
                }
                StartCoroutine(RestoreRoutine());
            }
        }
    }

    private IEnumerator RestoreRoutine()
    {
        UpdateTimer();
        UpdateHealth();
        while (totalHealth < maxHealth)
        {
            DateTime currentTime = DateTime.Now;
            DateTime counter = nextHealthTime;
            bool isAdding = false;
            while (currentTime > counter)
            {
                if (totalHealth < maxHealth)
                {
                    isAdding = true;
                    totalHealth++;
                    DateTime timeToAdd = lastAddedTime > counter ? lastAddedTime : counter;
                    counter = AddDuration(timeToAdd, restoreDuration);
                }
                else
                    break;
            }
            if (isAdding)
            {
                lastAddedTime = DateTime.Now;
                nextHealthTime = counter;
            }
            UpdateTimer();
            UpdateHealth();
            Save();
            yield return null;
        }
       restoring = true;
    }
    private void UpdateTimer()
    {
        if(totalHealth>=maxHealth)
        {
            textTimer.text = "Full";
            return;
        }

        TimeSpan t = nextHealthTime - DateTime.Now;
        string value = String.Format("{0}:{1:D2}:{2:D2}",(int) t.TotalHours, t.Minutes, t.Seconds);
        textTimer.text = value;
    }
    private void UpdateHealth()
    {
        textHealth.text = totalHealth.ToString();
    }  
    public void AddHealth()
    {

        if (totalHealth == 0)
        {
            if (Advertisement.IsReady("rewardedVideo"))
            {
                Advertisement.Show("rewardedVideo");
            totalHealth += 3;

            }
            UpdateHealth();
            UpdateTimer();
            Save();
        }
    }
    private DateTime AddDuration(DateTime time, int duration)
    {
        return time.AddMinutes(duration);
    }
    private void Load()
    {
        totalHealth = PlayerPrefs.GetInt("totalHealth");
        nextHealthTime = StringToDate(PlayerPrefs.GetString("nextHealthTime"));
        lastAddedTime = StringToDate(PlayerPrefs.GetString("lastAddedTime"));

    }
    private void Save()
    {
        PlayerPrefs.SetInt("totalHealth", totalHealth);
        PlayerPrefs.SetString("nextHealthTime", nextHealthTime.ToString());
        PlayerPrefs.SetString("lastAddedTime", lastAddedTime.ToString());
    }
    private DateTime StringToDate(string date)
    {
        if (String.IsNullOrEmpty(date))
            return DateTime.Now;
        return DateTime.Parse(date);
    }

}
