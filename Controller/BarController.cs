using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarController: MonoBehaviour {

	private Slider _slider;
    private IBar barCallback;
    private bool isFull = true;
    private bool isEnough = true;
    private bool currDirection = false;
    private bool prevDirection = false;
    private float enoughBoundary;
    private float notEnoughBoundary;

    protected void Awake() {
        _slider = GetComponent<Slider>() != null ? GetComponent<Slider>() : gameObject.AddComponent<Slider>();
    }

    public void Init(IBar barCallback, float maxValue, float enoughBoundary, float notEnoughBoundary = 0.1F) {
        this.notEnoughBoundary = notEnoughBoundary;
        this.enoughBoundary = enoughBoundary;
        this.barCallback = barCallback;
        _slider.maxValue = maxValue;
        _slider.value = maxValue;
    }

    private void Update() {
        OnBarBecomeFull();
        OnBarBecomeEnough();
        OnBarBecomeNotEnough();
        OnBarIncrease();
        OnBarDecrease();
    }

    public void Increase(float value) {
        _slider.value += value;
        currDirection = true;
        prevDirection = false;
    }

    public void Decrease(float value) {
        _slider.value -= value;
        currDirection = false;
        prevDirection = true;
    }

    public float GetCurrentValue() {
        return _slider.value;
    }

    public float GetMaxValue() {
        return _slider.maxValue;
    }

    private void OnBarBecomeFull() {
        if (GetCurrentValue() == GetMaxValue() && !isFull) {
            Debug.Log("Bar has become Full");
            isFull = true;
            barCallback.OnBarBecomeFull();
        }
    }
    
    private void OnBarBecomeEnough() {
        if (GetCurrentValue() >= enoughBoundary && !isEnough) {
            Debug.Log("Bar has become Enough");
            isEnough = true;
            barCallback.OnBarBecomeEnough();
        }
    }

    private void OnBarBecomeNotEnough() {
        if (GetCurrentValue() < notEnoughBoundary && isEnough) {
            Debug.Log("Bar has become Not Enough");
            isEnough = false;
            barCallback.OnBarBecomeNotEnough();
        }
    }

    private void OnBarIncrease() {
        if (!prevDirection && currDirection && !isFull) {
            Debug.Log("Bar Increase");
            prevDirection = true;
            barCallback.OnBarIncrease();
        }
    }

    private void OnBarDecrease() {
        if (prevDirection && !currDirection) {
            Debug.Log("Bar Decrease");
            prevDirection = false;

            if (isFull) {
                isFull = false;
            }

            barCallback.OnBarDecrease();
        }
    }
}