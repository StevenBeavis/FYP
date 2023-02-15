using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Abilities : MonoBehaviour
{

    [Header("Ability 1")]
    public Image abilityImage1;
    public float cooldown1 = 2f;
    public float freeze1 = 2f;
    bool isCooldown1 = false;
    public KeyCode ability1;

    [Header("Ability Dash")]
    public Image abilityImage2;
    public float cooldown2 = 2f;
    public float freeze2 = 2f;
    bool isCooldown2 = false;
    public KeyCode ability2;

    // Start is called before the first frame update
    void Start()
    {
        abilityImage1.fillAmount = 1;
    }

    // Update is called once per frame
    void Update()
    {
        Ability1();

        freeze1 -= 2 * Time.deltaTime;

        AbilityDash();

        freeze2 -= 2 * Time.deltaTime;
    }

    void Ability1()
    {
        if (LockOn.fireUI == true) /*&& isCooldown1 == false*/
        {
            LockOn.fireUI = false;
            //isCooldown1 = true;
            abilityImage1.fillAmount -= .125f;
            freeze1 = 2f;


        }
        if (Input.GetMouseButton(1))
        {
            if (/*isCooldown1 &&*/ freeze1 <= 0)
            {

                abilityImage1.fillAmount += .5f / cooldown1 * Time.deltaTime;
                if (abilityImage1.fillAmount >= 1)
                {
                    abilityImage1.fillAmount = 1;
                    //isCooldown1 = false;
                }
            }
        }
        if (abilityImage1.fillAmount <= .125f)
        {
            LockOn.canFire = false;
        }
        else
            LockOn.canFire = true;

    }
    void AbilityDash()
    {
        if (ThirdPersonDash.dashUI == true) /*&& isCooldown1 == false*/
        {
            ThirdPersonDash.dashUI = false;
            //isCooldown2 = true;
            abilityImage2.fillAmount -= .5f;
            freeze2 = 2f;


        }
        
        if (/*isCooldown2 &&*/ freeze2 <= 0)
        {

            abilityImage2.fillAmount += .5f / cooldown2 * Time.deltaTime;
            if (abilityImage2.fillAmount >= 1)
            {
                abilityImage2.fillAmount = 1;
                //isCooldown1 = false;
            }
        }
        
        if (abilityImage2.fillAmount <= .5f)
        {
            ThirdPersonDash.canDash = false;
        }
        else
            ThirdPersonDash.canDash = true;

    }


}

