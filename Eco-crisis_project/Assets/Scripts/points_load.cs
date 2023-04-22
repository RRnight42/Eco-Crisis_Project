using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class points_load : MonoBehaviour
{

    public TMP_Text timeText;
    public TMP_Text points;
    public GameObject recordText;

    int time;
    int recordTime;

   
    void Start()
    {
        recordText.SetActive(false);
        points.text = "" + PlayerPrefs.GetInt("points");

        recordTime = PlayerPrefs.GetInt("record");
        time = PlayerPrefs.GetInt("time");

        if(time < recordTime)
        {
            PlayerPrefs.SetInt("record", time);
            recordText.SetActive(true);

        }
        

        int minutes = Mathf.FloorToInt(PlayerPrefs.GetInt("time") / 60);
        int seconds = Mathf.FloorToInt(PlayerPrefs.GetInt("time") % 60);
        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

 
}
