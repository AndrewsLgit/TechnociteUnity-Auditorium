using System;
using UnityEngine;
using UnityEngine.InputSystem;

public interface IAuditoriumCharacterController
{
    Vector2 MousePosition { get; }
    
    void SubToClickStartEvent(Action clickAction);
    void SubToClickEndEvent(Action clickAction);
    void UnsubFromClickStartEvent(Action clickAction);
    void UnsubFromClickEndEvent(Action clickAction);
}
public class AuditoriumCharacterController : MonoBehaviour, AuditoriumInputSystem.IMainActionsActions, IAuditoriumCharacterController
{
    
    #region Public

    public Vector2 MousePosition => _mousePosition;
    
    #endregion
    
    #region Private
    
    private AuditoriumInputSystem _auditoriumInputSystem;
    
    private Vector2 _mousePosition;

    private Action _onClickStartEvent;
    private Action _onClickEndEvent;
    
    #endregion
    
    #region Unity API
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _auditoriumInputSystem = new AuditoriumInputSystem();
        _auditoriumInputSystem.Enable();
        _auditoriumInputSystem.MainActions.SetCallbacks(this);
    }

    private void OnDisable()
    {
        _auditoriumInputSystem.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        _mousePosition = _auditoriumInputSystem.MainActions.Look.ReadValue<Vector2>();
    }
    
    #endregion

    #region Main Methods
    
    public void OnClick(InputAction.CallbackContext context)
    {
        if (context.started) _onClickStartEvent?.Invoke();
        if (context.canceled) _onClickEndEvent?.Invoke();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        _mousePosition = _auditoriumInputSystem.MainActions.Look.ReadValue<Vector2>();
    }
    
    public void SubToClickStartEvent(Action clickAction)
    {
        _onClickStartEvent += clickAction;
    }

    public void SubToClickEndEvent(Action clickAction)
    {
        _onClickEndEvent += clickAction;
    }

    public void UnsubFromClickStartEvent(Action clickAction)
    {
        _onClickStartEvent -= clickAction;
    }

    public void UnsubFromClickEndEvent(Action clickAction)
    {
        _onClickEndEvent -= clickAction;
    }

    #endregion

}
