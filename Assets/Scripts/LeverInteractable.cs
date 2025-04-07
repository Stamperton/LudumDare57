using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class LeverInteractable : InteractableBase
{
    [SerializeField] Animator leverVisualObjectAnimator;

    [SerializeField] bool startingState = true;
    bool isOn = false;
    bool isInteractable = true;

    public UnityEvent OnSwitchActive;
    public UnityEvent OnSwitchInactive;

    public override void OnInteractionEnd()
    {
        //
    }

    public override void OnInteractionStart(PlayerInteraction.PlayerTool currentPlayerTool)
    {
        if (isInteractable && currentPlayerTool == requiredTool)
            StartCoroutine(CO_LeverFlip());
    }

    private void Start()
    {
        isOn = startingState;
        if (isOn)
        {
            leverVisualObjectAnimator.Play("Lever_On_Idle");
            OnSwitchActive?.Invoke();
        }
        else
        {
            leverVisualObjectAnimator.Play("Lever_Off_Idle");
            OnSwitchInactive?.Invoke();
        }
    }

    public IEnumerator CO_LeverFlip()
    {
        isInteractable = false;
        leverVisualObjectAnimator.SetTrigger("Trigger");
        yield return new WaitForSeconds(0.8f);
        isOn = !isOn;
        if (isOn)
            OnSwitchActive?.Invoke();
        else
            OnSwitchInactive?.Invoke();
        yield return new WaitForSeconds(0.25f);
        isInteractable = true;
    }


}
