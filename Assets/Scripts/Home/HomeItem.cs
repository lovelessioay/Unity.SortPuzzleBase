using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class HomeItem
{
    [SerializeField] private string key;
    [SerializeField] private bool unlocked;
    [SerializeField, Range(0f, 100f)] private int cost;

    public string Key { get => key; }
    public bool Unlocked { get => unlocked; set => unlocked = value; }
    public int Cost { get => cost; }

    HomeItem(string key = "", bool unlocked = false, int cost = 0) 
    { 
        this.key = key;
        this.unlocked = unlocked;
        this.cost = cost;
    }

    public string Serialize()
    {
        return $"{key} {unlocked} {cost}";
    }

    public static HomeItem Deserialize(string serializedData)
    {
        string[] data = serializedData.Split(' ');
        Debug.Log(serializedData);
        return new HomeItem(data[0], Boolean.Parse(data[1]), int.Parse(data[2]));
    }
}
