using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Febucci.UI;
using System.Threading;

public class ComputerReboot : MonoBehaviour
{
    [SerializeField] TMP_Text readoutText;
    [SerializeField] TypewriterByCharacter textTypewriter;

    private void OnEnable()
    {
        StartCoroutine(CO_RebootScreen());
    }

    public void RebootScreen()
    {
        StartCoroutine(CO_RebootScreen());
    }

    IEnumerator CO_RebootScreen()
    {
        textTypewriter.ShowText("REBOOTING DISPLAY \r\n. . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . .");
        textTypewriter.StartShowingText();
        yield return new WaitForSeconds(Random.Range(5, 10));
        textTypewriter.ShowText("REBOOTING SUCESSFUL");
        textTypewriter.StartShowingText();
        yield return new WaitForSeconds(2f);
        this.gameObject.SetActive(false);
    }

}
