using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField] PlayerController playerController;
    [SerializeField] LockOn lockOn;
    [SerializeField] ThirdPersonMovement thirdPersonMovement;

    public string SceneName;

    public static bool startBattle;
    public static bool endBattle;


    GameState state;
    public enum GameState {Roaming, Dialogue, Battle}
    // Start is called before the first frame update
    void Start()
    {
        
        Cursor.lockState = CursorLockMode.Confined;
        
        state = GameState.Roaming;
        DialogueManager.Instance.OnShowDialogue += ShowDialogue;
        DialogueManager.Instance.OnCloseDialogue += CloseDialogue;
    }

    void ShowDialogue()
    {
        state = GameState.Dialogue;
    }
    void CloseDialogue()
    {
        if (state == GameState.Dialogue)
            state = GameState.Roaming;
    }

    void Battle()
    {
        if (state == GameState.Roaming)
        {
            state = GameState.Battle;
        }
        
    }
    void EndBattle()
    {
        if (state == GameState.Battle)
        {
            state = GameState.Roaming;
        }
    }



    private void Update()
    {
        /*if (Input.GetKey(KeyCode.Q))
        {
            
            if (Cursor.visible == false)
            { Cursor.visible = true;
            }
            if (Cursor.visible == true)
            {
                Cursor.visible = false;
            }

        }*/
        if (startBattle==true)
        {
            startBattle = false;
            Battle();
        }
        if (endBattle == true)
        {
            endBattle = false;
            EndBattle();
        }
        if (state == GameState.Roaming)
        {
            playerController.HandleUpdate();
        }
        else if (state == GameState.Dialogue)
        {
            
            DialogueManager.Instance.HandleUpdate();

        }
        else if (state == GameState.Battle)
        {

            //lockOn.HandleUpdate();
            //thirdPersonMovement.HandleUpdate();
            //SceneManager.LoadScene(1, LoadSceneMode.Additive);
            

        }
        
    }
}
