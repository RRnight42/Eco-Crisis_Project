using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuManager : MonoBehaviour
{
    int lvl;
    public Button btn_continue;
    float deleteTime = 0;
    float completed = 7;
    public TMP_Text continueLevelInt;
    public TMP_Text deleteText;
    public Image delImage;
    public Image LVL;
    public Sprite L1;
    public Sprite L2;
    public Sprite L3;
    void Start()
    {
        lvl = PlayerPrefs.GetInt("Level");
      

        if(lvl > 0)
        {
            btn_continue.gameObject.SetActive(true);
        }

        StartCoroutine(DeleteData());

        if (PlayerPrefs.HasKey("Level"))
        {
            if(PlayerPrefs.GetInt("Level") == 1)
            {
                LVL.sprite = L1;
            }
            if (PlayerPrefs.GetInt("Level") == 2)
            {
                LVL.sprite = L2;
            }
            if (PlayerPrefs.GetInt("Level") == 3)
            {
                LVL.sprite = L3;
            }
            btn_continue.gameObject.SetActive(true);
        }
    }

    void Update()
    {

        if (Input.GetKey(KeyCode.Delete))
        {
            deleteText.color = Color.red;
            deleteTime += Time.deltaTime;
        }
        if (Input.GetKeyUp(KeyCode.Delete))
        {
            deleteText.color = Color.white;
            deleteTime = 0;

        }
    }

    public void StartLevel()
    {
        PlayerPrefs.DeleteKey("Level");
        SceneManager.LoadSceneAsync(1, LoadSceneMode.Single);
    }
    public void ContinueLevel()
    {
        SceneManager.LoadSceneAsync(1, LoadSceneMode.Single);
    }
    public void ExitGame()
    {
        Application.Quit();
    }

    IEnumerator DeleteData()
    {
        while (true)
        {
            float time = (float)deleteTime / completed;
            delImage.fillAmount = time;


            if (deleteTime > 7)
            {
                PlayerPrefs.DeleteAll();
                Application.Quit();
            }
            yield return null;
        }

    }
}
