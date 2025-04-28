using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

public class SpawnPool : MonoBehaviour
{
    #region Private
    
    [FormerlySerializedAs("_projectilePrefab")] [SerializeField] private GameObject _poolPrefab;
    [FormerlySerializedAs("_projectilePoolSize")] [SerializeField] private int _poolSize;

    // List containing all instances of our prefab.
    private List<GameObject> _instanceList = new List<GameObject>();

    #endregion
    
    #region Main Methods
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Instantiate all the projectiles in our pool
        // set active = false, we will activate our projectiles on use
        for (int i = 0; i < _poolSize; i++)
        {
            var instance = Instantiate(_poolPrefab, transform);
            instance.SetActive(false);
            _instanceList.Add(instance);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #endregion
    
    #region Utils
    
    // Return one inactive instance for use (for example when shooting, we set our projectile to active
    public GameObject GetFirstAvailableInstance()
    {
        foreach (var instance in _instanceList)
        {
            if (instance.activeSelf == false)
            {
                return instance;
            }
        }
        // if there are no inactive instances (all instances are in use) then we create a new instance and add it to our list
        var newInstance = Instantiate(_poolPrefab, transform);
        newInstance.SetActive(false);
        _instanceList.Add(newInstance);
        return newInstance;
    }
    
    // Useful to activate a max number of instances, for example, my AsteroidSpawner spawns(activates) asteroids as long as there's less than 5 asteroids active.
    public int ActiveInstanceCount => _instanceList.Count(x => x.activeSelf);
    
    #endregion
}
