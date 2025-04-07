using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    [SerializeField] Vector3 rotation;
    [SerializeField] float rotationSpeed = 5;
    [SerializeField] bool isActive = true;

    private void Update()
    {
        if (isActive)
            transform.Rotate(rotation * rotationSpeed * Time.deltaTime);
    }

    public void SetActive(bool isActive)
    { this.isActive = isActive; }

}
