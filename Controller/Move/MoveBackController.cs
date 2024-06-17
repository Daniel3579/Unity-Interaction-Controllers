using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBackController : MoveController {

    private IMoveBack moveCallback;

    public void Init(IMoveBack moveCallback, float speed) {
        this.moveCallback = moveCallback;
        base.Init(speed);
    }

    protected override void MoveListener() {
        _keyController.OnBackMovement(() => {
            currIsMoving = true;
            Move();
        });
    }

    protected override void StopListener() {
        _keyController.OnVerticalZero(() => {
            currIsMoving = false;
        });
    }

    protected override void Move() {
        Vector3 direction = Vector3.back * speed * Time.deltaTime;
        _transform.Translate(direction, Space.World);
    }

    protected override void MoveStartCallback() {
        Debug.Log("Move Back Start");
        moveCallback.OnMovementStart();
    }

    protected override void MoveContinueCallback() {
        Debug.Log("Moving Back");
        moveCallback.OnMovementContinue();
    }

    protected override void MoveFinishCallback() {
        Debug.Log("Move Back Finish");
        moveCallback.OnMovementFinish();
    }
}