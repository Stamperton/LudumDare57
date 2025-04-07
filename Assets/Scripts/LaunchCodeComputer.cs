using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchCodeComputer : MonoBehaviour
{
    [SerializeField] TMPro.TMP_Text screenReadout;
    int thisEntry = 0;

    public int GetNumber() => thisEntry;

    private void Start()
    {
        thisEntry = 0;
        screenReadout.text = thisEntry.ToString();
    }

    public void NumberUp()
    {
        thisEntry++;
        if (thisEntry > 9)
            thisEntry = 0;

        screenReadout.text = thisEntry.ToString();
    }

    public void NumberDown()
    {
        thisEntry--;
        if (thisEntry < 0)
            thisEntry = 9;

        screenReadout.text = thisEntry.ToString();
    }
}
