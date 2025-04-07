using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioClipRandomiser : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] Vector2 delayRange;
    float timer;

    void GetNewDelayRange()
    {
        timer = Random.Range(delayRange.x, delayRange.y);
    }

    private void Update()
    {
        if (audioSource.isPlaying == false)
            timer -= Time.deltaTime;

        if (timer <= 0)
        {
            GetNewDelayRange();
            audioSource.Play();
        }
    }
}
