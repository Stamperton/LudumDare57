using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractableBase : MonoBehaviour
{
    public class OnAnyInteractableOverloadEventArgs : EventArgs
    {
        public float damage;
        public float screenShakeIntensity;
    }

    [SerializeField] protected PlayerInteraction.PlayerTool requiredTool;

    public abstract void OnInteractionStart(PlayerInteraction.PlayerTool currentPlayerTool);

    public abstract void OnInteractionEnd();
}
