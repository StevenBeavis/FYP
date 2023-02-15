using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Fade : MonoBehaviour
{

    public Image black;
    public AnimationCurve curve;

    void Start()
    {
        StartCoroutine(FadeIn());

    }
    public void FadeT(string scene)
    {
        StartCoroutine(Fadeout(scene));
    }
    IEnumerator FadeIn()
    {
        float q = 1f;

        while (q > 0f)
        {
            q -= Time.deltaTime * 1f;
            float a = curve.Evaluate(q);
            black.color = new Color(0f, 0f, 0f, a);
            yield return 0;
        }
    }
    IEnumerator Fadeout(string scene)
    {
        float q = 0f;

        while (q < 1f)
        {
            q += Time.deltaTime * 1f;
            float a = curve.Evaluate(q);
            black.color = new Color(0f, 0f, 0f, a);
            yield return 0;
        }

        SceneManager.LoadScene(scene);

    }



}
