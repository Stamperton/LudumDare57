using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class WarningBreachScreen : MonoBehaviour
{
    public static WarningBreachScreen Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
    }

    public GameObject warningText;
    float timer;

    bool isActive;

    private void Update()
    {
        isActive = SubmarineManager.Instance.GetWaterGainRate() > 0;

        if (!isActive)
            warningText.SetActive(false);

        else
            timer -= Time.deltaTime;

        if (timer <= 0)
        {
            warningText.SetActive(!warningText.activeInHierarchy);
            timer = 0.67f;
        }
    }
}
