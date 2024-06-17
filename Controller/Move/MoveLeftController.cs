using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeftController : MoveController {

    private IMoveLeft moveCallback;

    public void Init(IMoveLeft moveCallback, float speed) {
        this.moveCallback = moveCallback;
        base.Init(speed);
    }

    protected override void MoveListener() {
        _keyController.OnLeftMovement(() => {
            currIsMoving = true;
            Move();
        });
    }

    protected override void StopListener() {
        _keyController.OnHorizontalZero(() => {
            currIsMoving = false;
        });
    }

    protected override void Move() {
        Vector3 direction = Vector3.left * speed * Time.deltaTime;
        _transform.Translate(direction, Space.World);
    }

    protected override void MoveStartCallback() {
        Debug.Log("Move Left Start");
        moveCallback.OnMovementStart();
    }

    protected override void MoveContinueCallback() {
        Debug.Log("Moving Left");
        moveCallback.OnMovementContinue();
    }

    protected override void MoveFinishCallback() {
        Debug.Log("Move Left Finish");
        moveCallback.OnMovementFinish();
    }
}