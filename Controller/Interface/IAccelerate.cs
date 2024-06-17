using UnityEngine;

public interface IAccelerate: IMove {

    new void OnMovementStart() {}

    new void OnMovementContinue() {}
    
    new void OnMovementFinish() {}
}