using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bonus : MonoBehaviour {

    private ParticleSystem _particleSystem;
    private AudioController _audioController;
    private Animator _animator;
    private Score _score;
    
    private void Awake() {
        _particleSystem = transform.parent.Find("Partical").GetComponent<ParticleSystem>() ?? throw new MissingComponentException("ParticleSystem not found");
        _audioController = GetComponent<AudioController>() != null ? GetComponent<AudioController>() : gameObject.AddComponent<AudioController>();
        _score = GameObject.Find("Score").GetComponentInChildren<Score>() ?? throw new MissingComponentException("Score not found");
        _animator = GetComponent<Animator>() != null ? GetComponent<Animator>() : gameObject.AddComponent<Animator>();
    }

    private void Start() {
        RuntimeAnimatorController animatorController = Resources.Load<RuntimeAnimatorController>("Anim/Bonus/Bonus");
        _animator.runtimeAnimatorController = animatorController;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            Debug.Log("Bonus Taken");
            _score.Increase();
            _animator.SetTrigger("Take");
            _particleSystem.Play();
            _audioController.Play(_audioController.onBonusTake);
            StartCoroutine(DestroyAfterEffectsCoroutine());
        }
    }

    IEnumerator DestroyAfterEffectsCoroutine() {
        while (_audioController.GetAudioSource().isPlaying || IsAnimationPlaying()) {
            yield return null;
        }

        Destroy(transform.parent.gameObject);
    }

    private bool IsAnimationPlaying() {
        return _animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1 || _animator.IsInTransition(0);
    }
}