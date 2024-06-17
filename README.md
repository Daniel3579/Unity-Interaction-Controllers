# Unity Interaction Controllers

Allows for quickly adding movement and jump capabilities to an object and makes it easier to write object behavior scripts by responding to change events of:
- Movement
- Jumping
- UI

## Like this

``` c#
public class ObjectBehaviorModel: MonoBehaviour, IJump {

  JumpController _jumpController;
  
  void Start() {
    _jumpController = gameObject.AddComponent<JumpController>();
    _jumpController.Init(this, jumpForce);
  }

  void Update() {…}
  
  // IJump Interface Implementation
  public void OnJumpStart() {
    _animator.SetTrigger("Jump");
    _audioController.Play(_audioController.onJumpStart1);
    _barController.Decrease(barDecreaseValue);
  }
  
  public void OnJumpContinue() {
    // Do something while the Jump is in Continue
  }
  
  public void OnJumpFinish() {
    _audioController.Play(_audioController.onJumpFinish1);
    _particleSystem.Play();
  }
}
```

## The package includes 7 controllers:
- 4 Movement controllers for each direction:
  - Forward
  - Backward
  - Right
  - Left
- Jump controller
- Acceleration controller (a type of movement)
- UI bar controller (for health and energy bars)

---

### The movement and jump controllers each have 3 event callback void methods:
- #### For Movement:
  - `OnMovementStart()` – Calls once at the start of movement
  - `OnMovementContinue()` – Calls continuously while movement is ongoing
  - `OnMovementFinish()` – Calls once at the finish of movement

- #### For Jump:
  - `OnJumpStart()` – Calls once at the start of jumping
  - `OnJumpContinue()` – Calls continuously while jumping is ongoing
  - `OnJumpFinish()` – Calls once at the finish of jumping

---

### The bar controller has 5 event callback void methods:
- `OnBarBecomeFull()` – Calls once upon reaching fullness
- `OnBarBecomeEnough()` – Calls once upon reaching a certain sufficiency threshold
- `OnBarBecomeNotEnough()` – Calls once upon reaching a certain insufficiency threshold
- `OnBarIncrease()` – Calls continuously while the bar is being increased
- `OnBarDecrease()` – Calls continuously while the bar is being decreased

---

### Here are the available methods for interacting with controllers:
- #### Move Controller
  - ``` c#
    void Init(IMove moveCallback, float speed)
    ```
  - ``` c#
    void AllowMove()
    ```
  - ``` c#
    void DenyMove()
    ```

- #### Jump Controller
  - ``` c#
    void Init(IJump jumpCallback, float jumpForce)
    ```
  - ``` c#
    void AllowJump()
    ```
  - ``` c#
    void DenyJump()
    ```

- #### Bar Controller
  - ``` c#
    void Init(IBar barCallback, float maxValue, float enoughBoundary, float notEnoughBoundary = 0.1F)
    ```
  - ``` c#
    void Increase(float value)
    ```
  - ``` c#
    void Decrease(float value)
    ```
  - ``` c#
    float GetCurrentValue()
    ```
  - ``` c#
    float GetMaxValue()
    ```

## To add movement and jump capabilities, it is sufficient to create and initialize instances of the controllers

``` c#
public class ObjectBehaviorModel: MonoBehaviour {

  // Create Movement controller variable for each direction
  MoveForwardController _moveForwardController;
  MoveRightController _moveRightController; 
  MoveBackController _moveBackController;
  MoveLeftController _moveLeftController;

  // Create Jump controller variable
  JumpController _jumpController;

  // Create Acceleration controller variable
  AccelerationController _accelerationController;

  void Awake() {
    // Create Movement controller instance for each direction
    _moveForwardController = gameObject.AddComponent<MoveForwardController>();
    _moveRightController = gameObject.AddComponent<MoveRightController>();
    _moveBackController = gameObject.AddComponent<MoveBackController>();
    _moveLeftController = gameObject.AddComponent<MoveLeftController>();

    // Create Acceleration controller instance
    _accelerationController = gameObject.AddComponent<AccelerationController>();

    // Create Jump controller instance
    _jumpController = gameObject.AddComponent<JumpController>();
  }
  
  void Start() {
    // Initialize Movement controller instance for each direction
    _moveForwardController.Init(this, speed);
    _moveRightController.Init(this, speed);
    _moveBackController.Init(this, speed);
    _moveLeftController.Init(this, speed);

    // Initialize Acceleration controller instances
    _accelerationController.Init(this, speed * acceleration);

    // Initialize Jump controller instances
    _jumpController.Init(this, jumpForce);
  }
}
```

## To create custom responses to events of a specific controller, you need to implement its interface

``` c#
public class ObjectBehaviorModel: MonoBehaviour, IMoveForward, IMoveBack, IMoveRight, IMoveLeft, IAccelerate, IJump, IBar {

  // Create controllers variables
  // ...

  void Awake() {
    // Create controllers instances
    // ...
  }
  
  void Start() {
    // Initialize controllers instances
    // ...
  }

  void IMoveForward.OnMovementStart() {/* Do something when the Move Forward Starts */}
  void IMoveForward.OnMovementContinue() {/* Do something while the is Move Forward in Continue */}
  void IMoveForward.OnMovementFinish() {/* Do something when the Move Forward Finishes */}
  
  void IMoveBack.OnMovementStart() {/* Do something when the Move Back Starts */}
  void IMoveBack.OnMovementContinue() {/* Do something while the Move Back is in Continue */}
  void IMoveBack.OnMovementFinish() {/* Do something when the Move Back Finishes */}
  
  void IMoveRight.OnMovementStart() {/* Do something when the Move Right Starts */}
  void IMoveRight.OnMovementContinue() {/* Do something while the Move Right is in Continue */}
  void IMoveRight.OnMovementFinish() {/* Do something when the Move Right Finishes */}
  
  void IMoveLeft.OnMovementStart() {/* Do something when the Move Left Starts */}
  void IMoveLeft.OnMovementContinue() {/* Do something while the Move Left is in Continue */}
  void IMoveLeft.OnMovementFinish() {/* Do something when the Move Left Finishes */}
  
  void IAccelerate.OnMovementStart() {/* Do something when the Accelerate Starts */}
  void IAccelerate.OnMovementContinue() {/* Do something while the Accelerate is in Continue */}
  void IAccelerate.OnMovementFinish() {/* Do something when the Accelerate Finishes */}
  
  public void OnJumpStart() {/* Do something when the Jump Starts */}
  public void OnJumpContinue() {/* Do something while the Jump is in Continue */}
  public void OnJumpFinish() {/* Do something when the Jump Finishes */}
  
  public void OnBarBecomeFull() {/* Do something when reaching fullness */}
  public void OnBarBecomeEnough() {/* Do something when reaching a certain sufficiency threshold */}
  public void OnBarBecomeNotEnough() {/* Do something when reaching a certain insufficiency threshold */}
  public void OnBarIncrease() {/* Do something while the bar is being increased */}
  public void OnBarDecrease() {/* Do something while the bar is being decreased */}
}
```

## Install Guide
Import `Interaction Controllers.unitypackage` in Unity project  
via `Assets` &rarr; `Import Package` &rarr; `Custom Package` &rarr; `Interaction Controllers.unitypackage`

You can also import a **demo project** or run a **MacOS application**
