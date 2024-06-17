using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ModelFox : MonoBehaviour, IMoveForward, IMoveBack, IMoveRight, IMoveLeft, IAccelerate, IJump, IBar {

    [SerializeField] private float speed = 5;
    [SerializeField] private float acceleration = 7;
    [SerializeField] private float jumpForce = 9;
    [SerializeField] private float maxEnergy = 11;
    [SerializeField] private float barIncreaseValue = 0.0005F;
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
    private bool isMoving = false;

    private void Awake() {
        _accelerationController = gameObject.AddComponent<AccelerationController>();
        _moveForwardController = gameObject.AddComponent<MoveForwardController>();
        _moveRightController = gameObject.AddComponent<MoveRightController>();
        _moveBackController = gameObject.AddComponent<MoveBackController>();
        _moveLeftController = gameObject.AddComponent<MoveLeftController>();
        _audioController = gameObject.AddComponent<AudioController>();
        _jumpController = gameObject.AddComponent<JumpController>();
        _barController = GetComponentInChildren<BarController>() ?? throw new MissingComponentException("BarController not found");
        _particleSystem = GetComponent<ParticleSystem>() ?? throw new MissingComponentException("ParticleSystem not found");
        _animator = GetComponent<Animator>() != null ? GetComponent<Animator>() : gameObject.AddComponent<Animator>();
    }

    private void Start() {
        RuntimeAnimatorController animatorController = Resources.Load<RuntimeAnimatorController>("Anim/Fox/Fox");
        _animator.runtimeAnimatorController = animatorController;

        _accelerationController?.Init(this, speed * acceleration);
        _moveForwardController?.Init(this, speed);
        _moveRightController?.Init(this, speed);
        _moveBackController?.Init(this, speed);
        _moveLeftController?.Init(this, speed);
        _jumpController?.Init(this, jumpForce);
        _barController?.Init(this, maxEnergy, barDecreaseValue, barDecreaseValue);
    }

    private void OnValidate() {
        _accelerationController?.Init(this, speed * acceleration);
        _moveForwardController?.Init(this, speed);
        _moveRightController?.Init(this, speed);
        _moveBackController?.Init(this, speed);
        _moveLeftController?.Init(this, speed);
        _jumpController?.Init(this, jumpForce);
        _barController?.Init(this, maxEnergy, barDecreaseValue, barDecreaseValue);
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

    void IAccelerate.OnMovementStart() {
        _moveForwardController.DenyMove();
        _audioController.Play(_audioController.onAccelerateStart2);
    }

    void IAccelerate.OnMovementContinue() {
        _barController.Increase(barIncreaseValue * acceleration);
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
        _barController.Decrease(barDecreaseValue);
    }

    public void OnJumpFinish() {
        _audioController.Play(_audioController.onJumpFinish1);
        _particleSystem.Play();
    }

    public void OnBarBecomeEnough() {
        _jumpController.AllowJump();
    }

    public void OnBarBecomeNotEnough() {
        _jumpController.DenyJump();
    }
}