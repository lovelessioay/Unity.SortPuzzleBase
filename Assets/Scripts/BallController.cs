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
    [SerializeField] private Colors colorTable;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private bool isPlaceHolder;

    private bool isHidden = false;

    public bool Hide
    {
        get => isHidden;
        set
        {
            isHidden = value;
            spriteRenderer.sprite = (value) ? colorTable.hidden : getSprite(colorID);
        }
    }

    public int ColorID { 
        get => colorID;
        set
        {
            colorID = value;
            spriteRenderer.sprite = getSprite(colorID);
        }
    }

    public void MakeHappy()
    {
        spriteRenderer.sprite = colorTable.coloredSpritesHappy[colorID];
    }

    private Sprite getSprite(int colorID)
    {
        return (isPlaceHolder) ? colorTable.placeholder : colorTable.coloredSprites[colorID];
    }

    public PivotPoint Point { 
        get => point;
        set
        {
            point = value;
            if (!point)
            {
                Hide = false;
            }
            else Hide = point.Masked;
        }
    }

    private void OnValidate()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Awake()
    {
        name = $"Ball{colorID}-{FindObjectsOfType<BallController>().Length}";
    }

    private void Start()
    {
        spriteRenderer.sprite = (isHidden) ? colorTable.hidden : getSprite(colorID);
    }
}
