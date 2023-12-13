using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    private static GameController instance;
    public static GameController Instance { get => instance; }
    
    [SerializeField] private string nextLevel;
    [SerializeField] private ColbController[] colbs;

    private void Awake()
    {
        if (!instance)
            instance = this;

        foreach (ColbController controller in colbs)
        {
            controller.FillPivotPoints();
        }
    }

    private bool Validate()
    {
        foreach (ColbController colb in colbs)
        {
            if (!colb.ValidateColb()) 
                return false;
        }

        Debug.LogWarning("Validaion passed!");
        return true;
    }

    public void ValidateLevel()
    {
        if (Validate())
            SceneManager.LoadScene(nextLevel);
    }
}
