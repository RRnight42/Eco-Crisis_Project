using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject enemy;
    public GameObject[] enemies;
    int lvl;
    int SpawnTime;

    // Start is called before the first frame update
    void Start()
    {
        
        StartCoroutine(Spawnear());
    }
    IEnumerator Spawnear()
    {
        yield return new WaitForSeconds(2);
        lvl = PlayerPrefs.GetInt("Level");

        if(lvl == 1) {
            SpawnTime = 11;
        }
        if (lvl == 2) {
            SpawnTime = 7;
        }
        if (lvl == 3) {
            SpawnTime = 3;
        }

        
        while (true)
        {
            enemies = GameObject.FindGameObjectsWithTag("enemy");
            yield return new WaitForSeconds(3);



            if (enemies.Length < 40)
            {
                yield return new WaitForSeconds(Random.Range(1, SpawnTime)); 
                    
                for(int i = 0; i < 5; i++)
                {
                 Instantiate(enemy, this.transform.position, this.transform.rotation);
                }
               
            }

        }
    }


}
