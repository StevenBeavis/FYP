using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyOverworldSpawner : MonoBehaviour
{
    [Header("Basic Troll")]
    public GameObject basicEnemy;
    [Header("Basic Golem")]
    public GameObject aoeEnemy;

    public int xPos;
    public int zPos;

    public static int enemyCount;
    public bool enemySpawn = false;

    void Start()
    {
        StartCoroutine(SpawnEnemy());
        //GameController.startBattle = true;
        
    }

    IEnumerator SpawnEnemy()
    {
        xPos = Random.Range(25, 45);
        zPos = Random.Range(125, 135);
        enemyCount += 1;
        var Obj = GameObject.Instantiate(basicEnemy, new Vector3(xPos, 3, zPos), Quaternion.identity);
        Obj.transform.parent = GameObject.Find("World").transform;
        //Instantiate(basicEnemy, new Vector3(xPos, 3, zPos), Quaternion.identity);
        yield return new WaitForSeconds(1);
        enemySpawn = true;
    }
    
    public void CheckCount()
    {
        if (enemySpawn == true)
        {
            if (enemyCount <= 0)
            {
                StartCoroutine(SpawnEnemy());
                //enemySpawn = false;

            }

        }

    }

    // Update is called once per frame
    void Update()
    {
        CheckCount();
    }
}
