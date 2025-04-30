using System;
using System.Linq;
using Codice.CM.Common;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class VolumeBoxScript : MonoBehaviour
{

    #region Private
    
    [SerializeField] private float _currentVolume;
    [SerializeField] private float _maxVolume;
    private float _minVolume = 0;
    private float _currentAudioTime;
    
    [SerializeField] private float _volumeIncrement;
    [SerializeField] private float _volumeDecrement;
    [SerializeField] private AudioSource[] _audioLayer1;
    [SerializeField] private AudioSource[] _audioLayer2;
    [SerializeField] private AudioSource[] _audioLayer3;

    private int[] _maxVolumeSteps = new int[4];

    private enum AudioStep
    {
        
    }

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
        DecreaseVolumeOverTime(_volumeDecrement);
        _currentAudioTime = _audioLayer1[0].time;
    }

    /*private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Particle"))
        {
            IncreaseVolume(_volumeIncrement);
            
            switch (_volumeIncrement)
            {
                
            }
        }
    }*/

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Particle"))
        {
            IncreaseVolume(_volumeIncrement);

            if (_currentVolume >= _maxVolumeSteps[0] && _currentVolume <= _maxVolumeSteps[1])
            {
                StopAudioLayer(_audioLayer2);
                PlayAudioLayer(_audioLayer1);
            }
            if (_currentVolume >= _maxVolumeSteps[1] && _currentVolume <= _maxVolumeSteps[2])
            {
                StopAudioLayer(_audioLayer3);
                PlayAudioLayer(_audioLayer2);
            }
            if (_currentVolume >= _maxVolumeSteps[2] && _currentVolume <= _maxVolumeSteps[3]) PlayAudioLayer(_audioLayer3);
            
            // TODO: stop audio layers when current volume is not on audio step anymore
        }
    }

    #endregion

    #region Main Methods

    private void IncreaseVolume(float amount)
    {
        if (_currentVolume < _maxVolume) _currentVolume += amount;
        Debug.Log("Volume Increased");
    }

    private void DecreaseVolumeOverTime(float amount)
    {
        if (_currentVolume > _minVolume) _currentVolume -= amount * Time.deltaTime;
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
            audioSource.time = _currentAudioTime;
            if (!audioSource.isPlaying) audioSource.Play();
        }
    }

    private void StopAudioLayer(AudioSource[] audioLayer)
    {
        foreach (var audioSource in audioLayer)
        {
            // audioSource.volume = _currentVolume;
            // audioSource.time = _currentAudioTime;
            if (audioSource.isPlaying) audioSource.Stop();
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
