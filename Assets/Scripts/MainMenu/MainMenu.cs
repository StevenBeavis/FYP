using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
    public string level = "MainScene";

    public Fade Fader;

    public void Play()
    {
        Fader.FadeT(level);
        ThirdPersonMovement.isDead = false;



    }

    public void Quit()
    {
        Application.Quit();
    }
}
