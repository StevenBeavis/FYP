using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public GameObject pau;
    public GameObject Htp;
    public GameObject Htp2;
    
    public Text highScore;
    public GameObject ds;



    public string menuScene = "Main Menu";
    public string gameScene = "MainScene";

    public Fade Fader;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            //Inv();
        }
        if (PlayerHealth.hs == true)
        {
            highScore.text = PlayerPrefs.GetInt("level", 0).ToString();
            PlayerHealth.hs = false;
        }
        if ((ds.activeSelf) && Input.GetKeyDown(KeyCode.R))
        {
            
            Restart();
            
        }
        if ((ds.activeSelf) && Input.GetKeyDown(KeyCode.M))
        {
            Menu();

        }
        if ((pau.activeSelf)&& Input.GetKeyDown(KeyCode.R))
        {
            Restart();

        }
        if ((pau.activeSelf) && Input.GetKeyDown(KeyCode.M))
        {
            Menu();

        }
        if ((pau.activeSelf) && Input.GetKeyDown(KeyCode.Space))
        {
            HTP();

        }
        if ((Htp.activeSelf) && Input.GetKeyDown(KeyCode.Space))
        {
            HTP2();

        }
        if ((Htp2.activeSelf) && Input.GetKeyDown(KeyCode.Backspace))
        {
            HTP();

        }
        if ((Htp.activeSelf) && Input.GetKeyDown(KeyCode.Backspace))
        {
            Back();

        }
    }

    
    public void Pause()
    {
        pau.SetActive(!pau.activeSelf);

        if (pau.activeSelf)
        {
            Time.timeScale = 0f;

        }
        else
        {
            Time.timeScale = 1f;
        }

    }

    public void Restart()
    {
        Pause();

        Fader.FadeT(gameScene);
    }
    public void Menu()
    {
        Pause();
        Fader.FadeT(menuScene);

    }
    public void HTP()
    {
        if (pau.activeSelf||Htp2.activeSelf)
        {
            pau.SetActive(false);
            Htp2.SetActive(false);
            Htp.SetActive(true);
        }



    }
    public void HTP2()
    {
        if (pau.activeSelf || Htp.activeSelf)
        {
            pau.SetActive(false);
            Htp.SetActive(false);
            Htp2.SetActive(true);
        }
    }
    public void Back()
    {
        pau.SetActive(true);
        Htp.SetActive(false);
        Htp2.SetActive(false);
    }
}
