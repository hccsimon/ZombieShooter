using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class Zombie : MonoBehaviour
{
    public NavMeshAgent NPC_navi;
    public Transform target;
    public Animator EnemyAnimator;
    public Collider Enemycollider;
    public GameObject WatchingPlayer;
    public PlayerMovement PlayerMovement;
    public PlayerController PlayerController;
    public DuplicateNPC SpawnM;
    public DuplicateNPCLeft SpawnL;
    public DuplicateNPCRight SpawnR;
    public AudioSource ZombieSound;
    public AudioSource ZombieHurtSound;
    //public GameObject PlayerCollider;
    //public GameObject Player;

    public float health = 100f;

    private bool EnemyDead = false;
    float attacktime;
    string sceneName;

    [Header("Attack")]
    public float Range = 1f;
    public Transform AttackPoint;
    public LayerMask playerLayer;
    public bool AttackPlayer = false;
    public float Damage =10;
    public static int TotalKill;


    void Start()
    {
        sceneName = SceneManager.GetActiveScene().name;
        //TotalKill = 0;
        NPC_navi = GetComponent<NavMeshAgent>();

        //playerLayer = LayerMask.GetMask("player");

        //target = GameObject.Find("PlayerObject").transform;

        //WatchingPlayer = GameObject.Find("PlayerObject");
        ZombieSound = GetComponent<AudioSource>();

        //PlayerMovement= GameObject.Find("PlayerObject").GetComponent<PlayerMovement>();
        PlayerController = GameObject.Find("PlayerObject").GetComponent<PlayerController>();
        
        if(sceneName == "Stage1")
        {
            //SpawnM = GameObject.Find("Spawn").GetComponent<DuplicateNPC>();
            SpawnL = GameObject.Find("SpawnLeft").GetComponent<DuplicateNPCLeft>();
            SpawnR = GameObject.Find("SpawnRight").GetComponent<DuplicateNPCRight>();
        }
        

        AttackPoint = GameObject.FindWithTag("Player").transform;
    }

    void Update()
    {
        if(sceneName == "Stage2" && MachineGun.MachineGunTime == true)
        {
            target = GameObject.Find("MGturret").transform;
            //WatchingPlayer = GameObject.Find("MGturret");
        }else
        {
            target = GameObject.Find("PlayerObject").transform;
            //WatchingPlayer = GameObject.Find("PlayerObject");
        }
        if(health >0f && !PlayerController.escapeBool)
        {
            float dist = Vector3.Distance(target.position, transform.position);

            NPC_navi.destination = target.position;

            /*Vector3 v = WatchingPlayer.transform.position - transform.position;  
            v.x = v.z = 0.0f;
            transform.LookAt(WatchingPlayer.transform.position - v);*/
        }
        

        if (NPC_navi.velocity.magnitude >=0.1 &&  health >0 )
        {
            EnemyAnimator.SetBool("isRunning", true);
            EnemyAnimator.SetBool("Attacking",false); 
        }
        else if(NPC_navi.velocity.magnitude <0.1 ||  health <0)
        {
            EnemyAnimator.SetBool("isRunning", false);
        }       

        if(AttackPlayer && health>0 && !PlayerController.escapeBool)
        {
            Attack();
        }else
        {
            EnemyAnimator.SetBool("Attacking",false);
        }
    }

    public void Attack()
    {
        
        attacktime += Time.deltaTime;
        if(attacktime >=1)
        {
            if(sceneName == "Stage1" || sceneName == "Stage2")
            {
                PlayerController.PlayerHurt(Damage);
            }else
            {
                PlayerMovement.PlayerHurt(Damage);
            }

            if(EnemyAnimator.GetCurrentAnimatorStateInfo(0).IsName("Attacking"))
            {
                EnemyAnimator.SetBool("Attacking",false);
            }else
            {
                EnemyAnimator.SetBool("Attacking",true);
                attacktime = 0;
            }
            
        }
        
    }


    public void TakeDamage(float amount)
    {
        health -= amount;
        ZombieSound.Play();
        ZombieHurtSound.Play();
        if(health <= 0f)
        {
            Die();
        }
    }

    void Die()
    {
        TotalKill++;
        EnemyAnimator.SetBool("Attacking",false);
        EnemyAnimator.SetBool("isRunning", false);
        EnemyAnimator.SetTrigger("Dead0");
        EnemyAnimator.SetBool("Dead",true);
        Enemycollider.enabled =false;
        EnemyDead = true;
        AttackPlayer =false;
        if(sceneName =="Stage1")
        {
            //Spawn.SpawnMore();
            if(this.gameObject.tag =="Menemy")
            {
                SpawnM.MSpawnMore();
            }
            else if (this.gameObject.tag =="Renemy")
            {
                SpawnR.RSpawnMore();
            }
            else if (this.gameObject.tag =="Lenemy")
            {
                SpawnL.LSpawnMore();
            }
        }
        PlayerController.PlusPoint(500);
        gameObject.GetComponent<NavMeshAgent>().isStopped = true;

        Invoke("DestroyZombie" , 2f);

    }
    void DestroyZombie()
    {
        if(sceneName == "Stage1")
        {
            Destroy(gameObject);
            return;
        }
        if(this.gameObject.tag == "Preset")
        {
            Destroy(gameObject);
            return;
        }
        int respawn = Random.Range(1,4);
        EnemyAnimator.SetBool("Dead",false);
        gameObject.GetComponent<NavMeshAgent>().isStopped = false;
        Enemycollider.enabled =true;
        
        if(this.gameObject.tag == "BOSS")
        {
            health = 500f;
        }else
        {
            health = 100f;
        }

        switch(respawn)
        {
            case 1:
                this.gameObject.transform.position = new Vector3(-25,0,-38);
                break;
            case 2:
                this.gameObject.transform.position = new Vector3(-11,0,-26);
                break;
            case 3:
                this.gameObject.transform.position = new Vector3(-40,0,-26);
                break;
        }
    }

    public void OnTriggerEnter(Collider Hitbox) //is touching
    {
        if(Hitbox.gameObject.tag =="AttackZone")
        {
            AttackPlayer = true;
        }
    }
    public void OnTriggerExit(Collider Hitbox)
    {
        if(Hitbox.gameObject.tag =="AttackZone")
        {
            AttackPlayer = false;
        }
    }

}
