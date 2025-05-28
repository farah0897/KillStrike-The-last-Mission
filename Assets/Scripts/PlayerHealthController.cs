using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    public static PlayerHealthController instance;

    public int maxHealth, currentHealth;

    // for å gjøre lifet til player lengere 
    public float invincibleLength = 1f;
    private float invincCounter;

    private void Awake()
    {
        instance = this;
    }

    //    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;

        // for å kontrollere UI på skjermen 
        UIController.instance.healthSlider.maxValue = maxHealth;
        UIController.instance.healthSlider.value = currentHealth;
        UIController.instance.healthText.text = "HEALTH: " + currentHealth + "/" + maxHealth;
    }

    //    // Update is called once per frame
    void Update()
    {
        if (invincCounter > 0)
        {
            invincCounter -= Time.deltaTime;
        }
    }

    // player health
    public void DamagePlayer(int damageAmount)
    {
        currentHealth -= damageAmount;
        if (invincCounter <= 0)
        {
            if (invincCounter <= 0 && !GameManager.instance.levelEnding)
                // player skadd
                AudioManager.instance.PlaySFX(7);



            UIController.instance.ShowDamage();

            if (currentHealth <= 0)
            {
                gameObject.SetActive(false);

                currentHealth = 0;

                GameManager.instance.PlayerDied();

                //player død og plyaer skadd
                AudioManager.instance.StopBGM();
                AudioManager.instance.PlaySFX(6);
                AudioManager.instance.StopSFX(7);
            }

            invincCounter = invincibleLength;


            UIController.instance.healthSlider.value = currentHealth;
            UIController.instance.healthText.text = "HEALTH: " + currentHealth + "/" + maxHealth;
        }
    }   

            // Health pickups 
            public void HealPlayer(int healAmount)
            {
                currentHealth += healAmount;

                if (currentHealth > maxHealth)
                {
                    currentHealth = maxHealth;
                }

                UIController.instance.healthSlider.value = currentHealth;
                UIController.instance.healthText.text = "HEALTH: " + currentHealth + "/" + maxHealth;
            }
        
    
}
