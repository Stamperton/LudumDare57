using Febucci.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AnimEvents : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] TypewriterByCharacter typeWriter;

    public void AnimEvent_PauseGame()
    {
        Time.timeScale = 0.00001f;
    }

    public void AnimEvent_ResumeGame()
    {
        Time.timeScale = 1f;
    }

    public void AnimEvent_StartText()
    {
        typeWriter.ShowText(SubmarineManager.Instance.cutSceneText);
    }

    public void AnimEvent_ClearText()
    {
        typeWriter.ShowText("");
    }

    public void SetNextAnimState()
    {
        animator.SetTrigger("Trigger");
    }

    public void AnimEvent_LoadNextScene()
    {
        switch (SubmarineManager.Instance.GetCurrentLevel())
        {
            case SubmarineManager.Level.First:
                SceneManager.LoadScene(2);
                break;
            case SubmarineManager.Level.Second:
                SceneManager.LoadScene(3);
                break;
            case SubmarineManager.Level.Last:
                SceneManager.LoadScene(0);
                break;
            default:
                break;
        }
    }

    public void AnimEvent_ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
