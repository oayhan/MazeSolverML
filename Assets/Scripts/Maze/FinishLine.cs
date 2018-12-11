using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLine : MonoBehaviour
{

    private void OnTriggerEnter(Collider otherCollider)
    {
        if (otherCollider.CompareTag("agent"))
        {
            MazeAgent agent = otherCollider.GetComponent<MazeAgent>();
            agent.MazeSuccesful();
        }
    }
}
