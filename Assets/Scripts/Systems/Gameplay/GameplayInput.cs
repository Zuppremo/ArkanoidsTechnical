using System;
using UnityEngine;
using UnityEngine.InputSystem;

[Serializable]
public class GameplayInput 
{
    [SerializeField] private InputActionReference movementReference;
    [SerializeField] private InputActionReference launchReference;

    private InputAction movementAction;
    private InputAction launchAction;
    private IPaddleForInput paddleInput;
    private IBallForInput ballInput;

    public void Initialize(IPaddleForInput paddle, IBallForInput ball)
    {
        movementAction = movementReference.action;
        launchAction = launchReference.action;
        movementAction.performed += OnMovementPerformed;
        launchAction.performed += OnLaunchPerformed;
        movementAction.canceled += OnMovementCanceled;
        paddleInput = paddle;
        ballInput = ball;
        launchAction.Enable();
        movementAction.Enable();
    }

    public void Destroy()
    {
        launchAction.Disable();
        movementAction.Disable();
        movementAction.performed -= OnMovementPerformed;
        launchAction.performed -= OnLaunchPerformed;
        movementAction.canceled -= OnMovementCanceled;
    }

    private void OnLaunchPerformed(InputAction.CallbackContext context)
    {
        ballInput.Launch();
    }
    
    private void OnMovementPerformed(InputAction.CallbackContext context)
    {
        paddleInput.SetInput(context.ReadValue<float>());
    }

    private void OnMovementCanceled(InputAction.CallbackContext context)
    {
        paddleInput.SetInput(0);
    }
}
