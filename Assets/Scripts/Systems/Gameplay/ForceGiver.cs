using System;
using UnityEngine;

public class ForceGiver : MonoBehaviour
{
    [SerializeField] private Direction direction;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.GetComponent<Ball>() != null)
        {
            Debug.Log("enter");
            switch (direction)
            {
                case Direction.Left:
                    Debug.Log("Left");
                    collision.collider.GetComponent<Ball>().GiveLeftForceToBall();
                    break;
                case Direction.Right:
                    Debug.Log("Right");
                    collision.collider.GetComponent<Ball>().GiveRightForceToBall();
                    break;
                case Direction.Up:
                    Debug.Log("Up");
                    collision.collider.GetComponent<Ball>().GiveUpForceToBall();
                    break;
            }
        }
    }
}

public enum Direction
{
    Left,
    Right,
    Up,
    Down,
}