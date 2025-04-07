using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class TorpedoCameraRotation : MonoBehaviour
{
    public static TorpedoCameraRotation Instance;

    [SerializeField] float rotationSpeed = 5f;
    [SerializeField] Transform torpedoSpawn;
    [SerializeField] GameObject firedTorpedoPrefab;
    bool canMove = false;

    enum CurrentRotation { Left, Right, Up, Down }
    [SerializeField] CurrentRotation currentRotation = CurrentRotation.Left;

    private void Update()
    {
        if (!canMove) return;

        switch (currentRotation)
        {
            case CurrentRotation.Left:
                transform.Rotate(rotationSpeed * Time.deltaTime * Vector3.up);
                break;
            case CurrentRotation.Right:
                transform.Rotate(rotationSpeed * Time.deltaTime * Vector3.down);
                break;
            case CurrentRotation.Up:
                transform.Rotate(rotationSpeed * Time.deltaTime * Vector3.left);
                break;
            case CurrentRotation.Down:
                transform.Rotate(rotationSpeed * Time.deltaTime * Vector3.right);
                break;
            default:
                break;
        }
    }

    private void Awake()
    {
        //TorpedoLaunchCodes.OnTorpedoLaunched += TorpedoLaunchCodes_OnTorpedoLaunched;
        Instance = this;
    }

    //private void TorpedoLaunchCodes_OnTorpedoLaunched(object sender, System.EventArgs e)
    //{
    //    GameObject torpedo = Instantiate(firedTorpedoPrefab, torpedoSpawn);
    //}

    public void LaunchTorpedo()
    {
        GameObject torpedo = Instantiate(firedTorpedoPrefab, torpedoSpawn);
        torpedo.transform.SetParent(null);
    }

    public void TurnCameraLeft()
    {
        currentRotation = CurrentRotation.Left;
        canMove = true;
    }

    public void TurnCameraRight()
    {
        currentRotation = CurrentRotation.Right;
        canMove = true;
    }

    public void TurnCameraUp()
    {
        currentRotation = CurrentRotation.Up;
        canMove = true;
    }

    public void TurnCameraDown()
    {
        currentRotation = CurrentRotation.Down;
        canMove = true;
    }

    public void EndCameraMovement()
    {
        canMove = false;
    }
}
