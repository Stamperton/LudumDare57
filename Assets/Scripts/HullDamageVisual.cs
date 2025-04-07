using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class HullDamageVisual : InteractableBase
{
    public enum DamageIntensity { Superficial, Low, Medium, High }
    [SerializeField] DamageIntensity damageIntensity = DamageIntensity.Superficial;
    [SerializeField] GameObject[] damageVisualBreach;
    [SerializeField] GameObject[] damageVisualBreak;
    [SerializeField] int currentWaterLevel = 0;
    [SerializeField] float healthTimer = 10;
    [SerializeField] bool isRepairing = false;
    bool useBreach = false;

    private void OnEnable()
    {
        ResetAllDamagePrefabs();

        int _check = Random.Range(0, 100);
        useBreach = _check > 50;

        if (useBreach)
            damageVisualBreach[0].SetActive(true);
        else
            damageVisualBreak[0].SetActive(true);

    }

    public void Update()
    {
        if (isRepairing)
            healthTimer += 6 * Time.deltaTime;
        else
            healthTimer -= 1 * Time.deltaTime;

        if (healthTimer <= 0)
        {
            healthTimer = 10;
            switch (damageIntensity)
            {
                case DamageIntensity.Superficial:
                    damageIntensity = DamageIntensity.Low;
                    break;
                case DamageIntensity.Low:
                    damageIntensity = DamageIntensity.Medium;
                    break;
                case DamageIntensity.Medium:
                    damageIntensity = DamageIntensity.High;
                    break;
                default:
                    break;
            }
            SetSpecificDamageLevel(damageIntensity);
        }
        else if (healthTimer >= 10)
        {
            healthTimer = 0;
            switch (damageIntensity)
            {
                case DamageIntensity.Superficial:
                    OnInteractionEnd();
                    this.gameObject.SetActive(false);
                    currentWaterLevel = 0;
                    break;
                case DamageIntensity.Low:
                    damageIntensity = DamageIntensity.Superficial;
                    break;
                case DamageIntensity.Medium:
                    damageIntensity = DamageIntensity.Low;
                    break;
                case DamageIntensity.High:
                    damageIntensity = DamageIntensity.Medium;
                    break;
                default:
                    break;
            }
            SetSpecificDamageLevel(damageIntensity);
        }

    }

    public override void OnInteractionStart(PlayerInteraction.PlayerTool currentPlayerTool)
    {
        if (currentPlayerTool == requiredTool)
        {
            PlayerInteraction.Instance.SetToolActive(true);
            isRepairing = true;
        }
    }

    public override void OnInteractionEnd()
    {
        PlayerInteraction.Instance.SetToolActive(false);
        isRepairing = false;
    }

    public int GetCurrentWaterLevel()
    {
        return currentWaterLevel;
    }

    public void SetSpecificDamageLevel(DamageIntensity damageIntensity)
    {
        ResetAllDamagePrefabs();
        this.damageIntensity = damageIntensity;

        int targetPrefabIndex = 0;

        switch (damageIntensity)
        {
            case DamageIntensity.Superficial:
                targetPrefabIndex = 0;
                currentWaterLevel = 0;
                break;
            case DamageIntensity.Low:
                targetPrefabIndex = 1;
                currentWaterLevel = 1;
                break;
            case DamageIntensity.Medium:
                targetPrefabIndex = 2;
                currentWaterLevel = 2;
                break;
            case DamageIntensity.High:
                targetPrefabIndex = 3;
                currentWaterLevel = 3;
                break;
            default:
                break;
        }

        targetPrefabIndex = Mathf.Clamp(targetPrefabIndex, 0, damageVisualBreach.Length - 1);
        if (useBreach)
            damageVisualBreach[targetPrefabIndex].SetActive(true);
        else
            damageVisualBreak[targetPrefabIndex].SetActive(true);

    }

    public void ResetAllDamagePrefabs()
    {
        for (int i = 0; i < damageVisualBreach.Length; i++)
            damageVisualBreach[i].SetActive(false);
        for (int i = 0; i < damageVisualBreak.Length; i++)
            damageVisualBreak[i].SetActive(false);
    }
}
