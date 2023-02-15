using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemySpawner : MonoBehaviour
{
    [Header("Basic Troll")]
    public GameObject basicEnemy;
    [Header("Basic Golem")]
    public GameObject aoeEnemy;

    public int xPos;
    public int zPos;

    public static int enemyCount;
    public bool enemySpawn = false;
   
    public Camera victoryCam;



    void Start()
    {
        enemyCount = 0;
        ChooseSpawn();
        GameController.startBattle = true;
        victoryCam.gameObject.SetActive(false);
    }
    void ChooseSpawn()
    {
        if (BattleTrans.choose == 0)
        {
            StartCoroutine(SpawnEnemy());
        }
        else if (BattleTrans.choose == 1)
        { 
            StartCoroutine(SpawnEnemy1());
        }
        else if(BattleTrans.choose == 2)
        {
            StartCoroutine(SpawnEnemy2());
        }
        else if (BattleTrans.choose == 3)
        {
            StartCoroutine(SpawnEnemy3());
        }
        else if(BattleTrans.choose == 4)
        {
            StartCoroutine(SpawnEnemy4());
        }
        else if (BattleTrans.choose == 5)
        {
            StartCoroutine(SpawnEnemy5());
        }

    }
    IEnumerator SpawnEnemy() 
    {
        xPos = Random.Range(-25, 25);
        zPos = Random.Range(0, 8);
        enemyCount += 1;
        Instantiate(basicEnemy , new Vector3(xPos, 0, zPos), Quaternion.identity);
        yield return new WaitForSeconds(1);
        enemySpawn = true;
    }
    IEnumerator SpawnEnemy1()
    {
        xPos = Random.Range(-25, 25);
        zPos = Random.Range(0, 8);
        enemyCount += 1;
        Instantiate(basicEnemy, new Vector3(xPos, 0, zPos), Quaternion.identity);
        xPos = Random.Range(-25, 25);
        zPos = Random.Range(0, 8);
        enemyCount += 1;
        Instantiate(basicEnemy, new Vector3(xPos, 0, zPos), Quaternion.identity);
        xPos = Random.Range(-25, 25);
        zPos = Random.Range(0, 8);
        enemyCount += 1;
        Instantiate(basicEnemy, new Vector3(xPos, 0, zPos), Quaternion.identity);
        yield return new WaitForSeconds(1);
        enemySpawn = true;
    }
    IEnumerator SpawnEnemy2()
    {
        xPos = Random.Range(-25, 25);
        zPos = Random.Range(0, 8);
        enemyCount += 1;
        Instantiate(aoeEnemy, new Vector3(xPos, 0, zPos), Quaternion.identity);
        yield return new WaitForSeconds(1);
        enemySpawn = true;
    }
    IEnumerator SpawnEnemy3()
    {
        xPos = Random.Range(-25, 25);
        zPos = Random.Range(0, 8);
        enemyCount += 1;
        Instantiate(aoeEnemy, new Vector3(xPos, 0, zPos), Quaternion.identity);
        
        xPos = Random.Range(-25, 25);
        zPos = Random.Range(0, 8);
        enemyCount += 1;
        Instantiate(basicEnemy, new Vector3(xPos, 0, zPos), Quaternion.identity);
        yield return new WaitForSeconds(1);
        enemySpawn = true;
    }
    IEnumerator SpawnEnemy4()
    {
        xPos = Random.Range(-25, 25);
        zPos = Random.Range(0, 8);
        enemyCount += 1;
        Instantiate(basicEnemy, new Vector3(xPos, 0, zPos), Quaternion.identity);
        xPos = Random.Range(-25, 25);
        zPos = Random.Range(0, 8);
        enemyCount += 1;
        Instantiate(basicEnemy, new Vector3(xPos, 0, zPos), Quaternion.identity);
        xPos = Random.Range(-25, 25);
        zPos = Random.Range(0, 8);
        enemyCount += 1;
        Instantiate(basicEnemy, new Vector3(xPos, 0, zPos), Quaternion.identity);
        yield return new WaitForSeconds(1);
        enemySpawn = true;
        xPos = Random.Range(-25, 25);
        zPos = Random.Range(0, 8);
        enemyCount += 1;
        Instantiate(basicEnemy, new Vector3(xPos, 0, zPos), Quaternion.identity);
        xPos = Random.Range(-25, 25);
        zPos = Random.Range(0, 8);
        enemyCount += 1;
        Instantiate(basicEnemy, new Vector3(xPos, 0, zPos), Quaternion.identity);
        xPos = Random.Range(-25, 25);
        zPos = Random.Range(0, 8);
        enemyCount += 1;
        Instantiate(basicEnemy, new Vector3(xPos, 0, zPos), Quaternion.identity);
        yield return new WaitForSeconds(1);
        enemySpawn = true;
    }
    IEnumerator SpawnEnemy5()
    {
        xPos = Random.Range(-25, 25);
        zPos = Random.Range(0, 8);
        enemyCount += 1;
        Instantiate(aoeEnemy, new Vector3(xPos, 0, zPos), Quaternion.identity);
        xPos = Random.Range(-25, 25);
        zPos = Random.Range(0, 8);
        enemyCount += 1;
        Instantiate(aoeEnemy, new Vector3(xPos, 0, zPos), Quaternion.identity);

        xPos = Random.Range(-25, 25);
        zPos = Random.Range(0, 8);
        enemyCount += 1;
        Instantiate(basicEnemy, new Vector3(xPos, 0, zPos), Quaternion.identity);
        xPos = Random.Range(-25, 25);
        zPos = Random.Range(0, 8);
        enemyCount += 1;
        Instantiate(basicEnemy, new Vector3(xPos, 0, zPos), Quaternion.identity);
        xPos = Random.Range(-25, 25);
        zPos = Random.Range(0, 8);
        enemyCount += 1;
        Instantiate(basicEnemy, new Vector3(xPos, 0, zPos), Quaternion.identity);
        yield return new WaitForSeconds(1);
        enemySpawn = true;
    }
    IEnumerator EndScreen()
    {

        victoryCam.gameObject.SetActive(true);
        yield return new WaitForSeconds(5);
        BattleTrans.defeated = true;
    }
    public void CheckCount() 
    {
        if (enemySpawn == true)
        {
            if (enemyCount <= 0)
            {   //player win,
                PlayerExp.BasicExp = true;
                //add exp
                //move back to overworld
                Debug.Log("Enemies Defeated");
                
                GameController.endBattle = true;
                
                StartCoroutine(EndScreen());
                   
                
                if (PlayerPrefs.HasKey("LevelSaved"))
                {
                    /*
                    string levelToLoad = PlayerPrefs.GetString("LevelSaved");
                    SceneManager.LoadScene(levelToLoad);
                    */
                }
                enemySpawn = false;
                BattleTrans.choose++;
                if (BattleTrans.choose >= 6)
                {
                    BattleTrans.choose = 3;
                }
            }

        }
    
    }

    // Update is called once per frame
    void Update()
    {
        CheckCount();
    }
}
