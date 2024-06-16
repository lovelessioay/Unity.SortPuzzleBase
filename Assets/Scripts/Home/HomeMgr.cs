using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeMgr : MonoBehaviour, ISingleton
{
    private static HomeMgr instance;
    public static HomeMgr Instance { get => instance; }

    [SerializeField] private TMP_Text moneyLabel;
    private int money = 0;

    public int Money {
        get => money;
        set
        {
            money = value;
            moneyLabel.text = $"{money}";
        }
    }

    public void Awake()
    {
        if (PlayerPrefs.HasKey("money"))
            Money = PlayerPrefs.GetInt("money");
        instance = this;
    }

    public void OnDestroy()
    {
        if (PlayerPrefs.HasKey("money"))
        {
            PlayerPrefs.SetInt("money", money);
            PlayerPrefs.Save();
        }
        instance = null;
    }

    public void ChangeLevel(string name)
    {
        SceneManager.LoadScene(name);
    }
}
