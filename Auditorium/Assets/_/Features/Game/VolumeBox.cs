using System;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class VolumeBoxScript : MonoBehaviour
{

    #region Private
    
    [SerializeField] private float _currentVolume;
    [SerializeField] private float _maxVolume;
    private float _minVolume = 0;
    
    [SerializeField] private float _volumeIncrement;
    [SerializeField] private float _volumeDecrement;
    [SerializeField] private AudioSource[] _audioLayer1;
    [SerializeField] private AudioSource[] _audioLayer2;
    [SerializeField] private AudioSource[] _audioLayer3;

    private int[] _maxVolumeSteps = new int[4];

    #endregion
    
    #region Unity API
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SetupAudioSources();
        GetMaxVolumeSteps(0, 3);
        foreach (var i in _maxVolumeSteps)
        {
            //Debug.Log($"{i} maps to {SplitIntoSteps(0,3,_minVolume,_maxVolume, i)}");
            Debug.Log($"{i}");
        }
    }

    // Update is called once per frame
    void Update()
    {
        DecreaseVolume(_volumeDecrement);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Particle"))
        {
            IncreaseVolume(_volumeIncrement);
            
            switch (_volumeIncrement)
            {
                
            }
        }
    }
    
    #endregion

    #region Main Methods

    private void IncreaseVolume(float amount)
    {
        if (_currentVolume < _maxVolume) _currentVolume += amount;
    }

    private void DecreaseVolume(float amount)
    {
        if (_currentVolume > _minVolume) _minVolume -= amount * Time.deltaTime;
    }

    #endregion
    
    #region Utils

    private void SetupAudioSources()
    {
        SetupAudioLayers(_audioLayer1);
        SetupAudioLayers(_audioLayer2);
        SetupAudioLayers(_audioLayer3);
    }

    private void SetupAudioLayers(AudioSource[] audioLayer)
    {
        foreach (var audioSource in audioLayer)
        {
            audioSource.loop = true;
            audioSource.volume = _minVolume;
        }
    }

    private void PlayAudioLayer(AudioSource[] audioLayer)
    {
        foreach (var audioSource in audioLayer)
        {
            audioSource.volume = _currentVolume;
            audioSource.Play();
        }
    }

    private void GetMaxVolumeSteps(int indexMin, int indexMax)
    {
        Enumerable.Range(indexMin, indexMax+1)
            .ToList()
            .ForEach(i => _maxVolumeSteps[i] = (int)SplitIntoSteps(indexMin, indexMax, _minVolume,_maxVolume, i));
    }
    
    private float SplitIntoSteps(int indexMin, int indexMax, float rangeMin, float rangeMax, int currentIndex) => rangeMin + (currentIndex - indexMin) * (rangeMax - rangeMin) / (indexMax - indexMin);
    
    #endregion
}
