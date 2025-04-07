using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugMissionTimer : MonoBehaviour
{
    [SerializeField] TMPro.TMP_Text debugText;

    float timer;
    private void Update()
    {
        timer += Time.deltaTime;
        debugText.text = "Timer: " + timer.ToString("##.##.##");
    }
}
