using NUnit.Framework;
using TMPro;
using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI message;

    public void AlertMessage(string text)
    {
        Animator _messageAnimator = message.GetComponent<Animator>();
        message.text = text;
        _messageAnimator.SetTrigger("Message");
    }
}
