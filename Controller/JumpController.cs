using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpController : MonoBehaviour {

    private KeyController _keyController;
    private Rigidbody _rigidbody;
    private IJump jumpCallback;
    private float jumpForce;
    private bool currIsJumping = false;
    private bool prevIsJumping = false;
    private bool allowJump = true;

    private void Awake() {
        _keyController = GetComponent<KeyController>() != null ? GetComponent<KeyController>() : gameObject.AddComponent<KeyController>();
        _rigidbody = GetComponent<Rigidbody>() != null ? GetComponent<Rigidbody>() : gameObject.AddComponent<Rigidbody>();
    }

    public void Init(IJump jumpCallback, float jumpForce) {
        this.jumpCallback = jumpCallback;
        this.jumpForce = jumpForce;
    }

    private void Update() {
        if (allowJump) {
            JumpListener();
        }

        OnJumpStart();
        OnJumpContinue();
        OnJumpFinish();
    }

    private void JumpListener() {
        _keyController.OnSpaceDown(() => {
            if (!currIsJumping) {
                currIsJumping = true;
                Jump();
            }
        });
    }

    public void AllowJump() {
        Debug.Log("Allow Jump");
        allowJump = true;
    }

    public void DenyJump() {
        Debug.Log("Deny Jump");
        allowJump = false;
    }

    private void Jump() {
        _rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    private void OnJumpStart() {
        if (!prevIsJumping && currIsJumping) {
            Debug.Log("Jump Start");
            prevIsJumping = true;
            jumpCallback.OnJumpStart();
        }
    }

    private void OnJumpContinue() {
        if (prevIsJumping && currIsJumping) {
            Debug.Log("Jumping");
            jumpCallback.OnJumpContinue();
        }
    }

    private void OnJumpFinish() {
        if (prevIsJumping && !currIsJumping) {
            Debug.Log("Jump Finish");
            prevIsJumping = false;
            jumpCallback.OnJumpFinish();
        }
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag == "Ground") {
            currIsJumping = false;
        }
    }
}