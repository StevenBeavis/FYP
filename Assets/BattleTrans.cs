using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleTrans : MonoBehaviour
{
    //Public Variables
    public GameObject player;
    
    public GameObject world;
    
    public static bool inventoryHider;
    public static bool defeated;
    public static bool battle;
    public static bool unload;
    public bool isLoaded;
    public string levelToLoad;

    public Animator transition;
    public float transitiontime = 1.0f;
    public static int choose;


    //public static TransitionHandler instance;

    void Start()
    {
        //instance = this;

        
        isLoaded = false;

        inventoryHider = false;
        /*
        //LoadInfo.LoadAllInfo();
        
       
        //SaveInfo.SaveAllInfo();*/
    }

    private void Update()
    {
        //If enemy is defeated in battle, respawn same enemy after a certain amount of time for grinding purposes
        
        if (Input.GetKeyDown(KeyCode.V)|| battle == true)
        {
            battle = false;
            LoadBattleScene();
        }
        if (Input.GetKeyDown(KeyCode.B)|| defeated == true)
        {
            defeated = false;
            UnloadBattleScene();
        }
        
    }

    public void LoadBattleScene()
    {
        isLoaded = true;
        inventoryHider = true;
        StartCoroutine(BattleTransition());
        
    }

    public void UnloadBattleScene()
    {
        isLoaded = false;
        inventoryHider = false;
        StartCoroutine(UnloadBattleTransition());
        
    }

    //Load battle scene and deactivate current scene
    IEnumerator BattleTransition()
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitiontime);
        world.SetActive(false);
        AsyncOperation op = SceneManager.LoadSceneAsync(levelToLoad, LoadSceneMode.Additive);
        yield return op;
        transition.SetTrigger("End");
    }

    IEnumerator UnloadBattleTransition()
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitiontime);
        SceneManager.UnloadScene(levelToLoad);
        //yield return op;
        //yield return new WaitForSeconds(1.5f);
        
        world.SetActive(true);
        //SceneManager.UnloadSceneAsync(levelToLoad);
        

        /*
        if (CombatManager.won == true)
        {
            enemy = GameObject.FindWithTag("InBattle");
            enemy.SetActive(false);
            defeated = true;
            respawnTimer += Time.deltaTime;
        }
        */

        transition.SetTrigger("End");
    }
}
