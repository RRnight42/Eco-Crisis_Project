using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class Enemy : Character
{
    Animator anim;

    public NavMeshAgent AI;
    public GameObject player;
    public Player playerScript;
    public GameObject[] patrolPoints;
    
    public GameObject bullet ;
    public GameObject generator;
    public GameObject deathEffect;

    public bool following;    
    public int point;
    public float timeShoot;
    public float shootRate;
    public float angleVision = 110;
    public int range = 20;
    int lvl;
    void Start()
    {
      
        lifePoints = 50;
        following = false;
        point = Random.Range(0, 9);
        AI = this.GetComponent<NavMeshAgent>();
        generator = this.transform.GetChild(1).gameObject;
        patrolPoints = GameObject.FindGameObjectsWithTag("PatrolPoint");
        anim = this.GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerScript = player.GetComponent<Player>();
        StartCoroutine(GetLevel());
    }

    void Update()
    {
      
        patrol();
        detect();


        if (playerScript.win)
        {
            GameObject newdeathEffect = Instantiate(deathEffect, this.transform.position, this.transform.rotation);
            Destroy(this.gameObject);
        }

        if(lifePoints < 0)
        {
            playerScript.purityPoints = playerScript.purityPoints - Random.Range(5,11);
            playerScript.points = playerScript.points + 50;
            GameObject newdeathEffect = Instantiate(deathEffect, this.transform.position, this.transform.rotation);
            Destroy(this.gameObject);
        }
    }

  
    
    void patrol()
    {   
        AI.SetDestination(patrolPoints[point].transform.position);
        Vector3 distance= AI.transform.position - patrolPoints[point].transform.position;
        float DetectionDistance = 4f;

        if (distance.magnitude < DetectionDistance)
        {
            point = Random.Range(0, 9);
        }
     
    }
 
   void detect()
    {
        Vector3 distPlayer = player.transform.position - this.transform.position;
        if (distPlayer.magnitude < range)
        {
            RaycastHit result;
            if (Physics.Raycast(this.transform.position, distPlayer, out result, 40))
            {
                if (result.transform.tag == "Player")
                {
                    float angle = Vector3.Angle(this.transform.forward, distPlayer);
                    if (angle < angleVision)
                    {
                        following = true;
                        AI.SetDestination(player.transform.position);
                        AI.speed = 4.5f;
                        timeShoot = timeShoot + Time.deltaTime;
                        if (timeShoot > shootRate)
                        {
                            anim.SetTrigger("Attack");
                            timeShoot = 0;
                        }
                    }
                }
                else
                {
                    following = false;
                    AI.speed = 3.5f;
                }
            }
        }
    }

    public override void Attack()
    {
        //set it on the animator
       Instantiate(bullet, generator.transform.position, this.transform.rotation);
       
    }


    public void MeleeAttack()
    {
        Player player = GameObject.Find("Player").GetComponent<Player>();
        player.DamageDisplayer.SetTrigger("Damage");
        if (player.shieldActivated)
        {
            player.lifePoints -= 5;
        }
        else
        {
            player.lifePoints -= 20;
        }

    }

    IEnumerator GetLevel()
    {
        yield return new WaitForSeconds(1);
        lvl = PlayerPrefs.GetInt("Level");    


        if(lvl == 1)
        {
            shootRate = 7f;
        }
        if (lvl == 2)
        {
            shootRate = 5f;
        }
        if (lvl == 3)
        {
            shootRate = 3f;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            anim.SetTrigger("Attack");
        }
    }
}
