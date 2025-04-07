using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchCodeEntryManager : MonoBehaviour
{
    public static LaunchCodeEntryManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    [SerializeField] string currentSetLaunchCode;

    [SerializeField] LaunchCodeComputer[] launchCodeComputers;

    private void Update()
    {
        string launchCodeUpdate = "";
        for (int i = 0; i < launchCodeComputers.Length; i++)
        {
            launchCodeUpdate += launchCodeComputers[i].GetNumber().ToString();
        }
        currentSetLaunchCode = launchCodeUpdate;
    }

    public string GetLaunchCode()
    {
        return currentSetLaunchCode;
    }
}
