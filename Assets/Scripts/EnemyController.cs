using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
 
    private bool chasing;
    public float distanceToChase = 10f, distanceToLose = 15f, distanceToStop = 2f;


    private Vector3 targetPoint, startPoint;

      public NavMeshAgent agent;

    // for j�ge player 
    public float keepChasingTime = 5f;
    private float chaseCounter;

    public GameObject bullet;
    public Transform firePoint;

    // holder stry p� hvor ofte enemy shiter
    public float fireRate, waitBetweenShots = 2f, timeToShoot = 1f;
    private float fireCount, shotWaitCounter, shootTimeCounter;

    public Animator anim;

     private bool wasShot;

    // Start is called before the first frame update
    void Start()
    {
        // starte punkte til enemy. 
        startPoint = transform.position;

        shootTimeCounter = timeToShoot;
        shotWaitCounter = waitBetweenShots;
    }

    // Update is called once per frame
    void Update()
    {
        targetPoint = PlayerController.instance.transform.position;
        targetPoint.y = transform.position.y;

        if (!chasing)
        {
            if (Vector3.Distance(transform.position, targetPoint) < distanceToChase)
            {
                chasing = true;

                shootTimeCounter = timeToShoot;
                shotWaitCounter = waitBetweenShots;
            }
            // for f� enemy til � g� tilbake til posisjon
            if (chaseCounter > 0)
            {
                chaseCounter -= Time.deltaTime;

                if (chaseCounter <= 0)
                {
                    agent.destination = startPoint;
                }
            }
             // stopp hvis enemy er var orginal posisjon da stopper han � bevege seg
            if (agent.remainingDistance < .25f)
            {
                anim.SetBool("isMoving", false);
            }
            else
            {
                anim.SetBool("isMoving", true);
            }
        }
        else
        {
           // transform.LookAt(targetPoint);

            // theRB.velocity = transform.forward * moveSpeed;

                            // f� enemy til j�ge player via nav mesh
            if (Vector3.Distance(transform.position, targetPoint) > distanceToStop)
            {
                agent.destination = targetPoint;
            }
            else
            {
                // hvis du lar n�r player, du kan stopp og bevege deg. 
                agent.destination = transform.position;
            }

            if (Vector3.Distance(transform.position, targetPoint) > distanceToLose)
            {
                if (!wasShot)
                {
                    chasing = false;

                    // for j�ge player
   
                   chaseCounter = keepChasingTime;
                }
            }
            else
            {
                wasShot = false;
            }
            // handel shooting 
            if (shotWaitCounter > 0)
            {
                shotWaitCounter -= Time.deltaTime;

                if (shotWaitCounter <= 0)
                {
                    shootTimeCounter = timeToShoot;
                }
                // her j�ger enemy player. 
                anim.SetBool("isMoving", true);
            }
            else
            {

                if (PlayerController.instance.gameObject.activeInHierarchy)
                {

                    shootTimeCounter -= Time.deltaTime;

                    if (shootTimeCounter > 0)
                    {
                        fireCount -= Time.deltaTime;

                        if (fireCount <= 0)
                        {
                            fireCount = fireRate;
                            // n�r player hopper pekk oppe of skyt 
                            firePoint.LookAt(PlayerController.instance.transform.position + new Vector3(0f, 1.2f, 0f));

                            //check the angle to the player f�r emeny skyter 
                            Vector3 targetDir = PlayerController.instance.transform.position - transform.position;
                            float angle = Vector3.SignedAngle(targetDir, transform.forward, Vector3.up);

                            if (Mathf.Abs(angle) < 30f)
                            {

                                Instantiate(bullet, firePoint.position, firePoint.rotation);
                                // for � kontrollere emeny bevegelser, her starter enemy med � skyte. 
                                anim.SetTrigger("fireShot");
                            }
                            else
                            {
                                shotWaitCounter = waitBetweenShots;
                            }
                        }

                        agent.destination = transform.position;
                    }
                    else
                    {
                        shotWaitCounter = waitBetweenShots;
                    }
                }
                // n�r enemy skytter skal han ikke bevege seg 
                anim.SetBool("isMoving", false);
            }
        }
    }

    public void GetShot()
    {
        wasShot = true;

        chasing = true;

    }
}

