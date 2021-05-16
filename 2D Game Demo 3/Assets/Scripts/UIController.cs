using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] GameObject winOrDefeat;
    [SerializeField] Text messageText;

    public void SetMessageText(string txt)
    {
        messageText.text = txt;
    }

    public void SetActivePanel(bool istf)
    {
        winOrDefeat.SetActive(istf);
    }
}
