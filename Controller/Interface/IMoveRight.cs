using UnityEngine;

public interface IMoveRight: IMove {

    new void OnMovementStart() {}

    new void OnMovementContinue() {}
    
    new void OnMovementFinish() {}
}