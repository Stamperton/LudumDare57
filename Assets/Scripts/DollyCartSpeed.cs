using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class DollyCartSpeed : MonoBehaviour
{
    public static DollyCartSpeed Instance;

    private void Awake()
    {
        Instance = this;
    }

    [SerializeField] CinemachineDollyCart dollyCart;
    [SerializeField] CinemachinePathBase dollyPath;

    float currentSpeed;
    float targetSpeed;

    public float GetPathLength() => dollyPath.PathLength;
    public float GetPathPosition() => dollyCart.m_Position;

    void Start()
    {
        ShipControls.OnShipChangeSpeed += ShipControls_OnShipChangeSpeed;

        dollyCart.m_Position = 1;
    }

    private void Update()
    {
        dollyCart.m_Speed = Mathf.Lerp(targetSpeed, currentSpeed, Time.deltaTime);
    }

    private void ShipControls_OnShipChangeSpeed(object sender, float e)
    {
        currentSpeed = dollyCart.m_Speed;
        targetSpeed = e;
    }
}
