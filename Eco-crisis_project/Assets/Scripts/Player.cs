using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Player : Character
{

   public TMP_Text timeText;

    Image lifeImage;
    public Image purityImage;

    public AudioSource tickTimeAudio;
    public AudioSource reloadAudio;
    public AudioSource FiringAudio;

    public AudioClip firingClip;

    public GameObject fullAmmoToast;
    public GameObject emptyAmmoToast;
    GameObject ShieldBarImage;
    GameObject pointingTarget;
    GameObject firingPoint;

    GameObject[] spawners;
   
    public body_hitbox body;

    FPSController controller;
    Player miPlayer;
    
    GameObject shield;
    public GameObject hittingParticle;

    ParticleSystem shieldParticle;
    

    Animator targetAnimator;
    Animator shieldAnimator;
    Animator playerAnimator;

    bool pointing;
    bool shieldActivated;
   public bool win;
    bool lose;

    int lvl;
  public int ammo;   
   int maxLifePoints;
   int maxPurityPoints;
   int shieldTime;
   int shieldSeconds;
   public int points;
  public int purityPoints;
   int time;



    float firingTime;
   float fireRate;
   

    void Start()
    {
       
        
        lvl = PlayerPrefs.GetInt("Level");
        if(lvl == 0)
        {
           PlayerPrefs.SetInt("Level", 1);
            lvl = PlayerPrefs.GetInt("Level");
        }

        spawners = GameObject.FindGameObjectsWithTag("spawner");
        miPlayer = this.GetComponent<Player>();
        controller = this.GetComponent<FPSController>();
        PlayerPrefs.DeleteKey("time");
        timeText = GameObject.Find("time").GetComponent<TMP_Text>();
        lifeImage = GameObject.Find("LifeBar_S").GetComponent<Image>();
        purityImage = GameObject.Find("PurityBar_S").GetComponent<Image>();

        pointingTarget = GameObject.Find("Target");
        targetAnimator = pointingTarget.GetComponent<Animator>();

        ShieldBarImage = GameObject.Find("ShieldBar");
        shieldAnimator = ShieldBarImage.GetComponent<Animator>();

        playerAnimator = this.GetComponent<Animator>();

        firingPoint = GameObject.Find("FiringPoint");

        shield = GameObject.Find("shieldPlayer");
        shieldParticle = shield.GetComponent<ParticleSystem>();

        
        fullAmmoToast.gameObject.SetActive(false);
        emptyAmmoToast.gameObject.SetActive(false);

      
        shieldActivated = false;
        fireRate = 0.2f;     
        lifePoints = 100;
        shieldTime = 10;
        points = 0;

        if(lvl == 1)
        {
         purityPoints = 100;
        }
        if (lvl == 2)
        {
            purityPoints = 250;
        }
        if (lvl == 3)
        {
            purityPoints = 400;
        }
        
        maxLifePoints = 100;
        ammo = 30;
        pointing = false;

        StartCoroutine(TimeCounter());
        StartCoroutine(WinLoad());
        StartCoroutine(LoseLoad());
       
        
    }


    void Update()
    {


//development only for checking 
 
        if (Input.GetKeyDown(KeyCode.Y))
        {
            win = true;
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            lose = true;
        }



        if (lifePoints <= 0)
        {
            lose = true; 
        }

        if (purityPoints <= 0)
        {
           win = true;
        }


        float life = (float)lifePoints / maxLifePoints;
        lifeImage.fillAmount = life;


        float purity = (float)purityPoints / maxPurityPoints;
        purityImage.fillAmount = purity;
    
        //recharge
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (ammo < 30)
            {
                
                StartCoroutine(Recharge());
            }
            else
            {
                StartCoroutine(ShowFullAmmo());
            }

        }

       //pointing
        if (Input.GetKey(KeyCode.Mouse1))
        {
            Debug.DrawRay(firingPoint.transform.position, firingPoint.transform.right * 100, Color.red);
            playerAnimator.SetBool("pointing", true);
            targetAnimator.SetBool("pointing" , true);
            pointing = true;
        }
        else
        {
            playerAnimator.SetBool("pointing", false);
            targetAnimator.SetBool("pointing", false);
            pointing = false;
        }

        //shooting
        if (Input.GetKey(KeyCode.Mouse0) && pointing)
        { 
          if(ammo == 0)
          {
               
                playerAnimator.SetBool("firing", false);
              StartCoroutine(ShowEmptyAmmo());

          } else {

               
            playerAnimator.SetBool("firing" , true);
            

            firingTime += Time.deltaTime;

            if (ammo > 0)
            {
                if (firingTime > fireRate)
                {
                   
                    Attack();
                    ammo = ammo - 1;
                    firingTime = 0;
                }
            }                       
            }

        }
        else
        {
           
            playerAnimator.SetBool("firing" , false);
            firingTime = 0;
            
            
        }

      
    }

 
    public override void Attack()
    {      
        FiringAudio.PlayOneShot(firingClip);

        RaycastHit hit;
        
        if (Physics.Raycast(firingPoint.transform.position, firingPoint.transform.right, out hit , 100))
        {
            if (hit.transform.tag == "scene")
            {               
                GameObject hittingParticleEffect = Instantiate(hittingParticle, hit.point , hittingParticle.transform.rotation);
                hittingParticleEffect.GetComponent<ParticleSystem>().Play();
                Destroy(hittingParticleEffect, 4);

            }

            if (hit.transform.tag == "EnemyBullet")
            {            
                Destroy(hit.transform.gameObject);
            }

            if (hit.transform.tag == "enemy")
            {
                Vector3 distance = firingPoint.transform.position - hit.transform.position;

                
                if (distance.magnitude > 90)
                {
                  hit.transform.GetComponent<body_hitbox>().Damage(5);                  
                }

                if (distance.magnitude > 70 && distance.magnitude < 90)
                {
                    hit.transform.GetComponent<body_hitbox>().Damage(10);
                }

                if (distance.magnitude > 50 && distance.magnitude < 70)
                {
                    hit.transform.GetComponent<body_hitbox>().Damage(15);
                }

                if (distance.magnitude > 30 && distance.magnitude < 50)
                {
                    hit.transform.GetComponent<body_hitbox>().Damage(20);
                }

                if (distance.magnitude > 10 && distance.magnitude < 30)
                {
                    hit.transform.GetComponent<body_hitbox>().Damage(25);
                }

                if (distance.magnitude < 10)
                {
                    hit.transform.GetComponent<body_hitbox>().Damage(40);
                }

            }
            if (hit.transform.tag == "enemyHead")
            {
                Vector3 distance = firingPoint.transform.position - hit.transform.position;
               

                if (distance.magnitude > 90)
                {
                    hit.transform.GetComponent<body_hitbox>().DamageHead(5);
                }
                if (distance.magnitude > 70 && distance.magnitude < 90)
                {
                    hit.transform.GetComponent<body_hitbox>().DamageHead(10);
                }
                if (distance.magnitude > 50 && distance.magnitude < 70)
                {
                    hit.transform.GetComponent<body_hitbox>().DamageHead(15);
                }
                if (distance.magnitude > 30 && distance.magnitude < 50)
                {
                    hit.transform.GetComponent<body_hitbox>().DamageHead(20);
                }
                if (distance.magnitude > 10 && distance.magnitude < 30)
                {
                    hit.transform.GetComponent<body_hitbox>().DamageHead(25);
                }
                if (distance.magnitude < 10)
                {
                    hit.transform.GetComponent<body_hitbox>().DamageHead(40);
                }
            }
        }
    }

   

    IEnumerator WinLoad()
    {
        

        while (!win)
        {
            yield return new WaitForSeconds(1);
        }
       tickTimeAudio.volume = 0;
        StopAllCoroutines();
        PlayerPrefs.SetInt("Points", points);
        PlayerPrefs.SetInt("Level", lvl + 1);

        for (int i = 0; i < spawners.Length; i++)
        {
            GameObject spawner = spawners[i];
            spawner.gameObject.SetActive(false);
        }

      
    
        controller.canMove = false;
        PlayerPrefs.SetInt("time", time);
        if (!PlayerPrefs.HasKey("record"))
        {
            PlayerPrefs.SetInt("record", time);
        }
        SceneManager.LoadSceneAsync(2, LoadSceneMode.Additive);
        miPlayer.enabled = false;
        yield return null;

       
    
    }

   
    IEnumerator LoseLoad()
    {
        
        while (!lose)
        {
            yield return new WaitForSeconds(1);
        }
        tickTimeAudio.volume = 0;
        StopAllCoroutines();
        controller.canMove = false;
        SceneManager.LoadSceneAsync(3, LoadSceneMode.Additive);
        miPlayer.enabled = false;
        yield return null;
    }


    IEnumerator TimeCounter()
    {   
        tickTimeAudio.PlayDelayed(4f);
        yield return new WaitForSeconds(3);
        
        while (true)
        {

         yield return new WaitForSeconds(1);
         time++;
         int minutes = Mathf.FloorToInt(time / 60);
         int seconds = Mathf.FloorToInt(time % 60);
         timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
       
    }

    IEnumerator Recharge()
    {
        reloadAudio.Play();
        playerAnimator.SetBool("firing", false);
        playerAnimator.SetBool("recharge" , true);   
        yield return new WaitForSeconds(3);
        ammo = 30;
        playerAnimator.SetBool("recharge", false);
    }

    IEnumerator ShowFullAmmo()
    {
        fullAmmoToast.gameObject.SetActive(true);
        yield return new WaitForSeconds(2.5f);
        fullAmmoToast.gameObject.SetActive(false);
    }
    IEnumerator ShowEmptyAmmo()
    {
        emptyAmmoToast.gameObject.SetActive(true);
        yield return new WaitForSeconds(2.5f);
        emptyAmmoToast.gameObject.SetActive(false);
    }

    IEnumerator ShieldActive()
    {
        shieldSeconds = 0;
        shieldActivated = true;
        shieldAnimator.SetBool("shield", true);
        shieldParticle.Play();

        while(shieldSeconds < shieldTime)
        {
            yield return new WaitForSeconds(1);
            shieldSeconds = shieldSeconds + 1;
        }

        yield return null;
        shieldParticle.Stop();
        shieldActivated = false;
        
        shieldAnimator.SetBool("shield", false);
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Shield")
        {
            StartCoroutine(ShieldActive());
            Destroy(other.gameObject);
        }
        if(other.gameObject.tag == "Healing")
        {
            if(lifePoints >= 100)
            {

            }
            else
            {
                lifePoints = lifePoints + 25;
                Destroy(other.gameObject);
                if(lifePoints >= 100)
                {
                    lifePoints = 100;
                }
            }
        }
        if(other.gameObject.tag == "EnemyBullet")
        {
            if (shieldActivated)
            {

            }
            else
            {
                lifePoints = lifePoints - 10;
                Destroy(other.gameObject);
            }
        }

        if (other.gameObject.tag == "enemy")
        {
            if (shieldActivated)
            {
                lifePoints = lifePoints - 5;
            }
            else
            {
                lifePoints = lifePoints - 30;
            }
        }

    }
}


