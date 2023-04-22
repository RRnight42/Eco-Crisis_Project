using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class Enemy : Character
{
 
    public enum State { patrol ,  rest }
    public State state;

    public NavMeshAgent AI;
    public GameObject player;
    public Player playerScript;
    public GameObject[] patrolPoints;
    
    public bool following;    
    public int point;
    public float angleVision = 110;
    public int range = 20;

    void Start()
    {
        lifePoints = 100;
        state = State.rest;
        following = false;
        point = Random.Range(0, 9);
        AI = this.GetComponent<NavMeshAgent>();
        patrolPoints = GameObject.FindGameObjectsWithTag("PatrolPoint");
        player = GameObject.FindGameObjectWithTag("Player");
        playerScript = player.GetComponent<Player>();
        StartCoroutine(StateMachine());
    }

    void Update()
    {
        if(lifePoints < 0)
        {
            playerScript.purityPoints = playerScript.purityPoints - Random.Range(5,11);
            playerScript.points = playerScript.points + 50;
            Destroy(this.gameObject);
        }
        switch (state)
        {
            case State.patrol:
                Patrol();
                Detection();
                break;

            case State.rest:
                Attack();
                Detection();
                break;      
        }
    }

    IEnumerator StateMachine()
    {
        while (true)
        {
            state = State.patrol;
            yield return new WaitForSeconds(Random.Range(1,16));
            if(following == false)
            {           
            state = State.rest;
            yield return new WaitForSeconds(5);
            }
           
        }
        
    }
    void Patrol()
    {
        AI.isStopped = false;
        AI.SetDestination(patrolPoints[point].transform.position);
        Vector3 distance= AI.transform.position - patrolPoints[point].transform.position;
        float DetectionDistance = 4f;

        if (distance.magnitude < DetectionDistance)
        {
            point = Random.Range(0, 9);
        }
    }
    void Detection()
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
                    }
                }
                else
                {
                    following = false;
                }
            }
        }
    }
    void Resting()
    {
        AI.isStopped = true;
    }

    public override void Attack()
    {
        
        //instanciar la bola de residuos , por el momento solo ataca a melé quitando 30 de vida


    }
}
