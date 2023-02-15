using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    public CharacterController Controller;
    public Transform cam;
    Camera cams;

    Animator Lich;

    public float smoothTurning = 0.1f;
    float turnSmoothVelo;
    public float Speed = 5f;
    public float Gravity = -0.02f;

    public Transform groundCheck;
    public float groundDis = 0.4f;
    public LayerMask groundMask;

    public Transform ClosestEnemy;
    public Vector3 moveDirection;
    Vector3 velocity;
    bool isGrounded;

    public static bool isDead = false;

    void Start()
    {
        Lich = GetComponent<Animator>();
        isDead = false;
    }

    public void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDis, groundMask);

        if (isGrounded && velocity.y<0)
        {
            velocity.y = -0.02f;
        }
        if (!isDead)
        {
            float H = Input.GetAxisRaw("Horizontal");
            float V = Input.GetAxisRaw("Vertical");
            Vector3 direction = new Vector3(H, 0f, V).normalized;

            velocity.y += Gravity * Time.deltaTime;

            Controller.Move(velocity);

            bool hasHorizontalInput = !Mathf.Approximately(H, 0f);
            bool hasVerticalInput = !Mathf.Approximately(V, 0f);
            bool isWalking = hasHorizontalInput || hasVerticalInput;
        
            Lich.SetBool("isMoving", isWalking);

            if (direction.magnitude >= 0.1f /*&& !Input.GetKey(KeyCode.Mouse1)*/)
            {

                float tAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, tAngle, ref turnSmoothVelo, smoothTurning);

                moveDirection = Quaternion.Euler(0f, tAngle, 0f) * Vector3.forward;
                if (!Input.GetKey(KeyCode.Mouse1))
                {

                    transform.rotation = Quaternion.Euler(0f, angle, 0f).normalized;
                    Controller.Move(moveDirection.normalized * Speed * Time.deltaTime);
                }
                else
                {
                    Controller.Move(moveDirection.normalized * (Speed / 2) * Time.deltaTime);
                }




            }
            else if(isDead)
            {
                Lich.SetBool("isDead", true);
            }

            if (Input.GetMouseButtonDown(0))
            {
                /*
                Ray ray = cams.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, 100))
                {
                    Interactable interactable = hit.collider.GetComponent<Interactable>();
                    if (interactable != null)
                    {
                        SetFocus(interactable);
                    }
                }
                */
            }

        }
        
        




    }
}
