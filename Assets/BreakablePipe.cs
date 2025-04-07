using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class BreakablePipe : InteractableBase
{
    AudioSource audioSource;
    [SerializeField] MeshRenderer meshRenderer;
    [SerializeField] Material brokenMat, fixedMat;
    [SerializeField] ParticleSystem[] particleSystems;
    [SerializeField] AudioClip breakClip;

    PipeValveInteractable thisValve;

    bool isBroken = false;
    bool isRepairing = false;
    float Timer = 0;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        meshRenderer.material = fixedMat;
        SetParticlesActive(false);
    }

    public override void OnInteractionEnd()
    {
        PlayerInteraction.Instance.SetToolActive(false);
        isRepairing = false;
    }

    public override void OnInteractionStart(PlayerInteraction.PlayerTool currentPlayerTool)
    {
        if (currentPlayerTool != requiredTool)
            return;

        PlayerInteraction.Instance.SetToolActive(true);
        isRepairing = true;
    }

    private void Update()
    {
        if (!isBroken) return;

        if (isRepairing)
        {
            Timer += Time.deltaTime;
            if (Timer > 4)
            {
                FixPipe();
                Timer = 0;
            }
        }

    }

    public void BreakPipe(PipeValveInteractable valve)
    {
        thisValve = valve;
        meshRenderer.material = brokenMat;
        isBroken = true;
        audioSource.PlayOneShot(breakClip);
        audioSource.Play();
        SetParticlesActive(true);
        SubmarineManager.Instance.BrokenPipe();
    }

    public void FixPipe()
    {
        thisValve.RepairPipe();
        SetParticlesActive(false);
        audioSource.Stop();
        meshRenderer.material = fixedMat;
        isBroken = true;
        SubmarineManager.Instance.FixedPipe();
    }

    public bool IsPipeBroken()
    {
        return isBroken;
    }

    void SetParticlesActive(bool active)
    {
        foreach (var part in particleSystems)
        {
            if (active)
                part.Play();
            else
                part.Stop();
        }
    }
}
