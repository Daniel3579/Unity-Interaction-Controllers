using UnityEngine;

public interface IMoveForward: IMove {

    new void OnMovementStart() {}

    new void OnMovementContinue() {}
    
    new void OnMovementFinish() {}
}