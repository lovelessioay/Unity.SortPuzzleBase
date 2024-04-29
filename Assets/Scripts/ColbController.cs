using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ColbController : MonoBehaviour
{
    [SerializeField] protected PivotPoint[] points = new PivotPoint[4];
    protected bool validated = false;

    public virtual void FillPivotPoints()
    {
        foreach (PivotPoint point in points)
        {
            point.FillPivotPoint();
        }
    }

    public bool ValidateColb()
    {
        Debug.Log($"Started validation of {gameObject.name} with {points[0].Ball}");
        if (!points[0].Ball)
            return false;
        int colorType = points[0].Ball.ColorID;
        foreach (PivotPoint point in points)
        {
            if (!point.Ball || point.Ball.ColorID != colorType)
                return false;
        }

        foreach (PivotPoint point in points)
        {
            point.Ball.MakeHappy();
        }
        Debug.Log($"Colb validated! {name}");
        validated = true;
        return true;
    }

    protected virtual void PutBallIn(BallController ball)
    {
        if (Cannon.Instance.Availible || ball.Point.transform.parent == Cannon.Instance.transform)
        {
            // Todo
            PivotPoint source = ball.Point;
            if (source && source.transform.parent == this.transform) return;
            uint i = 0;
            while (!points[i].Ball && i < points.Length - 1) i++;
            BallController _ball = points[i++].Ball;
            points[0].Ball = ball;
            source.Ball = null;
            if (i == 1)
            {
                for (uint j = i; j < points.Length; j++)
                {
                    BallController c = points[j].Ball;
                    points[j].Ball = _ball;
                    _ball = c;
                }

                Cannon cannon = Cannon.Instance;
                source.Ball = null;
                cannon.PutBallIn(_ball);
            }
        }
    }

    public void ToggleCollider(bool state)
    {
        GetComponent<Collider>().enabled = state;
    }

    public virtual void OnMouseDown()
    {
        if (validated) return;
        if (!GameController.Instance)
        {
            Debug.LogWarning("There's no GameController on scene!");
            return;
        }

        if (BallController.Selected != null)
        {
            PutBallIn(BallController.Selected);
            BallController.Selected = null;

            GameController.Instance.ValidateLevel();
        }
        else
        {
            BallController.Selected = points[0].Ball;
        }
    }

    protected void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            BallController.Selected = null;
        }
    }
}
