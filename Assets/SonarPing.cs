using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonarPing : MonoBehaviour
{
    public static event EventHandler OnSonarPing;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("SonarPing"))
        {
            OnSonarPing?.Invoke(this, EventArgs.Empty);
            Debug.LogError("SONAR :" + other.gameObject.name);
        }
    }

}
