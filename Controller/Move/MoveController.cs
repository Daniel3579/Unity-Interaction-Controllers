using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MoveController: MonoBehaviour {
    
    protected abstract void MoveListener();
    protected abstract void StopListener();
    protected abstract void Move();
    protected abstract void MoveStartCallback();
    protected abstract void MoveContinueCallback();
    protected abstract void MoveFinishCallback();
    protected KeyController _keyController;
    protected Transform _transform;
    protected float speed;
    protected bool currIsMoving = false;
    private bool prevIsMoving = false;
    private bool allowMove = true;

    private void Awake() {
        _keyController = GetComponent<KeyController>() != null ? GetComponent<KeyController>() : gameObject.AddComponent<KeyController>();
        _transform = GetComponent<Transform>() != null ? GetComponent<Transform>() : gameObject.AddComponent<Transform>();
    }

    protected void Init(float speed)  {
        this.speed = speed;
    }

    private void Update() {
        if (allowMove) {
            MoveListener();
        }

        StopListener();

        OnMovementStart();
        OnMovementContinue();
        OnMovementFinish();
    }

    public void AllowMove() {
        Debug.Log("Allow Move");
        allowMove = true;
    }

    public void DenyMove() {
        Debug.Log("Deny Move");
        allowMove = false;
        currIsMoving = false;
    }

    private void OnMovementStart() {
        if (!prevIsMoving && currIsMoving) {
            prevIsMoving = true;
            MoveStartCallback();
        }
    }

    private void OnMovementContinue() {
        if (prevIsMoving && currIsMoving) {
            MoveContinueCallback();
        }
    }

    private void OnMovementFinish() {
        if (prevIsMoving && !currIsMoving) {
            prevIsMoving = false;
            MoveFinishCallback();
        }
    }
}