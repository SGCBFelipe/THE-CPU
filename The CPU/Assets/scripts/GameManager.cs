using NUnit.Framework;
using TMPro;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI message;

    public void AlertMessage(string text)
    {
        if (message == null)
            return;

        Animator _messageAnimator = message.GetComponent<Animator>();
        message.text = text;
        _messageAnimator.SetTrigger("Message");
    }

    public void Play()
    {
        SceneManager.LoadScene("maze");
    }

    public void Exit()
    {
        Application.Quit();
    }
}
