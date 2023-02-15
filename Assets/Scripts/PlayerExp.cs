using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerExp : MonoBehaviour
{
    public int level;
    public float currentExp;
    public float requiredExp;

    public static bool BasicExp;
    public static bool mpEXP;
 
    private float lerpTimer;
    private float delayTimer;
    [Header("UI")]
    public Image frontExpBar;
    public Image backExpBar;
    public Text levelText;
    public Text expText;
    [Header("Multiplier")]
    [Range(1f,300f)]
    public float additionMulti = 300;
    [Range(2f, 4f)]
    public float powerMulti = 2;
    [Range(7f, 14f)]
    public float divisionMulti = 7;


    void Start()
    {
        frontExpBar.fillAmount = currentExp / requiredExp;
        backExpBar.fillAmount = currentExp / requiredExp;
        requiredExp = CalRequiredExp();
        levelText.text = "" + level;
        PlayerPrefs.GetFloat("exp", currentExp);
        PlayerPrefs.GetInt("level", level);
    }

   
    void Update()
    {
        UpdateExpUI();
        if (Input.GetKeyDown(KeyCode.Equals))
            GainExpFlatRate(20);
        if (currentExp > requiredExp)
            LevelUp();
        if (BasicExp == true)
        {
            BasicExp = false;
            GainExpFlatRate(20);
        }
        if (mpEXP == true)
        {
            mpEXP = false;
            GainExpFlatRate(100);
        }

    }
    public void UpdateExpUI()
    { 
        float ExpFrac = currentExp / requiredExp;
        float FExp = frontExpBar.fillAmount;
        if (FExp < ExpFrac)
        {
            delayTimer += Time.deltaTime;
            backExpBar.fillAmount = ExpFrac;
            if (delayTimer > 3)
            {
                lerpTimer += Time.deltaTime;
                float percentComplete = lerpTimer / 4;
                frontExpBar.fillAmount = Mathf.Lerp(FExp, backExpBar.fillAmount, percentComplete);
            }
        }
        expText.text = currentExp + "/" + requiredExp;
    }

    public void GainExpFlatRate(float expGained)
    {
        currentExp += expGained*level;
        //save Exp
        PlayerPrefs.SetFloat("exp", currentExp);
        lerpTimer = 0f;
    }
    public void GainExpScaleable(float expGained, int passedLevel)
    {
        if (passedLevel < level)
        {
            float multiplier = 1 + (level - passedLevel) * 0.1f;
            currentExp += expGained * multiplier;
        }
        else
        {
            currentExp += expGained;
        }
        lerpTimer = 0f;
        delayTimer = 0f;
    }
    public void LevelUp() 
    {
        level++;
        frontExpBar.fillAmount = 0f;
        backExpBar.fillAmount = 0f;
        currentExp = Mathf.RoundToInt(currentExp - requiredExp);
        GetComponent<PlayerHealth>().IncreaseHealth(level);
        requiredExp = CalRequiredExp();
        levelText.text = "" + level;
        //save level
        if (level > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("level", level);
            
        }
        
        
    }
    private int CalRequiredExp()
    {
        int solveForReqExp = 0;
        for (int levelCycle = 1; levelCycle <= level; levelCycle++)
        {
            solveForReqExp += (int)Mathf.Floor(levelCycle + additionMulti * Mathf.Pow(powerMulti, levelCycle / divisionMulti));
        }
        return solveForReqExp / 4;
    }
}
