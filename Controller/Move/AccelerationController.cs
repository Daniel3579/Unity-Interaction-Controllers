using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccelerationController : MoveController {

    private IAccelerate moveCallback;

    public void Init(IAccelerate moveCallback, float speed) {
        this.moveCallback = moveCallback;
        base.Init(speed);
    }
    
    protected override void MoveListener() {
        _keyController.OnShiftHold(() => {
            _keyController.OnForwardMovement(() => {
                currIsMoving = true;
                Move();
            });
        }); 
    }

    protected override void StopListener() {
        _keyController.OnVerticalZero(() => {
            currIsMoving = false;
        });

        _keyController.OnShiftUp(() => {
            currIsMoving = false;
        });
    }

    protected override void Move() {
        Vector3 direction = Vector3.forward * speed * Time.deltaTime;
        _transform.Translate(direction, Space.World);
    }

    protected override void MoveStartCallback() {
        Debug.Log("Acceleration Start");
        moveCallback.OnMovementStart();
    }

    protected override void MoveContinueCallback() {
        Debug.Log("Accelerating");
        moveCallback.OnMovementContinue();
    }

    protected override void MoveFinishCallback() {
        Debug.Log("Acceleration Finish");
        moveCallback.OnMovementFinish();
    }
}