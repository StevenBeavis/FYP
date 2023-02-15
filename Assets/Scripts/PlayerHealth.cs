using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerHealth : MonoBehaviour
{
    private float health;
    private float lerpTimer;
    public float maxHealth = 100;
    public float chipSpeed = 1f;
    public Image frontHealthBar;
    public Image backHealthBar;
    public Text healthText;
    public static bool takeDamage;
    public static bool heal;
    public static bool apple;
    public GameObject ds;
    public GameObject playerUI;
    public static bool hs = true;


    void Start()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        health = Mathf.Clamp(health, 0, maxHealth);
        UpdateHealthUI();
        if (Input.GetKeyDown(KeyCode.L) || takeDamage == true)
        {
            takeDamage = false;
            PlayerTakeDamage(Random.Range(5,20));
        }
        if (Input.GetKeyDown(KeyCode.H)|| apple == true)
        {
            apple = false;
 
            RestoreHealth(5);
        }
        if (heal == true)
        {
            heal = false;
            RestoreHealth(Random.Range(20, 35));
        }
        if (health <= 0)
        {
            Die(); ThirdPersonMovement.isDead = true;
        }
        

    }
    public void UpdateHealthUI()
    {
        //Debug.Log(health);
        float fillFront = frontHealthBar.fillAmount;
        float fillBack = backHealthBar.fillAmount;
        float hf = health / maxHealth;
        if (fillBack > hf)
        {
            backHealthBar.color = Color.grey;
            frontHealthBar.fillAmount = hf;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            percentComplete = percentComplete * percentComplete;
            backHealthBar.fillAmount = Mathf.Lerp(fillBack, hf, percentComplete);
        }
        if (fillFront < hf)
        {
            backHealthBar.color = Color.green;
            backHealthBar.fillAmount = hf;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            percentComplete = percentComplete * percentComplete;
            frontHealthBar.fillAmount = Mathf.Lerp(fillFront, backHealthBar.fillAmount, percentComplete);
        }
        healthText.text = Mathf.Round(health) + "/" + Mathf.Round(maxHealth);
    }
    public void PlayerTakeDamage(float damage)
    {
        health -= damage;
        lerpTimer = 0f;
    }
    public void RestoreHealth(float healAmount)
    {
        health += healAmount;
        lerpTimer = 0f;
    }
    public void IncreaseHealth(int level)
    {
        maxHealth += (health * 0.01f) * ((100 - level) * 0.1f);
        health = maxHealth;
    }
    public void Die()
    {
        
        hs = true;
        ds.SetActive(true);
        playerUI.SetActive(false);
        //Time.timeScale = 0;
    }


}
