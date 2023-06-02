using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class managerScript : MonoBehaviour
{
    public Image toastLvl;

    public Sprite L1;
    public Sprite L2;
    public Sprite L3;
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

        if(lvl == 1)
        {
            toastLvl.sprite = L1;
        }
        if (lvl == 2)
        {
            toastLvl.sprite = L2;
        }
        if (lvl == 3)
        {
            toastLvl.sprite = L3;
        }
        
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
