using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float leftLimit;
    [SerializeField] private float rightLimit;
    private Paddle paddle;
    private GameController gameController;
    private void Awake()
    {
        paddle = FindObjectOfType<Paddle>();
    }
    void Update()
    {
        if(!Keyboard.current.aKey.isPressed && !Keyboard.current.dKey.isPressed)
            paddle.FreezePaddle();
        if (Keyboard.current.aKey.isPressed)
            if (paddle.transform.position.x < leftLimit)
                return;
            else
                paddle.MoveLeft();
        else if (Keyboard.current.dKey.isPressed)
            if (paddle.transform.position.x > rightLimit)
                return;
            else
                paddle.MoveRight();
    }
}
