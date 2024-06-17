using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

public class ModelToilet : MonoBehaviour, IMoveForward, IMoveBack, IMoveRight, IMoveLeft, IAccelerate, IJump, IBar {

  [SerializeField] private float speed = 5;
  [SerializeField] private float acceleration = 7;
  [SerializeField] private float jumpForce = 9;
  [SerializeField] private float maxEnergy = 11;
  [SerializeField] private float barDecreaseValue = 2.2F;
  private AccelerationController _accelerationController;
  private MoveForwardController _moveForwardController;
  private MoveRightController _moveRightController;
  private MoveBackController _moveBackController;    
  private MoveLeftController _moveLeftController;
  private AudioController _audioController;
  private ParticleSystem _particleSystem;
  private JumpController _jumpController;
  private BarController _barController;
  private Animator _animator;
  private Rigidbody _rigidbody;
  private Score _score;
  private bool isMoving = false;

  private void Awake() {
    _accelerationController = GetComponent<AccelerationController>() != null ? GetComponent<AccelerationController>() : gameObject.AddComponent<AccelerationController>();
    _moveForwardController = GetComponent<MoveForwardController>() != null ? GetComponent<MoveForwardController>() : gameObject.AddComponent<MoveForwardController>();
    _moveRightController = GetComponent<MoveRightController>() != null ? GetComponent<MoveRightController>() : gameObject.AddComponent<MoveRightController>();
    _moveBackController = GetComponent<MoveBackController>() != null ? GetComponent<MoveBackController>() : gameObject.AddComponent<MoveBackController>();
    _moveLeftController = GetComponent<MoveLeftController>() != null ? GetComponent<MoveLeftController>() : gameObject.AddComponent<MoveLeftController>();
    _audioController = GetComponent<AudioController>() != null ? GetComponent<AudioController>() : gameObject.AddComponent<AudioController>();
    _jumpController = GetComponent<JumpController>() != null ? GetComponent<JumpController>() : gameObject.AddComponent<JumpController>();
    _barController = GetComponentInChildren<BarController>() ?? throw new MissingComponentException("BarController not found");
    _particleSystem = GetComponent<ParticleSystem>() ?? throw new MissingComponentException("ParticleSystem not found");
    _animator = GetComponent<Animator>() != null ? GetComponent<Animator>() : gameObject.AddComponent<Animator>();
    _rigidbody = GetComponent<Rigidbody>() != null ? GetComponent<Rigidbody>() : gameObject.AddComponent<Rigidbody>();
    _score = GameObject.Find("Score").GetComponentInChildren<Score>() ?? throw new MissingComponentException("Score not found");
  }

  private void Start() {
    RuntimeAnimatorController animatorController = Resources.Load<RuntimeAnimatorController>("Anim/Toilet/Toilet");
    _animator.runtimeAnimatorController = animatorController;

    _accelerationController?.Init(this, speed * acceleration);
    _moveForwardController?.Init(this, speed);
    _moveRightController?.Init(this, speed);
    _moveBackController?.Init(this, speed);
    _moveLeftController?.Init(this, speed);
    _jumpController?.Init(this, jumpForce);
    _barController?.Init(this, maxEnergy, barDecreaseValue);
  }

  private void OnValidate() {
    _accelerationController?.Init(this, speed * acceleration);
    _moveForwardController?.Init(this, speed);
    _moveRightController?.Init(this, speed);
    _moveBackController?.Init(this, speed);
    _moveLeftController?.Init(this, speed);
    _jumpController?.Init(this, jumpForce);
    _barController?.Init(this, maxEnergy, barDecreaseValue);
  }

  private void Update() {
      isMoving = false;
  }

  private void LateUpdate() {
    if (isMoving) {
      _animator.SetBool("isWalk", true);

    } else {
      _animator.SetBool("isWalk", false);
    }
  }

  private void OnCollisionEnter(Collision collision) {
    if (collision.gameObject.tag == "Enemy") {
      _rigidbody.AddForce(Vector3.up * 10, ForceMode.Impulse);
      _barController.Decrease(barDecreaseValue);
      _audioController.Play(_audioController.scream);
    }
  }

  public void OnJumpStart() {
    _animator.SetTrigger("Jump");
    _audioController.Play(_audioController.onJumpStart1);
  }

  public void OnJumpFinish() {
    _audioController.Play(_audioController.onJumpFinish3);
    _particleSystem.Play();
  }

  public void OnBarBecomeNotEnough() {
    PlayerPrefs.SetInt("Score", _score.GetScore());
    PlayerPrefs.Save();
    SceneManager.LoadScene("GameOver");
  }

  void IMoveForward.OnMovementContinue() {
    isMoving = true;
  }

  void IMoveBack.OnMovementContinue() {
    isMoving = true;
  }

  void IMoveRight.OnMovementContinue() {
    isMoving = true;
  }

  void IMoveLeft.OnMovementContinue() {
    isMoving = true;
  }

  void IAccelerate.OnMovementStart() {
    _moveForwardController.DenyMove();
    _audioController.Play(_audioController.onAccelerateStart1);
  }

  void IAccelerate.OnMovementContinue() {
    isMoving = true;
  }

  void IAccelerate.OnMovementFinish() {
    _moveForwardController.AllowMove();
    _audioController.Play(_audioController.onAccelerateFinish1);
  }
}