using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour

{
    public float moveSpeed, lifeTime; 
    public Rigidbody theRigidbody;
    public GameObject impactEffect;
    public int damage=1; // hvor mye skade 

    public bool damageEnemy, damagePlayer; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // farten til kulen. 
        theRigidbody.velocity=transform.forward*moveSpeed;

        // sette timen kulen exixterer for den blir odelagt. 
        lifeTime -= Time.deltaTime;
        if(lifeTime <= 0)
        {
            Destroy(gameObject); 
        }
    }

    // for odelagge bullet eller treff
    private void OnTriggerEnter(Collider other)
    {
        // ødelage når kulla treffer en enemy.
        if(other.gameObject.tag == "Enemy" && damageEnemy)
        {
            other.gameObject.GetComponent<EnemyHealthController>().DamageEnemy(damage);  
        }
        // hvis player blir truffet
        if(other.gameObject.tag== "Player" && damagePlayer)
        {
           PlayerHealthController.instance.DamagePlayer(damage);


        }

        Destroy(gameObject);
        Instantiate(impactEffect, transform.position+(transform.forward* -(moveSpeed*Time.deltaTime)), transform.rotation);

    }
}
