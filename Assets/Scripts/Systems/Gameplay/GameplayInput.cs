using System;
using UnityEngine;
using UnityEngine.InputSystem;

[Serializable]
public class GameplayInput 
{
    [SerializeField] private InputActionReference movementReference;
    [SerializeField] private InputActionReference launchReference;
    [SerializeField] private InputActionReference pauseReference;

    private InputAction movementAction;
    private InputAction launchAction;
    private InputAction pauseAction;
    private IPaddleForInput paddleInput;
    private IBallForInput ballInput;
    private IGameControllerForState gameController;

    public void Initialize(IPaddleForInput paddle, IBallForInput ball, IGameControllerForState gameController) 
    {
        movementAction = movementReference.action;
        launchAction = launchReference.action;
        pauseAction = pauseReference.action;
        pauseAction.performed += OnPausePerformed;
        movementAction.performed += OnMovementPerformed;
        launchAction.performed += OnLaunchPerformed;
        movementAction.canceled += OnMovementCanceled;
        paddleInput = paddle;
        ballInput = ball;
        this.gameController = gameController;
        pauseAction.Enable();
        launchAction.Enable();
        movementAction.Enable();
    }

    public void Destroy()
    {
        pauseAction.Disable();
        launchAction.Disable();
        movementAction.Disable();
        pauseAction.performed -= OnPausePerformed;
        movementAction.performed -= OnMovementPerformed;
        launchAction.performed -= OnLaunchPerformed;
        movementAction.canceled -= OnMovementCanceled;
    }

    private void OnPausePerformed(InputAction.CallbackContext context)
    {
        gameController.SetPause();
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
