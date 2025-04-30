using System;
using UnityEngine;

public class ParticleBehavior : MonoBehaviour
{
 
    #region Private
    
     private Rigidbody2D _rigidbody;
     [SerializeField] private float _speed = 300f;
     [SerializeField] private float _lifeTime = 15f;
     
    #endregion
    
    #region Unity API
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void OnEnable()
    {
        Invoke(nameof(Deactivate), _lifeTime);
    }

    private void OnDisable()
    {
        CancelInvoke(nameof(Deactivate));
    }

    #endregion

    #region Main Methods

    private void Move()
    {
        //_rigidbody.linearVelocity = new Vector2(0, -_speed);
        _rigidbody.AddForce(gameObject.transform.up * (_speed));
    }
    
    #endregion

    #region Utils

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }

    #endregion


}
