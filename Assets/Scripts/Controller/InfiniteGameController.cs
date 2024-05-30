using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteGameController : MonoBehaviour
{
    [SerializeField] private Transform[] colbPivot1;
    [SerializeField] private Transform[] colbPivot2;
    [SerializeField] private Color[] randomColors; 
    [SerializeField] private ColbController colbControllerPrefab;
    [SerializeField] private int colbsMaxCount;

    private void PregenLevel()
    {
        int colbsCount = Random.Range(0, colbsMaxCount - 1) + 2;
        PivotPoint.Colbs = colbsCount;
        Transform[] colbsCountList = (colbsCount % 2 != 0) ? colbPivot2 : colbPivot1;

        int startPosition = colbsCount / 2 - ((colbsCount % 2 == 0) ? 0 : 1);
        Debug.Log(colbsCount);

        for (int i = 0; i < colbsCount; i++)
        {
            ColbController controller = Instantiate(colbControllerPrefab, colbsCountList[i].position, Quaternion.identity).GetComponent<ColbController>();
            GameController.Instance.AddColbController(controller);
            controller.FillPivotPoints();
        }
    }

    private void Start()
    {
        PregenLevel();

        Camera.main.backgroundColor = randomColors[Random.Range(0, randomColors.Length)];
    }
}
