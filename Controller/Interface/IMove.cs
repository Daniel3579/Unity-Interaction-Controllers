using UnityEngine;

public interface IMove {

    void OnMovementStart() {
        Debug.Log("Movement Start");
    }

    void OnMovementContinue() {
        Debug.Log("Moving");
    }
    
    void OnMovementFinish() {
        Debug.Log("Movement Finish");
    }
}