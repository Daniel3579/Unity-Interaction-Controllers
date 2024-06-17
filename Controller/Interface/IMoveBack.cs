using UnityEngine;

public interface IMoveBack: IMove {

    new void OnMovementStart() {}

    new void OnMovementContinue() {}
    
    new void OnMovementFinish() {}
}