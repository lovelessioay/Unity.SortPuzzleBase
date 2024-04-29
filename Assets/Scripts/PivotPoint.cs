using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

public class PivotPoint : MonoBehaviour
{
    [SerializeField] private BallController prefab;
    [SerializeField, AllowNull] private BallController ballController;
    [SerializeField] private bool debugMe = false;
    private static int colbs = 0;
    private static int[] colors;

    public static void Clear()
    {
        colbs = 0;
        colors = null;
    }

    public void FillPivotPoint()
    {
        if (colbs == 0)
        {
            colbs = FindObjectsOfType<ColbController>().Length - FindObjectsOfType<Cannon>().Length;
            colors = new int[colbs];
        }
        // Color ID diffusion
        int randomColorId = Random.Range(0, colbs);
        while (colors[randomColorId] >= 4)
        {
            randomColorId++;
            if (randomColorId >= colbs) randomColorId = 0;

            // Add loop check! Or maybe not, don't give af
        }
        if (ballController == null)
        {
            Ball = Instantiate(prefab, transform.position, Quaternion.identity);
        }
        ballController.ColorID = randomColorId;
        colors[randomColorId]++;
    }

    public BallController Ball
    {
        get => ballController;
        set
        {
            ballController = value;

            if (debugMe) Debug.LogWarning($"{name} - {ballController}");
            if (ballController)
                ballController.Point = this;
        }
    }

    private void Update()
    {
        if (ballController && Vector2.Distance(ballController.transform.position, transform.position) >= 0.001f)
        {
            ballController.transform.position = Vector3.Lerp(ballController.transform.position, transform.position, Time.deltaTime * 2f);
            // ballController.transform.position = transform.position;
        }
    }
}
