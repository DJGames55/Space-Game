using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(menuName = "InputReader")]
public class InputReader : ScriptableObject, GameInput.IShipControlsActions, GameInput.IUIActions
{
    private GameInput _gameInput;

    private void OnEnable()
    {
        if(_gameInput == null)
        {
            _gameInput = new GameInput();
            _gameInput.Gameplay.SetCallbacks(this);
        }

        _gameInput.Gameplay.Enable();
    }

    public event Action<Vector3> MoveEvent;
    public void OnMove(InputAction.CallbackContext context)
    {
        MoveEvent?.Invoke(context.ReadValue<Vector3>());
    }

    public event Action<float> RollEvent;
    public void OnRoll(InputAction.CallbackContext context)
    {
        RollEvent?.Invoke(context.ReadValue<float>());
    }

    public event Action<Vector2> RotateEvent;
    public void OnRotate(InputAction.CallbackContext context)
    {
        RotateEvent?.Invoke(context.ReadValue<Vector2>());
    }

    public event Action PauseEvent;
    public void OnPause()
    {
        PauseEvent?.Invoke();
    }

    public event Action ResumeEvent;
    public void OnResume()
    {
        ResumeEvent?.Invoke();
    }
}
