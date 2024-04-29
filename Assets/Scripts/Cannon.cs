using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;

public class Cannon : ColbController
{
    private static Cannon instance;
    public static Cannon Instance { get => instance; }

    public bool Availible { get => points[3].Ball == null; }

    private void Awake()
    {
        instance = this;
    }

    public override void FillPivotPoints()
    {
        return;
    }

    protected override void PutBallIn(BallController ball)
    {
        PivotPoint source = ball.Point;
        if (source && source.transform.parent == this.transform) return;        // If trying to put in itself
        uint i = 0;
        while (!points[i].Ball && i < points.Length - 1) i++;
        BallController _ball = points[i++].Ball;
        points[0].Ball = ball;

        for (uint j = i; j < points.Length; j++)
        {
            BallController c = points[j].Ball;
            points[j].Ball = _ball;
            _ball = c;
        }

        if (source.Ball == ball) source.Ball = null;
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

    public override void OnMouseDown()
    {
        if (!BallController.Selected)
            base.OnMouseDown();
    }
}
