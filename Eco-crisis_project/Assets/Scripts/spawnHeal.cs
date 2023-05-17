using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnHeal : MonoBehaviour
{
    GameObject[] items;

    public GameObject heal;
    public GameObject shield;

    int RandomItem;
    int time;
    void Start()
    {
        StartCoroutine(SpawnItem());
    }

   IEnumerator SpawnItem()
    {
        while (true)
        {
        yield return new WaitForSeconds(1);
        time++;
            


             if(time == 30)
            {
               // items = GameObject.FindGameObjectsWithTag("");

               if(items.Length > 5)
               {
                 time = 0;
               }
                else
                 {
                 RandomItem = Random.Range(1, 4);

                 if(RandomItem == 1 || RandomItem == 2)
                 {
                  Instantiate(heal, this.transform.position, this.transform.rotation);
                    time = 0;
                 }
                  if(RandomItem == 3)
                  {
                    Instantiate(shield, this.transform.position, this.transform.rotation);
                    time = 0;
                  }
               
          }





            }

       

        }
      
    }
}
