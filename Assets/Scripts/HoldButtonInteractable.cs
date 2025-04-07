using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HoldButtonInteractable : InteractableBase
{
    [SerializeField] Animator switchVisualObject;

    public UnityEvent OnInteractStart;
    public UnityEvent OnInteractEnd;

    bool isPressed = false;

    private void Start()
    {
        switchVisualObject.SetBool("isPressed", isPressed);
    }

    public override void OnInteractionEnd()
    {
        isPressed = false;
        switchVisualObject.SetBool("isPressed", isPressed);
        PlayerInteraction.Instance.SetToolActive(false);
        OnInteractEnd?.Invoke();
    }

    public override void OnInteractionStart(PlayerInteraction.PlayerTool currentPlayerTool)
    {
        if (currentPlayerTool != requiredTool)
            return;

        PlayerInteraction.Instance.SetToolActive(true);
        isPressed = true;
        switchVisualObject.SetBool("isPressed", isPressed);
        OnInteractStart?.Invoke();
    }
}
