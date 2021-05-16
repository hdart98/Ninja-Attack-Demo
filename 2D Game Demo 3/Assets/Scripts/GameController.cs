using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField] private PlayerController playerCtrl;
    [SerializeField] private UIController uiCtrl;

    private GameObject[] Enemys;

    private void Update()
    {
        Enemys = GameObject.FindGameObjectsWithTag("Enemy");
        if (Enemys.Length == 0)
        {
            uiCtrl.SetActivePanel(true);
            uiCtrl.SetMessageText("YOU WIN !");
        }

        if (playerCtrl.IsDead() == true)
        {
            uiCtrl.SetActivePanel(true);
            uiCtrl.SetMessageText("YOU LOSE !");
        }
    }

    public void ExitToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
