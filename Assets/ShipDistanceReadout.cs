using Febucci.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ShipDistanceReadout : MonoBehaviour
{
    [SerializeField] TMPro.TextMeshProUGUI distanceText;
    [SerializeField] TypewriterByCharacter typeWriter;
    [SerializeField] UnityEngine.UI.Image updateImage;
    ShipControls shipControls;

    public float timeBetweenUpdates = 10f;
    float timer;

    private void Start()
    {
        shipControls = GetComponentInParent<ShipControls>();
        UpdateParameters();
    }


    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0)
            UpdateParameters();

        updateImage.fillAmount = timer / timeBetweenUpdates;
    }

    public void UpdateParameters()
    {
        timer = timeBetweenUpdates;
        float distance = DollyCartSpeed.Instance.GetPathLength() - DollyCartSpeed.Instance.GetPathPosition();
        string distanceString = "DISTANCE TO TARGET: " + distance.ToString("####");

        string travelledString = "DISTANCE TRAVELLED: " + DollyCartSpeed.Instance.GetPathPosition().ToString("####");

        float progressPercent = (DollyCartSpeed.Instance.GetPathPosition() / DollyCartSpeed.Instance.GetPathLength());
        string progressString = "PROGRESS: " + (progressPercent * 100).ToString("###") + "%";

        float timeToTarget = distance / shipControls.GetCurrentSpeed();
        string currentSpeed = "CURRENT SPEED: " + shipControls.GetCurrentSpeed();
        string remainingTimeString = "TIME TO TARGET: " + timeToTarget.ToString("####");
        string estimateString = "                  (Seconds, Estimated)";

        string fullText =
                            distanceString + "\n" +
                travelledString + "\n" +
                progressString + "\n" + "\n" +
                currentSpeed + "\n" +
                remainingTimeString + "\n" 
                + estimateString;

        typeWriter.ShowText(fullText);
    }
}
