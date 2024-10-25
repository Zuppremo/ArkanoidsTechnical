using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Paddle : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private InputActionReference actionReference;
    private Rigidbody rb;
    private float moveDirection;
    private InputAction action;

    private void Awake()
    {
        action = actionReference.action;
        rb = GetComponent<Rigidbody>();
        action.performed += OnActionPerformed;
        action.canceled += OnActionCanceled;
    }

    private void OnActionCanceled(InputAction.CallbackContext context)
    {
        moveDirection = 0;
    }

    private void OnEnable()
    {
        action.Enable();
    }

    private void FixedUpdate()
    {
        rb.velocity = (Vector3.right * moveDirection) * speed * Time.fixedDeltaTime;
    }

    private void OnDestroy()
    {
        action.performed -= OnActionPerformed;
        action.canceled -= OnActionCanceled;
    }

    private void OnActionPerformed(InputAction.CallbackContext context)
    {
        moveDirection = context.ReadValue<float>();
    }

    private void OnDisable()
    {
        action.Disable();
    }
}
