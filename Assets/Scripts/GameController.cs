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

    private void Start()
    {
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

    private IEnumerator SwitchScene()
    {
        PivotPoint.Clear();
        BallController.Selected = null;
        instance = null;
        AsyncOperation operation = SceneManager.LoadSceneAsync(nextLevel);

        while (!operation.isDone)
        {
            Debug.LogWarning(operation.progress);
            yield return new WaitForEndOfFrame();
        }
    }

    public void ValidateLevel()
    {
        if (Validate()) StartCoroutine(SwitchScene());            
    }
}
