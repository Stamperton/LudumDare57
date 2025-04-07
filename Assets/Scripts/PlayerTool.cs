using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTool : InteractableBase
{
    [SerializeField] PlayerInteraction.PlayerTool thisTool;
    public override void OnInteractionEnd()
    {
        //
    }

    public override void OnInteractionStart(PlayerInteraction.PlayerTool currentPlayerTool)
    {
        if (currentPlayerTool == requiredTool)
        {
            if (PlayerInteraction.Instance.SetPlayerTool(thisTool))
                Destroy(this.gameObject);
        }
    }
}
