using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinManager : MonoBehaviour
{
    bool activated;
    bool credits;
    int lvl; 
    // Start is called before the first frame update
    void Start()
    {
       
        activated = false;
        StartCoroutine(ActivateBool());
        StartCoroutine(GetLevel());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && activated && !credits)
        {
            SceneManager.LoadSceneAsync(1, LoadSceneMode.Single);
        }

        if (Input.GetKeyDown(KeyCode.Space) && !activated && credits)
        {
            SceneManager.LoadSceneAsync(4, LoadSceneMode.Single);
        }
    }

    IEnumerator GetLevel()
    {
        yield return new WaitForSeconds(1);
        lvl = PlayerPrefs.GetInt("Level");

        if (lvl > 3)
        {
            credits = true;
            activated = false;
        }

    }

    IEnumerator ActivateBool()
    {
        yield return new WaitForSeconds(7);
        activated = true;
    }
}
