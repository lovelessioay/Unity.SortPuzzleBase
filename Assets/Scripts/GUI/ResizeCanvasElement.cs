using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResizeCanvasElement : MonoBehaviour
{
    [SerializeField] private RectTransform self;
    [SerializeField] private Vector2 initialResolution;

    [ExecuteInEditMode]
    private void OnRectTransformDimensionsChange()
    {
        float resize = self.rect.width / initialResolution.x * initialResolution.y; 
        self.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, resize);

        Debug.Log("Call");
    }
}
