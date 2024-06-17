using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelCube : MonoBehaviour, IMoveForward, IMoveBack, IMoveRight, IMoveLeft, IAccelerate, IJump, IBar {
    [SerializeField] private float speed = 5;
    [SerializeField] private float acceleration = 7;
    [SerializeField] private float jumpForce = 9;
    [SerializeField] private float maxEnergy = 11;
    [SerializeField] private float barIncreaseValue = 0.0005F;
    [SerializeField] private float barDecreaseValue = 0.01F;
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
    }

    private void Start() {
        RuntimeAnimatorController animatorController = Resources.Load<RuntimeAnimatorController>("Anim/Cube/Cube");
        _animator.runtimeAnimatorController = animatorController;

        _accelerationController?.Init(this, speed * acceleration);
        _moveForwardController?.Init(this, speed);
        _moveRightController?.Init(this, speed);
        _moveBackController?.Init(this, speed);
        _moveLeftController?.Init(this, speed);
        _jumpController?.Init(this, jumpForce);
        _barController?.Init(this, maxEnergy, maxEnergy * 0.3F, barDecreaseValue);
    }

    private void OnValidate() {
        _accelerationController?.Init(this, speed * acceleration);
        _moveForwardController?.Init(this, speed);
        _moveRightController?.Init(this, speed);
        _moveBackController?.Init(this, speed);
        _moveLeftController?.Init(this, speed);
        _jumpController?.Init(this, jumpForce);
        _barController?.Init(this, maxEnergy, maxEnergy * 0.3F, barDecreaseValue);
    }

    private void Update() {
        isMoving = false;
    }

    private void LateUpdate() {
        if (isMoving) {
            _animator.SetBool("isRun", true);

        } else {
            _animator.SetBool("isRun", false);
        }
    }

    void IAccelerate.OnMovementStart() {
        _moveForwardController.DenyMove();
        _audioController.Play(_audioController.onAccelerateStart1);
        _particleSystem.Play();
    }

    void IAccelerate.OnMovementContinue() {
        _barController.Decrease(barDecreaseValue);
        isMoving = true;
    }

    void IAccelerate.OnMovementFinish() {
        _moveForwardController.AllowMove();
        _audioController.Play(_audioController.onAccelerateFinish2);
    }

    void IMoveForward.OnMovementContinue() {
        _barController.Increase(barIncreaseValue);
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

    public void OnJumpStart() {
        _animator.SetTrigger("Jump");
        _audioController.Play(_audioController.onJumpStart1);
    }

    public void OnJumpFinish() {
        _audioController.Play(_audioController.onJumpFinish2);
    }

    public void OnBarBecomeEnough() {
        _accelerationController.AllowMove();
    }

    public void OnBarBecomeNotEnough() {
        _accelerationController.DenyMove();
    }
}