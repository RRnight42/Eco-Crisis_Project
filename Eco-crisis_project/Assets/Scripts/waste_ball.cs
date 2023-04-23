using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waste_ball : MonoBehaviour
{
    float vel;
    // Start is called before the first frame update
    void Start()
    {
        vel = 5;
        Destroy(this.gameObject , 5);
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Translate(this.transform.forward * vel * Time.deltaTime);
    }
}
