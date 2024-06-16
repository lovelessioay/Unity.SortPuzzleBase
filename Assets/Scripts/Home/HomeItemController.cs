using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HomeItemController : MonoBehaviour
{
    [SerializeField] private HomeItem item;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Canvas canvas;
    [SerializeField] private TMP_Text label;

    private void Start()
    {
        // Load serialized data
        if (item.Key != "" && PlayerPrefs.HasKey(item.Key))
        {
            string serializedData = PlayerPrefs.GetString(item.Key);

            item = HomeItem.Deserialize(serializedData);
        }
        else
        {
            Debug.LogError($"There's no Item key on {name}");
            PlayerPrefs.SetString(item.Key, item.Serialize());
        }

        spriteRenderer.enabled = item.Unlocked;
        canvas.enabled = !item.Unlocked && HomeMgr.Instance.Money >= item.Cost;
        label.text = $"{item.Cost}";
    }

    public void Buy()
    {
        if (HomeMgr.Instance.Money >= item.Cost)
        {
            HomeMgr.Instance.Money -= item.Cost;

            item.Unlocked = true;
            canvas.enabled = false;
            spriteRenderer.enabled = true;
        }             
    }

    private void OnDestroy()
    {
        string serializedData = item.Serialize();
        PlayerPrefs.SetString(item.Key, serializedData);
        PlayerPrefs.Save();
    }
}
