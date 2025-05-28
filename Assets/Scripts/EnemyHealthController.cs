using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthController : MonoBehaviour
{
    public int CurrentHealth = 5;

    public EnemyController theEC; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Enamy heath

    public void DamageEnemy(int damegeAmount)
    {
        CurrentHealth-=damegeAmount;

        if(theEC != null)
        {
            theEC.GetShot();
        }

        if (CurrentHealth <= 0)
        {
            Destroy(gameObject);
            AudioManager.instance.PlaySFX(6); 

        }
        
    }
}
