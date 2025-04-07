using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorpedoLauncher : InteractableBase
{
    public static TorpedoLauncher Instance { get; private set; }

    bool isInteractable = true;
    bool isTorpedoLoaded = false;
    Animator torpedoAnimator;
    [SerializeField] GameObject loadedTorpedo;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        torpedoAnimator = GetComponent<Animator>();
        //TorpedoLaunchCodes.OnTorpedoLaunched += TorpedoLaunchCodes_OnTorpedoLaunched;
        loadedTorpedo.SetActive(false);
        torpedoAnimator.SetBool("Open", true);
        isTorpedoLoaded = false;
        isInteractable = true;
    }

    private void TorpedoLaunchCodes_OnTorpedoLaunched(object sender, System.EventArgs e)
    {
    }

    public void OnTorpedoLaunched()
    {
        loadedTorpedo.SetActive(false);
        torpedoAnimator.SetBool("Open", true);
        isInteractable = true;
    }

    public override void OnInteractionEnd()
    {
        //throw new System.NotImplementedException();
    }

    public bool IsTorpedoLoaded() => isTorpedoLoaded;

    public override void OnInteractionStart(PlayerInteraction.PlayerTool currentPlayerTool)
    {
        if (currentPlayerTool != requiredTool)
            return;
        if (!isInteractable)
            return;

        isTorpedoLoaded = true;
        PlayerInteraction.Instance.RemovePlayerTools();
        loadedTorpedo.SetActive(true);
        torpedoAnimator.SetBool("Open", false);
        isInteractable = false;

    }
}
