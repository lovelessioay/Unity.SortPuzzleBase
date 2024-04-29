using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
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
    [SerializeField] private Sprite[] coloredSpritesHappy;
    [SerializeField] private SpriteRenderer spriteRenderer;

    public int ColorID { 
        get => colorID;
        set
        {
            colorID = value;
            spriteRenderer.sprite = coloredSprites[colorID];
        }
    }

    public void MakeHappy()
    {
        spriteRenderer.sprite = coloredSpritesHappy[colorID];
    }

    public PivotPoint Point { get => point; set => point = value; }

    private void OnValidate()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Awake()
    {
        name = $"Ball{colorID}-{FindObjectsOfType<BallController>().Length}";
    }
}
