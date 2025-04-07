using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static InteractableBase;

public class ShipControls : MonoBehaviour
{
    public static event EventHandler<float> OnShipChangeSpeed;

    [SerializeField] Image fillImage;

    float currentSpeed = 2;
    float maxSpeed = 3;
    private void Start()
    {
        StartCoroutine(SpeedOnStartBugFix());
    }

    public void IncreaseSpeed()
    {
        currentSpeed++;
        currentSpeed = Mathf.Clamp(currentSpeed, 0, maxSpeed);
        fillImage.fillAmount = currentSpeed / maxSpeed;
        OnShipChangeSpeed?.Invoke(this, currentSpeed);
    }

    public void DecreaseSpeed()
    {
        currentSpeed--;
        currentSpeed = Mathf.Clamp(currentSpeed, 0, maxSpeed);
        fillImage.fillAmount = currentSpeed / maxSpeed;
        OnShipChangeSpeed?.Invoke(this, currentSpeed);
    }

    public float GetCurrentSpeed() => currentSpeed;


    private IEnumerator SpeedOnStartBugFix()
    {
        yield return new WaitForSeconds(0.2f);
        currentSpeed = 2;
        currentSpeed = Mathf.Clamp(currentSpeed, 0, maxSpeed);
        fillImage.fillAmount = currentSpeed / maxSpeed;
        OnShipChangeSpeed?.Invoke(this, currentSpeed);
    }
}
