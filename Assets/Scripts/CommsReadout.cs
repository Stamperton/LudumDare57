using Febucci.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CommsReadout : MonoBehaviour
{
    public static CommsReadout Instance { get; private set; }

    public static event EventHandler OnNewMessageRecieved;

    [SerializeField] TMPro.TMP_Text screenText;
    [SerializeField] TypewriterByCharacter typeWriter;
    [SerializeField] AudioSource[] audioSource;
    [SerializeField] AudioClip newMessageAlert;
    [SerializeField] AudioClip errorAlert;

    [Header("Game End")]
    [SerializeField] string gameEndMessage;
    [SerializeField] AudioClip gameEndAudio;

    int displayedMessageIndex;

    bool canControl = true;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        foreach (AudioSource source in audioSource)
        {
            source.loop = false;
        }
    }

    public void PlayEndGameMessage()
    {
        AddNewMessage(gameEndMessage, gameEndAudio);
    }

    public List<string> currentRecievedMessages = new List<string>();
    public List<AudioClip> currentMessageAudio = new List<AudioClip>();

    public void AddNewMessage(string newMessage, AudioClip messageTTS)
    {
        foreach (AudioSource source in audioSource)
        {
            source.Stop();
        }

        currentRecievedMessages.Add(newMessage);
        currentMessageAudio.Add(messageTTS);

        foreach (AudioSource source in audioSource)
        {
            source.clip = newMessageAlert;
            source.Play();
        }
        StartCoroutine(CO_QueueAudio());
    }

    public void DisplayMessage(int messageIndex, bool useAlert = false)
    {
        string displayString = currentRecievedMessages[messageIndex];
        typeWriter.ShowText(displayString);
        OnNewMessageRecieved?.Invoke(this, EventArgs.Empty);
        displayedMessageIndex = messageIndex;
    }

    public void NextMessage()
    {
        if (!canControl) return;
        if (displayedMessageIndex == (currentRecievedMessages.Count - 1))
        {
            foreach (AudioSource source in audioSource)
            {
                source.PlayOneShot(errorAlert);
            }
        }
        else
        {
            displayedMessageIndex++;
            displayedMessageIndex = Mathf.Clamp(displayedMessageIndex, 0, currentRecievedMessages.Count - 1);
            DisplayMessage(displayedMessageIndex);
        }
    }

    public void Button_PlayTTS()
    {
        if (SubmarineManager.Instance.IsGameOver()) return;

        if (currentMessageAudio.Count == 0)
        {
            foreach (AudioSource source in audioSource)
            {
                source.PlayOneShot(errorAlert);
            }
            return;
        }
        foreach (AudioSource source in audioSource)
        {
            source.Stop();
            source.clip = currentMessageAudio[displayedMessageIndex];
            source.Play();
        }
    }

    public void Button_StopTTS()
    {
        if (!canControl) return;
        foreach (AudioSource source in audioSource)
        {
            source.Stop();

        }
    }

    public void PreviousMessage()
    {
        if (!canControl) return;
        if (displayedMessageIndex == 0)
        {
            foreach (AudioSource source in audioSource)
            {
                source.PlayOneShot(errorAlert);
            }
        }
        else
        {
            displayedMessageIndex--;
            displayedMessageIndex = Mathf.Clamp(displayedMessageIndex, 0, currentRecievedMessages.Count - 1);
            DisplayMessage(displayedMessageIndex);
        }
    }

    IEnumerator CO_QueueAudio()
    {
        while (audioSource[0].isPlaying)
        {
            yield return null;
        }
        yield return new WaitForSeconds(1);

        if (SubmarineManager.Instance.GetCurrentLevel() == SubmarineManager.Level.Last && SubmarineManager.Instance.IsGameOver())
            foreach (AudioSource source in audioSource)
            {
                source.loop = true;
            }

        DisplayMessage(currentRecievedMessages.Count - 1, true);

        foreach (AudioSource source in audioSource)
        {
            source.clip = currentMessageAudio[currentMessageAudio.Count - 1];
            source.Play();
        }
    }
}
