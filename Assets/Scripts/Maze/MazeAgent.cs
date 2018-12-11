using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CharacterController))]
public class MazeAgent : Agent
{
    [SerializeField]
    private float raycastDistance = 10;

    [SerializeField]
    private LayerMask raycastLayerMask;

    [SerializeField]
    private Maze maze;

    private Vector3 position;
    private Vector3 startPosition = new Vector3(0, 0.15f, -22);
    private const float MovementSpeed = 5;
    private const float RotationSpeed = 2;

    private float leftDistance;
    private float forwardDistance;
    private float rightDistance;
    private float averageDistance;

    private CharacterController characterController;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    public override List<float> CollectState()
    {
        List<float> state = new List<float>();

        state.Add(leftDistance);
        state.Add(forwardDistance);
        state.Add(rightDistance);

        return state;
    }

    public override void AgentStep(float[] act)
    {
        reward = -0.001f;

        float rotationAmount = Mathf.Clamp(act[0], -1, 1);

        transform.Rotate(0, rotationAmount * RotationSpeed, 0);

        characterController.SimpleMove(transform.forward * MovementSpeed);

        CalculateDistances();

        if (forwardDistance > leftDistance && forwardDistance > rightDistance)
        {
            reward += 0.1f;

            reward += Mathf.Max(0.1f - Mathf.Abs(leftDistance - rightDistance) * 0.2f, 0);

        }
    }

    private void CalculateDistances()
    {
        RaycastHit hitInfo;

        //forward distance
        if (Physics.Raycast(transform.position, transform.forward, out hitInfo, raycastDistance, raycastLayerMask))
        {
            forwardDistance = hitInfo.distance;
        }
        else
        {
            forwardDistance = raycastDistance;
        }

        //right distance
        if (Physics.Raycast(transform.position, transform.right, out hitInfo, raycastDistance, raycastLayerMask))
        {
            rightDistance = hitInfo.distance;
        }
        else
        {
            rightDistance = raycastDistance;
        }

        //left distance
        if (Physics.Raycast(transform.position, -transform.right, out hitInfo, raycastDistance, raycastLayerMask))
        {
            leftDistance = hitInfo.distance;
        }
        else
        {
            leftDistance = raycastDistance;
        }
    }

    public override void AgentReset()
    {
        position = startPosition;

        maze.ResetMaze();

        transform.position = position;
        transform.rotation = Quaternion.identity;

        CalculateDistances();
    }

    public override void AgentOnDone()
    {
        
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.collider.CompareTag("wall"))
            MazeFailed();
    }

    public void MazeFailed()
    {
        reward = -1f;
        done = true;
    }

    public void MazeSuccesful()
    {
        reward = 1;
        done = true;

        Debug.Log("Reward:" + CumulativeReward);
    }
}
