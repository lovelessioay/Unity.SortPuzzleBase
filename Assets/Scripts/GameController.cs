using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    private static GameController instance;
    public static GameController Instance { get => instance; }
    
    [SerializeField] private string nextLevel;
    [SerializeField] private List<ColbController> colbs;
    [SerializeField, Range(0, 100)] private int moneyAmount;
    [SerializeField] private GameObject winScreen;

    public void AddColbController(ColbController colbController)
    {
        colbs.Add(colbController);
    }

    public string NextLevel { get => nextLevel; }

    private void Awake()
    {
        instance = this;
        
        if (!Cannon.Instance)
        {
            Debug.LogError($"Level {SceneManager.GetActiveScene().name} contains no Cannon manager object");
            return;
        }

        foreach (ColbController controller in colbs)
        {
            controller.FillPivotPoints();
        }
    }

    private bool Validate()
    {
        bool result = true;
        foreach (ColbController colb in colbs)
        {
            result &= colb.ValidateColb();
        }

        return result;
    }

    public IEnumerator SwitchScene(string nextLevel)
    {
        if (!PlayerPrefs.HasKey(SceneManager.GetActiveScene().name))
        {
            int money = (PlayerPrefs.HasKey("money")) ? PlayerPrefs.GetInt("money") : 0;
            PlayerPrefs.SetString(SceneManager.GetActiveScene().name, "pass");
            PlayerPrefs.SetInt("money", money + moneyAmount);
            PlayerPrefs.Save();
        }

        PivotPoint.Clear();
        BallController.Selected = null;
        instance = null;
        UnityEngine.AsyncOperation operation = SceneManager.LoadSceneAsync(nextLevel);
        operation.allowSceneActivation = false;
        while (operation.progress < 0.9f)
        {
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForSeconds(1f);
        operation.allowSceneActivation = true;
    }

    public void ValidateLevel()
    {
        if (Validate())
            winScreen.SetActive(true);
    }
}
