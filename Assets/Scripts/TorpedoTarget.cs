using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorpedoTarget : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SubmarineManager.Instance.TakeDamageFromCollision();
            Destroy(this.gameObject);
        }

        else if (other.CompareTag("Torpedo"))
        {
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
    }
}
