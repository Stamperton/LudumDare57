using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SingleButtonInteractable : InteractableBase
{
    [SerializeField] Animator switchVisualObject;

    [SerializeField] float pressResetDelay = 0.2f;

    public UnityEvent OnInteract;

    float timer = 0f;
    bool canInteract = true;

    private void Update()
    {
        if (timer > 0f && !canInteract)
            timer -= Time.deltaTime;

        if (timer <= 0f && !canInteract)
            canInteract = true;
    }


    public override void OnInteractionEnd()
    {
        //Nothing
    }

    public override void OnInteractionStart(PlayerInteraction.PlayerTool currentPlayerTool)
    {
        if (!canInteract || currentPlayerTool != requiredTool)
            return;

        timer = pressResetDelay;
        canInteract = false;
        switchVisualObject.SetTrigger("Trigger");
        OnInteract?.Invoke();
    }


}
