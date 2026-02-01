using System;
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
            _gameInput.ShipControls.SetCallbacks(this);
            _gameInput.UI.SetCallbacks(this);
        }

        SetShipControls();
    }

    private void OnDisable()
    {
        _gameInput.ShipControls.Disable();
    }

    public void SetShipControls()
    {
        _gameInput.ShipControls.Enable();
        _gameInput.UI.Disable();
    }

    public void SetUI()
    {
        _gameInput.UI.Enable();
        _gameInput.ShipControls.Disable();
    }

    public void DisableControls()
    {
        _gameInput.ShipControls.Disable();
        _gameInput.UI.Disable();
    }

    // Ship Controls
    #region Ship Controls

    public event Action DebugEvent;
    public void OnDebug(InputAction.CallbackContext context)
    {
        DebugEvent?.Invoke();
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
    public void OnPause(InputAction.CallbackContext context)
    {
        if (context.started)
            PauseEvent?.Invoke();
    }

    public event Action OpenQTEvent;
    public void OnOpenQT(InputAction.CallbackContext context)
    {
        if (context.started)
            OpenQTEvent?.Invoke();
    }

    public event Action OpenWeaponsEvent;
    public void OnOpenWeapons(InputAction.CallbackContext context)
    {
        OpenWeaponsEvent?.Invoke();
    }

    // Weapons
    #region Weapons
    public event Action WSlotSelect1Event;
    public void OnWSlotSelect1(InputAction.CallbackContext context)
    {
        WSlotSelect1Event?.Invoke();
    }

    public event Action WSlotSelect2Event;
    public void OnWSlotSelect2(InputAction.CallbackContext context)
    {
        WSlotSelect2Event?.Invoke();
    }

    public event Action WSlotSelect3Event;
    public void OnWSlotSelect3(InputAction.CallbackContext context)
    {
        WSlotSelect3Event?.Invoke();
    }
    #endregion
    #endregion

    // UI
    #region UI

    public event Action UICloseEvent;
    public void OnClose(InputAction.CallbackContext context)
    {
        if (context.started)
            UICloseEvent?.Invoke();
    }

    public event Action CancelQTEvent;
    public void OnCancelQT(InputAction.CallbackContext context)
    {
        if (context.started)
            CancelQTEvent?.Invoke();
    }

    #endregion
}
