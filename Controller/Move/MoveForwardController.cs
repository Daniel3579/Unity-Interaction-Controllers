using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForwardController : MoveController {

    private IMoveForward moveCallback;

    public void Init(IMoveForward moveCallback, float speed) {
        this.moveCallback = moveCallback;
        base.Init(speed);
    }
    
    protected override void MoveListener() {
        _keyController.OnForwardMovement(() => {
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
        Vector3 direction = Vector3.forward * speed * Time.deltaTime;
        _transform.Translate(direction, Space.World);
    }

    protected override void MoveStartCallback() {
        Debug.Log("Move Forward Start");
        moveCallback.OnMovementStart();
    }

    protected override void MoveContinueCallback() {
        Debug.Log("Moving Forward");
        moveCallback.OnMovementContinue();
    }

    protected override void MoveFinishCallback() {
        Debug.Log("Move Forward Finish");
        moveCallback.OnMovementFinish();
    }
}