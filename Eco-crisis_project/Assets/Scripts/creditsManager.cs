using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class creditsManager : MonoBehaviour
{
    bool activated;

    // Start is called before the first frame update
    void Start()
    {
        activated = false;
        StartCoroutine(ActivateBool());
    }

     void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && activated)
        {
            SceneManager.LoadSceneAsync(0, LoadSceneMode.Single);
        }
    }

    IEnumerator ActivateBool()
    {
        yield return new WaitForSeconds(30);
        activated = true;
    }
}
