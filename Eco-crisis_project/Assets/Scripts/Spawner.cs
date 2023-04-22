using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject enemy;
    public GameObject[] enemies;
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = this.GetComponent<Animator>();
        StartCoroutine(Spawnear());
    }
    IEnumerator Spawnear()
    {
        while (true)
        {
            enemies = GameObject.FindGameObjectsWithTag("enemy");
            yield return new WaitForSeconds(3);



            if (enemies.Length < 40)
            {
                yield return new WaitForSeconds(Random.Range(3, 9));              
                Instantiate(enemy, this.transform.position, this.transform.rotation);
            }

        }
    }


}
