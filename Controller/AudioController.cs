using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class AudioController : MonoBehaviour {

    private AudioSource _audioSource;
    public AudioClip onJumpStart1;
    public AudioClip onJumpFinish1;
    public AudioClip onJumpFinish2;
    public AudioClip onJumpFinish3;
    public AudioClip onAccelerateStart1;
    public AudioClip onAccelerateStart2;
    public AudioClip onAccelerateFinish1;
    public AudioClip onAccelerateFinish2;
    public AudioClip onBonusTake;
    public AudioClip scream;

    private void Awake() {
        _audioSource = GetComponent<AudioSource>() != null ? GetComponent<AudioSource>() : gameObject.AddComponent<AudioSource>();
    }

    private void Start() {
        onJumpStart1 = Resources.Load<AudioClip>("Audio/Jump/JumpStart/JumpStart1");
        onJumpFinish1 = Resources.Load<AudioClip>("Audio/Jump/JumpFinish/JumpFinish1");
        onJumpFinish2 = Resources.Load<AudioClip>("Audio/Jump/JumpFinish/JumpFinish2");
        onJumpFinish3 = Resources.Load<AudioClip>("Audio/Jump/JumpFinish/JumpFinish3");

        onAccelerateStart1 = Resources.Load<AudioClip>("Audio/Acceleration/AccelerationStart/AccelerationStart1");
        onAccelerateStart2 = Resources.Load<AudioClip>("Audio/Acceleration/AccelerationStart/AccelerationStart2");
        onAccelerateFinish1 = Resources.Load<AudioClip>("Audio/Acceleration/AccelerationFinish/AccelerationFinish1");
        onAccelerateFinish2 = Resources.Load<AudioClip>("Audio/Acceleration/AccelerationFinish/AccelerationFinish2");

        onBonusTake = Resources.Load<AudioClip>("Audio/BonusTake");
        scream = Resources.Load<AudioClip>("Audio/Scream");
    }

    public void Play(AudioClip audioClip) {
        _audioSource.PlayOneShot(audioClip);
    }

    public AudioSource GetAudioSource() {
        return _audioSource;
    }
}