using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ColbController : MonoBehaviour
{
    [SerializeField] private PivotPoint[] points = new PivotPoint[4];
    private bool validated = false;
    public void FillPivotPoints()
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

    private void PutBallIn(BallController ball)
    {
        // Todo
        PivotPoint source = ball.Point;
        if (source.transform.parent == this.transform) return;
        BallController _ball = points[0].Ball;
        points[0].Ball = ball;
       
        for (int i = 1; i < points.Length; i++)
        {
            BallController c = points[i].Ball;
            points[i].Ball = _ball;
            Debug.Log(_ball);
            _ball = c;
        }
        source.Ball = _ball;
    }

    public void ToggleCollider(bool state)
    {
        GetComponent<Collider>().enabled = state;
    }

    public void OnMouseDown()
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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            BallController.Selected = null;
        }
    }
}
