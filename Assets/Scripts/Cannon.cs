using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;

public class Cannon : ColbController
{
    [SerializeField] private LayerMask m_LayerMask;

    private static Cannon instance;
    public static Cannon Instance { get => instance; }

    public bool Availible { get => points[points.Length - 1].Ball == null; }

    private void Awake()
    {
        instance = this;
    }

    public override void PutBallIn(BallController ball)
    {
        PivotPoint source = ball.Point;
        if (source && source.transform.parent == this.transform) return;        // If trying to put in itself
        if (points[0].Ball) return;

        BallController _ball = points[0].Ball;

        if (_ball)
        {
            ball.Point.Ball = _ball;
        }
        points[0].Ball = ball;
    }

    public void UpdateStack()
    {
        uint distance = 0;
        while (points[distance++].Ball == null && distance < 4) ;

        for (uint i = 0; i < points.Length - distance; i++) 
        {
            points[i + distance].Ball = points[i].Ball;
            points[i + distance].Ball = null;
        }
    }

    public void OnMouseUp()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 10f, m_LayerMask);
        if (hit)
        {
            ColbController controller = hit.collider.transform.GetComponent<ColbController>();
            if (controller)
            {
                if (controller.ValidateColb())
                    return;

                Debug.Log($"Hit {controller}");
                BallController.Selected = points[0].Ball;
                controller.PutBallIn(points[0].Ball);

                GameController.Instance.ValidateLevel();
            }
        }
    }

    public void OnMouseDrag()
    {
        float delta = Mathf.Clamp(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, -10, 10);
        transform.position = new Vector3(delta, transform.position.y, transform.position.z);
    }
}
