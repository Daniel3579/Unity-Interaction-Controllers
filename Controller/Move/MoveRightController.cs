using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveRightController : MoveController {

    private IMoveRight moveCallback;

    public void Init(IMoveRight moveCallback, float speed) {
        this.moveCallback = moveCallback;
        base.Init(speed);
    }

    protected override void MoveListener() {
        _keyController.OnRightMovement(() => {
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
        Vector3 direction = Vector3.right * speed * Time.deltaTime;
        _transform.Translate(direction, Space.World);
    }

    protected override void MoveStartCallback() {
        Debug.Log("Move Right Start");
        moveCallback.OnMovementStart();
    }

    protected override void MoveContinueCallback() {
        Debug.Log("Moving Right");
        moveCallback.OnMovementContinue();
    }

    protected override void MoveFinishCallback() {
        Debug.Log("Move Right Finish");
        moveCallback.OnMovementFinish();
    }
}