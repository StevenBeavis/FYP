using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockOn : MonoBehaviour
{
    
    public Transform target;
    private Enemy targetEnemy;

    public float range = 50f;
   
    [Header("Setup")]
    public string enemyTag = "Enemy";

    public Transform toRotate;
    public float turnSpeed = 10f;
    
    [Header("FireAttack")]
    public float fireRate = 1f;
    private float fireCountdown = 2f;
    public GameObject firePrefab;
    public static bool fireUI;
    public static bool canFire;

    [Header("Laser")]
    public bool uselaser = false;
    public int damageOverTime = 30;
    public float slow = .5f;
    public LineRenderer lineRenderer;
    public ParticleSystem impactEffect;
    public Light impactLight;


    public Transform firePoint;

    public AudioClip fireSound;
    public AudioClip chargeSound;
    private void Start()
    {
        InvokeRepeating("FindClosestGameObject", 0f, 0.5f);
        canFire = true;
        fireUI = true;
    }

    public void FindClosestGameObject()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;
        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
            targetEnemy = nearestEnemy.GetComponent<Enemy>();
        }
        else
        {
            target = null;
        }
    }

    public void Update()
    {
        if (target == null)
        {
            if (uselaser)
            {
                if (lineRenderer.enabled)
                {
                    lineRenderer.enabled = false;
                    impactEffect.Stop();
                    impactLight.enabled = false;
                }
            }
            return;
        }
        if (uselaser)
        {
            Laser();
        }
        else if (Input.GetMouseButton(1))
        {
            LockOnTarget();
        }
        
        if (Input.GetMouseButton(0))
        {
            //melee attack
        }
        if (Input.GetMouseButtonDown(1))
        {
            AudioSource.PlayClipAtPoint(chargeSound, firePoint.transform.position);
        }
        
        if (Input.GetMouseButton(1))
        {
            
            if (Input.GetMouseButton(0))
            {
                if (fireCountdown <= 0f)
                {
                    if (!PlayerHealth.hs)
                    {
                        if (canFire == true)
                        {
                            AudioSource.PlayClipAtPoint(fireSound, firePoint.transform.position);
                            Attack();
                            fireCountdown = 1f / fireRate;
                            fireUI = true;
                        }
                    }
                    
                    
                    //a.abilityImage1.fillAmount -= 10;
                }
                
            }
            fireCountdown -= Time.deltaTime;

        }
        
        
    }

    void LockOnTarget()
    {
        
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(toRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        toRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);

        
        //target lock on method
        
    }
    void Attack() 
    {
        var d = target.GetComponent<Animator>().GetBool("isDead");
        if (d == false)
        { 
            GameObject Fireb = (GameObject)Instantiate(firePrefab, firePoint.position, firePoint.rotation);
            PlayerAttack fireball = Fireb.GetComponent<PlayerAttack>();
            
            if (fireball != null)
                fireball.Chase(target);
        }
            

        
    }
    void Laser()
    {

        targetEnemy.TakeDamage(damageOverTime * Time.deltaTime);
        targetEnemy.Slow(slow);
        if (!lineRenderer.enabled)
        {
            lineRenderer.enabled = true;
            impactEffect.Play();
            impactLight.enabled = true;
        }

        lineRenderer.SetPosition(0, firePoint.position);
        lineRenderer.SetPosition(1, target.position);

        Vector3 dir = firePoint.position - target.position;

        impactEffect.transform.position = target.position + dir.normalized;

        impactEffect.transform.rotation = Quaternion.LookRotation(dir);

    }
}