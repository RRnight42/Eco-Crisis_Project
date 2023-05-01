using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class body_hitbox : MonoBehaviour
{

    public GameObject enemy;

    Enemy enemyBrain;

        void Start()
    {
        enemyBrain = enemy.GetComponent<Enemy>(); 
    
    }
   public void Damage( int damage)
    {
        int criticalProbability = Random.Range(1, 25);

        if (criticalProbability == 1)
        {
            int criticalDamage = damage * 2;
            enemyBrain.lifePoints -= criticalDamage;
        }
        else
        {
            enemyBrain.lifePoints -= damage;
        }
    }

    public void DamageHead(int damage)
    {
        int damageHead = damage * 2;
        int criticalProbability = Random.Range(1, 25);

        if (criticalProbability == 1)
        {
            int criticalDamage = damageHead * 2;
            enemyBrain.lifePoints -= criticalDamage;
        }
        else
        {
            enemyBrain.lifePoints -= damageHead;
        }
    }
}
