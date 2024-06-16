using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinScreenController : MonoBehaviour
{
    private GameController gameController;

    [SerializeField] private string mainMenu;

    private void Start()
    {
        gameController = GameController.Instance;
    }

    public void LevelChange()
    {
        StartCoroutine(gameController.SwitchScene(gameController.NextLevel));
    }

    public void toMainMenu()
    {
        StartCoroutine(gameController.SwitchScene(mainMenu));
    }
}
