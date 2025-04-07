using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PipeValveInteractable : InteractableBase
{
    AudioSource audioSource;
    [SerializeField]
    TMPro.TMP_Text pressureDebugText;
    [SerializeField] float valvePressureMax = 100;
    [SerializeField] float valvePressureCurrent;
    [SerializeField] float valvePressureGainRate = 1;
    [SerializeField] Transform valveVisual;
    [SerializeField] Transform valveNeedleVisual;
    [SerializeField] float valveVisualRotationMultiplier = 5;
    [SerializeField] BreakablePipe attachedPipe;
    [Header("Damage")]
    [SerializeField] protected Vector2 damageOnOverloadRange;
    [SerializeField] protected float damageScreenShakeIntensity = 1;

    enum State { PressureRising, PressureFalling, Broken }
    [SerializeField] State currentState;

    bool isActive = true;

    private void Start()
    {
        ResetValve();
    }

    private void OnEnable()
    {
        ResetValve();
    }

    void ResetValve()
    {
        audioSource = GetComponent<AudioSource>();

        valvePressureCurrent = Random.Range(0, valvePressureMax * 0.33f);
        valvePressureGainRate = Random.Range(0.8f, 1.2f);

        currentState = State.PressureRising;
        isActive = true;
    }

    private void Update()
    {
        if (SubmarineManager.Instance.IsGameOver()) return;

        switch (currentState)
        {
            case State.PressureRising:
                valvePressureCurrent += 1 * valvePressureGainRate * Time.deltaTime;
                valvePressureCurrent = Mathf.Clamp(valvePressureCurrent, 0, valvePressureMax);
                if (valvePressureCurrent >= valvePressureMax)
                {
                    valvePressureCurrent = 0;
                    SubmarineManager.Instance.PipeOverload();
                    attachedPipe.BreakPipe(this);
                    currentState = State.Broken;
                }

                break;
            case State.PressureFalling:
                valvePressureCurrent -= 15 * Time.deltaTime;
                valvePressureCurrent = Mathf.Clamp(valvePressureCurrent, 0, valvePressureMax);
                if (valvePressureCurrent == 0)
                    audioSource.Stop();
                break;
            case State.Broken:
                break;
            default:
                break;
        }

        UpdateValveDisplay();
    }

    public void RepairPipe()
    {
        currentState = State.PressureRising;
    }

    public override void OnInteractionEnd()
    {
        if (currentState == State.PressureFalling)
        {
            currentState = State.PressureRising;
            audioSource.Stop();
        }
    }

    public override void OnInteractionStart(PlayerInteraction.PlayerTool currentPlayerTool)
    {
        if (currentPlayerTool == requiredTool && currentState != State.Broken)
        {
            currentState = State.PressureFalling;
            audioSource.Play();
        }
    }

    void UpdateValveDisplay()
    {
        if (pressureDebugText.isActiveAndEnabled)
            pressureDebugText.text = valvePressureCurrent.ToString("###") + " PSI";

        valveVisual.localRotation = Quaternion.Euler(valvePressureCurrent * valveVisualRotationMultiplier, valveVisual.localRotation.y, valveVisual.localRotation.z);
        valveNeedleVisual.localRotation = Quaternion.Euler(valveNeedleVisual.localRotation.x, valveNeedleVisual.localRotation.y, +valvePressureCurrent * 2.85f);
    }
}
