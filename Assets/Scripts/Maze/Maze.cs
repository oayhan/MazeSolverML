using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maze : MonoBehaviour
{
    [SerializeField]
    private GameObject academy;

    [SerializeField]
    private Transform[] finishPoints;

    [SerializeField]
    private Transform finishLine;

    private MazeAcademy mazeAcademy;

    private void Awake()
    {
        if (academy == null)
            academy = GameObject.Find("Academy");

        if (academy != null)
            mazeAcademy = academy.GetComponent<MazeAcademy>();
    }

    public void ResetMaze()
    {
        finishLine.position = finishPoints[mazeAcademy.finishLinePos].position;
        finishLine.rotation = finishPoints[mazeAcademy.finishLinePos].rotation;
    }
}
