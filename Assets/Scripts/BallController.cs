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

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Collider2D col;

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

    public PivotPoint Point { get => point; set
        {
            point = value;

            rb.bodyType = (point == null) ? RigidbodyType2D.Kinematic : RigidbodyType2D.Dynamic;
            col.enabled = (point);
        }
    }

    private void OnValidate()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
}
