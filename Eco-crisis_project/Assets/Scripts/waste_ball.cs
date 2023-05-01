using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waste_ball : MonoBehaviour
{
    float vel;
    Rigidbody rigid;
    // Start is called before the first frame update
    void Start()
    {
        rigid = this.GetComponent<Rigidbody>();
        vel = 5;
        Destroy(this.gameObject , 5);
    }

    // Update is called once per frame
    void Update()
    {
        rigid.AddForce(this.transform.forward * vel);
    }
}
