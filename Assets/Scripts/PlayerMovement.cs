using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float forceMagnitude;
    [SerializeField] private float maxVelocity;
    [SerializeField] private float rotationSpeed;


    private Camera mainCamera;
    private Rigidbody rigidbodyOfThePlane;

    private Vector3 movementDirection;


    void Start()
    {
        rigidbodyOfThePlane = GetComponent<Rigidbody>();
        mainCamera = Camera.main;
    }

    
    void Update()
    {
        ProcessInput();
        KeepPlayerOnScreen();
        RotateToFaceVelocity();
    }

    private void FixedUpdate()
    {
        if (movementDirection == Vector3.zero) { return; }

        rigidbodyOfThePlane.AddForce(movementDirection*forceMagnitude, ForceMode.Force);
        rigidbodyOfThePlane.velocity = Vector3.ClampMagnitude(rigidbodyOfThePlane.velocity, maxVelocity);

    }


    private void ProcessInput()
    {
        if (Touchscreen.current.primaryTouch.press.IsPressed())
        {
            Vector2 screen_pos = Touchscreen.current.primaryTouch.position.ReadValue();
            Vector3 world_pos = mainCamera.ScreenToWorldPoint(screen_pos);

            movementDirection = world_pos - transform.position;
            movementDirection.z = 0f;
            movementDirection.Normalize();
        }
        else
        {
            movementDirection = Vector3.zero;
        }
    }

    private void KeepPlayerOnScreen()
    {
        Vector3 newPosition = transform.position;
        Vector3 viewportPosition =  mainCamera.WorldToViewportPoint(transform.position);

        if(viewportPosition.x > 1)
        {
            newPosition.x = -newPosition.x + 0.1f;
        }
        else if (viewportPosition.x < 0)
        {
            newPosition.x = -newPosition.x - 0.1f;
        }
        if (viewportPosition.y > 1)
        {
            newPosition.y = -newPosition.y + 0.1f;
        }
        else if (viewportPosition.y < 0)
        {
            newPosition.y = -newPosition.y - 0.1f;
        }

        transform.position = newPosition;
    }


    private void RotateToFaceVelocity()
    {
        if (rigidbodyOfThePlane.velocity == Vector3.zero)
        {
            return;
        }

        Quaternion targetRotation = Quaternion.LookRotation(rigidbodyOfThePlane.velocity, Vector3.back);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
}
