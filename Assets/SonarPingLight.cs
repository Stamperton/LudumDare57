using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class SonarPingLight : MonoBehaviour
{
    [SerializeField] AudioSource klaxonAudio;
    [SerializeField] MeshRenderer meshRenderer;
    [SerializeField] GameObject lightGO;
    [SerializeField] Material offMaterial;
    [SerializeField] Material onMaterial;

    float flashTimer;
    float activeTimer;
    bool active;
    bool toggle = true;

    private void Start()
    {
        StartCoroutine(SonarPingBug());
    }

    private void Update()
    {
        if (!active)
        {
            meshRenderer.material = offMaterial;
            lightGO.SetActive(false);
            return;
        }

        flashTimer -= Time.deltaTime;
        activeTimer -= Time.deltaTime;

        if (activeTimer <= 0)
            active = false;

        if (flashTimer <= 0)
        {
            flashTimer = 1f;
            if (toggle)
            {
                meshRenderer.material = offMaterial;
                lightGO.SetActive(false);
                toggle = false;
            }
            else
            {
                meshRenderer.material = onMaterial;
                lightGO.SetActive(true);
                toggle = true;
                klaxonAudio.Play();
            }
        }
    }

    private void SonarPing_OnSonarPing(object sender, System.EventArgs e)
    {
        active = true;
        activeTimer = 10;
        flashTimer = 0.5f;
    }

    IEnumerator SonarPingBug()
    {
        yield return new WaitForSeconds(0.5f);
        SonarPing.OnSonarPing += SonarPing_OnSonarPing;
    }
}
