using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnerScript : MonoBehaviour
{
    #region Private
    
    [SerializeField] private Transform _movementDirection;
    [SerializeField] private Vector2 _circleSize = new Vector2(0.6f, 0.6f);
    [SerializeField] private int _spawnNbr = 10;
    [SerializeField] private float _spawnInterval = 0.1f;
    private SpawnPool _spawnPool;
    private float _spawnTimer;

    #endregion
    
    #region Unity API
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _spawnPool = gameObject.GetComponent<SpawnPool>();
        _spawnTimer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        //Spawn();
    }

    private void FixedUpdate()
    {
        Spawn();
    }

    #endregion
    
    #region Main Methods
    
    private void Spawn()
    {
        _spawnTimer += Time.deltaTime;
        int activeInstances = _spawnPool.ActiveInstanceCount;
        if (_spawnTimer >= _spawnInterval && activeInstances <= _spawnNbr)
        {
            GameObject instance = _spawnPool.GetFirstAvailableInstance();
            // TODO: get range from circle center to radius
            var randomPos = Random.insideUnitCircle + gameObject.transform.position * _circleSize;
            instance.transform.position = randomPos;
            
            Vector2 direction = (Vector2)_movementDirection.transform.position - (Vector2)instance.transform.position;
            instance.transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);
            
            instance.SetActive(true);
            _spawnTimer = 0f;
        }
    }
    
    #endregion
    
}
