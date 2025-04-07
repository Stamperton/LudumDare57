using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class LaunchedTorpedo : MonoBehaviour
{
    [SerializeField] float torpedoSpeed = 15;

    // Update is called once per frame
    void Update()
    {
        //this.transform.Translate(transform.position + -transform.forward * torpedoSpeed * Time.deltaTime);

        transform.position = transform.position + transform.forward * torpedoSpeed * Time.deltaTime;
    }


}
