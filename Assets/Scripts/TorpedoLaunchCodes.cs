using Febucci.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TorpedoLaunchCodes : MonoBehaviour
{
    //public static event EventHandler OnTorpedoLaunched;

    enum State { Ready, Generating }
    State currentState;
    [SerializeField] AudioSource thisAudioSource;

    [SerializeField] TMPro.TextMeshProUGUI codesReadoutText;
    [SerializeField] TypewriterByCharacter typewriter;
    [SerializeField] float timeBetweenCodes;
    [SerializeField] Image fillImage;

    float timer;

    [SerializeField] int currentLaunchCode;

    private void Awake()
    {
        thisAudioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        GetNewLaunchCode();
        currentState = State.Ready;
        timer = timeBetweenCodes;
    }

    void GetNewLaunchCode()
    {
        currentLaunchCode = UnityEngine.Random.Range(111, 999);
        codesReadoutText.text = "LAUNCH CODE: " + currentLaunchCode;
    }

    void RegenerateLaunchCode()
    {
        currentState = State.Generating;
        timer = 2f;
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        switch (currentState)
        {
            case State.Ready:
                fillImage.fillAmount = timer / timeBetweenCodes;
                if (timer <= 0)
                {
                    RegenerateLaunchCode();
                }
                break;
            case State.Generating:
                GetNewLaunchCode();
                if (timer <= 0)
                {
                    currentState = State.Ready;
                    timer = timeBetweenCodes;
                }
                break;
            default:
                break;
        }
    }

    public void FireTorpedo()
    {
        string incomingCode = LaunchCodeEntryManager.Instance.GetLaunchCode();

        if (!TorpedoLauncher.Instance.IsTorpedoLoaded())
        {
            thisAudioSource.Play();
            return;
        }

        if (incomingCode == currentLaunchCode.ToString())
        {
            //LAUNCH
            Debug.Log("PEW TORPEDO");
            //OnTorpedoLaunched?.Invoke(this, EventArgs.Empty);
            //REPLACED WITH INSTANCED CODES
            TorpedoLauncher.Instance.OnTorpedoLaunched();
            TorpedoCameraRotation.Instance.LaunchTorpedo();
            SubmarineManager.Instance.ScreenShake(1);


            RegenerateLaunchCode();
        }
        else
        {
            RegenerateLaunchCode();
            //Play Bad Audio
            thisAudioSource.Play();
        }
    }
}
