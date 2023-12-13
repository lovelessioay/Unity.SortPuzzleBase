using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class BallController : MonoBehaviour
{
    private static BallController selected;
    public static BallController Selected { 
        get => selected;
        set => selected = value;
    }

    [SerializeField] private int colorID;
    [SerializeField, AllowNull] private PivotPoint point;

    [SerializeField] private Sprite[] coloredSprites;
    [SerializeField] private SpriteRenderer spriteRenderer;

    private Color[] colors = new Color[] { Color.red, Color.blue, Color.green, Color.gray };


    public int ColorID { 
        get => colorID;
        set
        {
            colorID = value;
            if (coloredSprites != null && coloredSprites.Length > colorID)
            {
                spriteRenderer.sprite = coloredSprites[colorID];
            }
            else
            {
                spriteRenderer.color = colors[colorID];
            }
        }
    }
    public PivotPoint Point { get => point; set => point = value; }

    private void OnValidate()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
}
