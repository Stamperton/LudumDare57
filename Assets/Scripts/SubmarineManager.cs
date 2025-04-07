using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SubmarineManager : MonoBehaviour
{
    public static SubmarineManager Instance { get; private set; }

    CinemachineImpulseSource screenShakeSource;
    [SerializeField] Animator cutSceneAnimator;

    public enum Level { First, Second, Last }
    [SerializeField] Level currentLevel;

    [Header("SubmarineStats")]
    [SerializeField] float currentHealth;
    [SerializeField] float maxHealth;

    [Header("Damage")]
    [SerializeField] GameObject floodWater;
    [SerializeField] GameObject floodVolume;
    [SerializeField] float waterLevel = 0;
    int waterGainRate = 0;
    int waterGainFromBrokenPipes = 0;

    [SerializeField] List<HullDamageVisual> hullDamageVisuals = new List<HullDamageVisual>();
    [SerializeField] List<ComputerReboot> rebootScreens = new List<ComputerReboot>();

    bool levelEnded = false;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Debug.LogError("Multiple SubmarineManager in Scene!");
            Destroy(this.gameObject);
        }

        screenShakeSource = GetComponent<CinemachineImpulseSource>();

        currentHealth = maxHealth;
        levelEnded = false;
    }

    public bool IsGameOver() => levelEnded;

    public void BrokenPipe()
    {
        waterGainFromBrokenPipes += 2;
    }

    public void FixedPipe()
    {
        waterGainFromBrokenPipes -= 2;
    }

    public Level GetCurrentLevel() => currentLevel;

    public void TakeDamageFromCollision()
    {
        currentHealth -= 25f;
        ScreenShake(4);
        SetHullBreachesActive(UnityEngine.Random.Range(1, 4));
        ScreenMalfunction();
        ScreenMalfunction();
    }

    private void SetHullBreachesActive(int breachAmount = 1)
    {
        for (int i = 0; i < breachAmount; i++)
        {
            int randomBreach = UnityEngine.Random.Range(0, hullDamageVisuals.Count);
            hullDamageVisuals[randomBreach].gameObject.SetActive(true);
        }
    }

    private void Start()
    {
        currentHealth = maxHealth;
        StartLevel();
    }

    private void LateUpdate()
    {
        if (levelEnded)
            return;

        waterGainRate = waterGainFromBrokenPipes;
        foreach (HullDamageVisual hullDamage in hullDamageVisuals)
        {
            waterGainRate += hullDamage.GetCurrentWaterLevel();
        }

        waterLevel += waterGainRate * Time.deltaTime;

        float floodScaleY = (waterLevel * 0.02f);
        //Debug.Log(floodScaleY);

        floodWater.transform.localScale = new Vector3(20, floodScaleY, 20);
        floodVolume.transform.localScale = new Vector3(20, floodScaleY, 20);

        if (waterLevel >= 240 && levelEnded == false)
        {
            levelEnded = true;
            GAMEOVER();
        }
    }

    public int GetWaterGainRate()
    {
        return waterGainRate;
    }

    public void PipeOverload()
    {
        currentHealth -= 10;
        ScreenShake(1);
        SetHullBreachesActive(UnityEngine.Random.Range(0, 3));
        ScreenMalfunction();

    }

   public void ScreenShake(float intensity)
    {
        screenShakeSource.GenerateImpulse(intensity);
    }

    public void ScreenMalfunction()
    {
        for (int i = 0; i < rebootScreens.Count; i++)
        {
            int screen = UnityEngine.Random.Range(0, rebootScreens.Count);
            rebootScreens[screen].gameObject.SetActive(true);
        }
    }

    public string cutSceneText;


    public void StartLevel()
    {
        switch (currentLevel)
        {
            case Level.First:
                cutSceneText = "OPERATOR LD57\r\n\r\nDESCEND INTO HOSTILE WATERS. \r\n\r\nSTAY SAFE.\r\n\r\nSTICK TO THE MISSION";
                cutSceneAnimator.Play("Intro");
                break;
            case Level.Second:
                cutSceneText = "OPERATOR LD57\r\n\r\nNEW DRILL SITE CONFIRMED. \r\n\r\nYOU MUST GO DEEPER.";
                cutSceneAnimator.Play("Intro");
                break;
            case Level.Last:
                cutSceneText = "OPERATOR LD57\r\n\r\nYOU CANNOT GO BACK NOW. \r\n\r\nWE NEED MORE. \r\n\r\nFIND OUR SALVATION.";
                cutSceneAnimator.Play("Intro");
                break;
            default:
                break;
        }
    }

    public void EndLevel()
    {
        levelEnded = true;

        switch (currentLevel)
        {
            case Level.First:
                cutSceneText = "OPERATOR LD57\r\n\r\nCONGRATULATIONS. \r\n\r\nMINOR DRILL SITE ACTIVE.\r\n\r\nBUT IT IS NOT ENOUGH";
                cutSceneAnimator.Play("Outro");
                break;
            case Level.Second:
                cutSceneText = "OPERATOR LD57\r\n\r\nYOU CONTINUE TO EXCEL. \r\n\r\nWE ARE SURPRISED AND PLEASED.";
                cutSceneAnimator.Play("Outro");
                break;
            case Level.Last:
                cutSceneText = "OPERATOR LD57\r\n\r\nTHIS IS THE END. \r\n\r\nDO NOT FEAR. \r\n\r\nALL WILL FINALLY BE WELL.";
                cutSceneAnimator.Play("GameWin");
                //StartCoroutine(BrokenAnimTransitonWorkaround());
                CommsReadout.Instance.PlayEndGameMessage();
                break;
            default:
                break;
        }
    }

    public void GAMEOVER()
    {
        levelEnded = true;
        cutSceneText = "OPERATOR LD57 LOST\r\n\r\nYOU HAVE FAILED. \r\n\r\nANOTHER WILL BE FOUND";
        cutSceneAnimator.Play("Death");
    }

}
