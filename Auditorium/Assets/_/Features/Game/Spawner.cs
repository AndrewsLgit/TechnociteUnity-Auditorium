using UnityEngine;

public class SpawnerScript : MonoBehaviour
{

    #region Public

    

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
        Spawn();
    }


    #endregion
    
    #region Utils
    
    private void Spawn()
    {
        _spawnTimer += Time.deltaTime;
        int activeInstances = _spawnPool.ActiveInstanceCount;
        if (_spawnTimer >= _spawnInterval && activeInstances <= _spawnNbr)
        {
            GameObject instance = _spawnPool.GetFirstAvailableInstance();
            // TODO: get range from circle center to radius
            var randomPos = Vector2.zero;
            instance.transform.position = randomPos;
            instance.SetActive(true);
            //instance.GetComponent<>() get instance behavior script
        }
    }
    
    #endregion
    
    #region Private
    
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private int _spawnNbr = 10;
    [SerializeField] private float _spawnInterval;
    private SpawnPool _spawnPool;
    private float _spawnTimer;

    #endregion
}
