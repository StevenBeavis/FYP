using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
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

    public LayerMask interactableLayer;
    public LayerMask groundMask;

    Vector3 velocity;
    bool isGrounded;

    public Transform playerView;
    public float interactionDistance = 100f;

    public GameObject interactionUI;
    public static bool loadedFromBattle;

    

   
    private Inventory inventory;
    [SerializeField] private UIInventory inventoryUI;
    
    private void Awake()
    {
        /*
        inventory = new Inventory();
        inventoryUI.SetInventory(inventory);
        */
    }

    void Start()
    {
        Lich = GetComponent<Animator>();
        inventory = new Inventory(UseItem);
        inventoryUI.SetPlayer(this);
        inventoryUI.SetInventory(inventory);

        //test
        

    }

    


    public void HandleUpdate()
    {

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDis, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -0.02f;
        }

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

            Vector3 moveDirection = Quaternion.Euler(0f, tAngle, 0f) * Vector3.forward;
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

        if (Input.GetKeyDown(KeyCode.E) && !isWalking)
        {
            //Interact();
            InteractionRay();
        }
        InteractCheck();
    }
    void InteractCheck() 
    {
        Ray ray = new Ray(playerView.position, playerView.forward);
        //mainCam.ViewportPointToRay(Vector3.one / 2f);
        Debug.DrawRay(ray.origin, ray.direction * 4, Color.red, .05f, false);
        RaycastHit hit;

        bool hitSomething = false;

        if (Physics.Raycast(ray, out hit, interactionDistance))
        {
            IInteractable interactable = hit.collider.GetComponent<IInteractable>();

            if (interactable != null)
            {
                hitSomething = true;
                //interactionText.text = interactable.GetDescription();
                //interactable.Interact();

            }
        }

        interactionUI.SetActive(hitSomething);
    }
    /* Null
    void Interact() 
    {

        var faceDir = new Vector3(Lich.GetFloat("Horizontal"), Lich.GetFloat("Horizontal"));
        var interactPos = transform.position - faceDir;

        Debug.DrawLine(transform.position, interactPos, Color.green, 0.5f);
        
        Collider[] collider = Physics.OverlapSphere(interactPos, 0.3f, interactableLayer);
        if (collider != null)
        {
            GetComponent<Collider>().gameObject.GetComponent<IInteractable>()?.Interact();
        }

    }*/
    void InteractionRay()
    {
        Ray ray = new Ray(playerView.position, playerView.forward);
        //mainCam.ViewportPointToRay(Vector3.one / 2f);
        Debug.DrawRay(ray.origin, ray.direction * 4, Color.red, 1f, false);
        RaycastHit hit;

        bool hitSomething = false;

        if (Physics.Raycast(ray, out hit, interactionDistance))
        {
            IInteractable interactable = hit.collider.GetComponent<IInteractable>();
            
            if (interactable != null)
            {
                hitSomething = true;
                //interactionText.text = interactable.GetDescription();
                interactable.Interact();
                
            }
        }

        interactionUI.SetActive(hitSomething);
    }
    public void UseItem(Item item)
    {
        switch (item.itemType) 
        {
            case Item.ItemType.HealthPotion:
                PlayerHealth.heal=true;
                inventory.RemoveItem(new Item { itemType = Item.ItemType.HealthPotion, amount = 1 });
                break;
            case Item.ItemType.MP:
                PlayerExp.mpEXP = true;
                inventory.RemoveItem(new Item { itemType = Item.ItemType.MP, amount = 1 });
                break;
            case Item.ItemType.Apple:
                PlayerHealth.heal = true;
                inventory.RemoveItem(new Item { itemType = Item.ItemType.Apple, amount = 1 });
                break;
            case Item.ItemType.Shield:
                ////////////////
                inventory.RemoveItem(new Item { itemType = Item.ItemType.Shield, amount = 1 });
                break;
            case Item.ItemType.Coins:
                ////////////////
                inventory.RemoveItem(new Item { itemType = Item.ItemType.Coins, amount = 1 });
                break;
            case Item.ItemType.Cloak:
                ////////////////
                inventory.RemoveItem(new Item { itemType = Item.ItemType.Cloak, amount = 1 });
                break;

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        ItemWorld itemWorld = other.GetComponent<ItemWorld>();
        if (itemWorld != null)
        {
            inventory.AddItem(itemWorld.GetItem());
            itemWorld.DestroySelf();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

    }
}
