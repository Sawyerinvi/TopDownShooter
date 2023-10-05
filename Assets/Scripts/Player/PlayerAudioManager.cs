using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudioManager : MonoBehaviour
{
    [SerializeField] private AudioClip _shootingAk74Clip;
    [SerializeField] private AudioClip _shootingMakarovClip;
    [SerializeField] private AudioClip _reloadAk74Clip;
    [SerializeField] private AudioClip _reloadMakarovClip;
    [Range(0,1)]
    [SerializeField] private float _volume;

    private AudioSource _shootingAk74Source;
    private AudioSource _shootingMakarovSource;
    private AudioSource _reloadAk74Source;
    private AudioSource _reloadMakarovSource;

    void Awake()
    {
        _shootingAk74Source = SetUpAudioSource(_shootingAk74Clip);
        _shootingMakarovSource = SetUpAudioSource(_shootingMakarovClip);
        _reloadAk74Source = SetUpAudioSource(_reloadAk74Clip);
        _reloadMakarovSource = SetUpAudioSource(_reloadMakarovClip);

    }

    private AudioSource SetUpAudioSource(AudioClip clip)
    {
        var source = gameObject.AddComponent<AudioSource>();
        source.playOnAwake = false;
        source.clip = clip;
        source.volume = _volume;
        source.loop = false;
        return source;
    }

    public void ShootAK74()
    {
        _shootingAk74Source.Play();
    }
    public void ShootMakarov()
    {
        _shootingMakarovSource.Play();
    }
    public void ReloadAK74()
    {
        _reloadAk74Source.Play();
    }
    public void ReloadMakarov()
    {
        _reloadMakarovSource.Play();
    }
}
