using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreachLight : MonoBehaviour
{
    [SerializeField] GameObject targetObject;
    [SerializeField] MeshRenderer meshRenderer;
    [SerializeField] Material onMaterial, offMaterial;
    [SerializeField] GameObject lightGO;

    private void Update()
    {
        lightGO.SetActive(targetObject.activeInHierarchy);

        if (targetObject.activeInHierarchy)
            meshRenderer.material = onMaterial;
        else meshRenderer.material = offMaterial;
    }
}