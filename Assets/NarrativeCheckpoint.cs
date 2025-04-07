using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NarrativeCheckpoint : MonoBehaviour
{
    [SerializeField][TextArea] string narrativeText;
    [SerializeField] AudioClip narrativeTTS;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CommsReadout.Instance.AddNewMessage(narrativeText, narrativeTTS);
        }
    }
}
