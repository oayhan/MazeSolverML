using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeAcademy : Academy
{
    public int finishLinePos;

    public override void AcademyReset()
    {
        Debug.Log("Finish line param-reset:" + resetParameters["finish_line"]);

        finishLinePos = Mathf.RoundToInt(resetParameters["finish_line"]);
    }

    public override void AcademyStep()
    {
        Debug.Log("Finish line param-step:" + resetParameters["finish_line"]);

        finishLinePos = Mathf.RoundToInt(resetParameters["finish_line"]);
    }
}
