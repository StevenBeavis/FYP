using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneEnter1 : MonoBehaviour
{
    public GameObject guard;
    Animator guardAnimator;
    public GameObject playerCam;
    public GameObject cutsceneCam;

    private void Start()
    {
        guard = GameObject.Find("GreetingFootman");
        guardAnimator = guard.GetComponent<Animator>();
        cutsceneCam.SetActive(false);

    }
    void OnTriggerEnter(Collider other)
    {
        this.gameObject.GetComponent<BoxCollider>().enabled = false;
        cutsceneCam.SetActive(true);
        //guard = GameObject.Find("GreetingFootman");
        playerCam.SetActive(false);
        StartCoroutine(FinishCut());
    }

    IEnumerator FinishCut()
    {
        guardAnimator.SetBool("Greet", true);
        yield return new WaitForSeconds(5);
        guardAnimator.SetBool("Greet", false);
        //player.SetActive(true);
        playerCam.SetActive(true);
        cutsceneCam.SetActive(false);
    }
}
