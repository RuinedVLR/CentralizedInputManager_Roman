using System;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Central entry point for all player input.
/// Receives messages from the Input System (PlayerInput component)
/// and exposes them as C# events for downstream systems (e.g. PlayerController) to consume.
/// </summary>
[RequireComponent(typeof(PlayerInput))]
public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }

    public event Action<Vector2> MoveEvent;
    public event Action<bool> JumpEvent;
    public event Action PauseEvent;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void OnMove(InputValue value)
    {
        MoveEvent?.Invoke(value.Get<Vector2>());
    }

    public void OnJump(InputValue value)
    {
        JumpEvent?.Invoke(value.isPressed);
    }

    public void OnPause(InputValue value)
    {
        if (value.isPressed)
        {
            PauseEvent?.Invoke();
        }
    }
}
