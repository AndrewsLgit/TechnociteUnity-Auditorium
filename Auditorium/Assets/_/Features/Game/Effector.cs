using System;
using UnityEngine;

public class EffectorScript : MonoBehaviour
{
    #region Private

    private IAuditoriumInputController _inputController;
    private Ray _ray;
    private Camera _camera;

    #endregion
    
    #region Unity API
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _inputController = GetComponent<IAuditoriumInputController>();
        _camera = Camera.main;
        SubscribeToAllEvents();
    }

    private void OnDestroy()
    {
        UnSubscribeFromAllEvents();
    }

    /*private void OnDisable()
    {
        UnSubscribeFromAllEvents();
    }*/

    // Update is called once per frame
    void Update()
    {
        
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
        /*Debug.Log(LookMousePosition().ToString());*/
    }

    private void OnClickStart()
    {
        Debug.Log(LookMousePosition().ToString());
    }
    
    #endregion

    #region Utils

    private Vector3 GetMousePosition() => _inputController.MousePosition;

    private void UnSubscribeFromAllEvents()
    {
        _inputController.UnsubFromClickStartEvent(OnClickStart);
        _inputController.UnsubFromClickEndEvent(OnClickEnd);
    }
    
    private void SubscribeToAllEvents()
    {
        _inputController.SubToClickStartEvent(OnClickStart);
        _inputController.SubToClickEndEvent(OnClickEnd);
        
    }


    #endregion
    
}
