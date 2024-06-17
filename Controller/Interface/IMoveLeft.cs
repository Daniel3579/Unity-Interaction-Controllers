using UnityEngine;

public interface IMoveLeft: IMove {

    new void OnMovementStart() {}

    new void OnMovementContinue() {}
    
    new void OnMovementFinish() {}
}