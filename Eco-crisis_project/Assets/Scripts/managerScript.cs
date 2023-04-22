using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class managerScript : MonoBehaviour
{
    public TMP_Text toastLvl;
    int lvl;
    public Color alpha;
    

    // Start is called before the first frame update
    void Start()
    {
        lvl = PlayerPrefs.GetInt("Level");
        if (lvl == 0)
        {
            PlayerPrefs.SetInt("Level", 1);
        }
        toastLvl.text = "Level " + PlayerPrefs.GetInt("Level");
        StartCoroutine(ShowToast());
        
    }

    
    
    IEnumerator ShowToast()
    {
        while (alpha.a < 1)
        {
            alpha.a += 0.05f;
            toastLvl.color = alpha;
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(2);
        StartCoroutine(DisableToast());
    }

    IEnumerator DisableToast()
    {
        while (alpha.a > 0)
        {
            alpha.a -= 0.05f;
            toastLvl.color = alpha;
            yield return new WaitForSeconds(0.1f);
        }
    }
}
