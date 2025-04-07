using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrillController : MonoBehaviour
{
    [SerializeField] LeverInteractable startLever;
    [SerializeField] AudioClip errorAudio;
    [SerializeField] RotateObject rotateObject;
    [SerializeField] GameObject bloodParticles;
    AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    bool drillReady = false;

    public void TryActivateDrill()
    {
        drillReady = (DollyCartSpeed.Instance.GetPathLength() - DollyCartSpeed.Instance.GetPathPosition() < 10f);

        if (!drillReady)
        {
            audioSource.PlayOneShot(errorAudio);
            StartCoroutine(startLever.CO_LeverFlip());
        }
        else
        {
            audioSource.Play();
            rotateObject.SetActive(true);
            SubmarineManager.Instance.EndLevel();
            if (SubmarineManager.Instance.GetCurrentLevel() == SubmarineManager.Level.Last)
                bloodParticles.SetActive(true);
        }
    }
}
