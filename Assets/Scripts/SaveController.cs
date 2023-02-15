using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SaveController : MonoBehaviour
{
    private void Start()
    {
    
    }

    public PlayerHealth health;
    public static void OnEnterBattle()
    { 
        string activeScene = SceneManager.GetActiveScene().name;
        PlayerPrefs.SetString("LevelSaved", activeScene);
        Debug.Log("Saved Active Scene");
        //PlayerPrefs.SetFloat("PlayerHealth", health.healthText);
    }
    
        
}
