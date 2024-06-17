using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyController : MonoBehaviour {
    
    public delegate void Callback();

    public void OnForwardMovement(Callback callback) {
        if (Input.GetAxis("Vertical") > 0) {
            callback();
        }
    }

    public void OnVerticalZero(Callback callback) {
        if (Input.GetAxis("Vertical") == 0) {
            callback();
        }
    }

    public void OnBackMovement(Callback callback) {
        if (Input.GetAxis("Vertical") < 0) {
            callback();
        }
    }

    public void OnRightMovement(Callback callback) {
        if (Input.GetAxis("Horizontal") > 0) {
            callback();
        }
    }

    public void OnHorizontalZero(Callback callback) {
        if (Input.GetAxis("Horizontal") == 0) {
            callback();
        }
    }

    public void OnLeftMovement(Callback callback) {
        if (Input.GetAxis("Horizontal") < 0) {
            callback();
        }
    }

    public void OnShiftDown(Callback callback) {
        if (Input.GetKeyDown(KeyCode.LeftShift)) {
            callback();
        }
    }

    public void OnShiftHold(Callback callback) {
        if (Input.GetKey(KeyCode.LeftShift)) {
            callback();
        }
    }

    public void OnShiftNotHold(Callback callback) {
        if (!Input.GetKey(KeyCode.LeftShift)) {
            callback();
        }
    }

    public void OnShiftUp(Callback callback) {
        if (Input.GetKeyUp(KeyCode.LeftShift)) {
            callback();
        }
    }

    public void OnSpaceDown(Callback callback) {
        if (Input.GetKeyDown(KeyCode.Space)) {
            callback();
        }
    }
}