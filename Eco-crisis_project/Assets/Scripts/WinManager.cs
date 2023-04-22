using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinManager : MonoBehaviour
{
    bool activated;
    // Start is called before the first frame update
    void Start()
    {
        activated = false;
        StartCoroutine(ActivateBool());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && activated)
        {
            SceneManager.LoadSceneAsync(1, LoadSceneMode.Single);
        }
    }

    IEnumerator ActivateBool()
    {
        yield return new WaitForSeconds(7);
        activated = true;
    }
}
