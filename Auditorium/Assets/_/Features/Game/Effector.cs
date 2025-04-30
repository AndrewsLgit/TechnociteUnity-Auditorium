using System;
using UnityEngine;

public class EffectorScript : MonoBehaviour
{
    #region Private

    private IAuditoriumInputController _inputController;
    private Ray _ray;
    private Camera _camera;
    private bool _mouseOverBorder = false;
    private bool _isDragging = false;
    private Vector3 _previousScale;
    [SerializeField] private float _minScale = 0.4f;
    [SerializeField] private float _maxScale = 1.2f;

    #endregion
    
    #region Unity API
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _inputController = GetComponent<IAuditoriumInputController>();
        _camera = Camera.main;
        SubscribeToAllEvents();
        _previousScale = transform.localScale;
        _maxScale = transform.localScale.x * 2;
        _minScale = transform.localScale.x/3;
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

    private void OnMouseOver()
    {
        float newRadius = Vector2.Distance(transform.position, GetMouseWorldPoint());
        
            if (newRadius >= _maxScale || newRadius <= _minScale) _mouseOverBorder = false;
            
            else
            {
                _mouseOverBorder = true;
                var newScale = new Vector3(_previousScale.x + 0.1f, _previousScale.y + 0.1f, 0);
                transform.localScale = (newScale.x >= _maxScale) || (newScale.x <= _minScale) ? newScale : _previousScale;
                //transform.localScale = new Vector3(_previousScale.x + 0.1f, _previousScale.y + 0.1f, 0);
            }
            
            //Debug.Log(_mouseOverBorder);
    }

    private void OnMouseDrag()
    {
        if (_mouseOverBorder)
        {
            float newRadius = Vector2.Distance(transform.position, GetMouseWorldPoint());
            float clampedScale = Mathf.Clamp(newRadius * 2f, _minScale, _maxScale);
            
            transform.localScale = new Vector3(clampedScale, clampedScale, 1f);
            _previousScale = transform.localScale;
            
        }
        else transform.position = GetMouseWorldPoint();
    }

    #endregion
    
    #region Main Methods
    
    private void OnClickEnd()
    {
        /*Debug.Log(LookMousePosition().ToString());*/
    }

    private void OnClickStart()
    {
        
        //Debug.Log(GetMouseWorldPoint().ToString());
    }
    
    #endregion

    #region Utils

    private Vector2 GetMousePosition2D() => _inputController.MousePosition;
    
    private Vector2 GetMouseWorldPoint() => _camera.ScreenToWorldPoint(GetMousePosition2D());
    /*{
        var pos = (Vector2)GetMousePosition();
        return _camera.ScreenToWorldPoint(pos);
    }*/

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
