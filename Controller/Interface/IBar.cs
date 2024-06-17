using UnityEngine;

public interface IBar {

    void OnBarBecomeFull() {}

    void OnBarBecomeEnough() {}

    void OnBarBecomeNotEnough() {}

    void OnBarIncrease() {}
    
    void OnBarDecrease() {}
}