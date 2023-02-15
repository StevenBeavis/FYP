using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonDash : MonoBehaviour
{
    ThirdPersonMovement ms;

    public float dashSpeed;
    public float dashTime;

    public static bool dashUI;
    public static bool canDash;

    // Start is called before the first frame update
    void Start()
    {
        ms = GetComponent<ThirdPersonMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (canDash == true)
            {
                StartCoroutine(Dash());
                dashUI = true;

            }
            
        }
    }
    IEnumerator Dash()
    { 
        float startTime = Time.time;

        while (Time.time < startTime + dashTime)
        {
            ms.Controller.Move(ms.moveDirection * dashSpeed * Time.deltaTime);
            //yield return new WaitForSeconds(5f);
            yield return null;
        }
    }
}
