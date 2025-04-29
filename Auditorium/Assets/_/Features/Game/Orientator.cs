using System;
using UnityEngine;
using Input = UnityEngine.Windows.Input;

public class OrientatorScript : MonoBehaviour
{
    #region Private

    private IAuditoriumCharacterController _characterController;
    private Ray _ray;
    private Camera _camera;

    #endregion
    
    #region Unity API
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _characterController = GetComponent<IAuditoriumCharacterController>();
        _camera = Camera.main;
        
    }

    private void OnDestroy()
    {
        UnSubscribeFromAllEvents();
    }

    private void OnDisable()
    {
        UnSubscribeFromAllEvents();
    }

    // Update is called once per frame
    void Update()
    {
        var mousePosInScreen = _characterController.MousePosition;
    }
    
    #endregion
    
    #region Main Methods

    private Vector3 LookMousePosition()
    {
        var pos = GetMousePosition();
        return _camera.ScreenToWorldPoint(new Vector3(pos.x, pos.y, _camera.transform.position.z));
    }
    
    private void OnClickEnd()
    {
        throw new NotImplementedException();
    }

    private void OnClickStart()
    {
        throw new NotImplementedException();
    }
    
    #endregion

    #region Utils

    private Vector3 GetMousePosition() => _characterController.MousePosition;

    private void UnSubscribeFromAllEvents()
    {
        _characterController.UnsubFromClickStartEvent(OnClickStart);
        _characterController.UnsubFromClickEndEvent(OnClickEnd);
    }
    
    private void SubscribeToAllEvents()
    {
        _characterController.SubToClickStartEvent(OnClickStart);
        _characterController.SubToClickEndEvent(OnClickEnd);
        
    }


    #endregion
    
}
