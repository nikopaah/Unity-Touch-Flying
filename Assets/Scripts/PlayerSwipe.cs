using DigitalRubyShared;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSwipe : MonoBehaviour
{
    public FingersScript FingerScript;
    private SwipeGestureRecognizer swipeGesture;

    [SerializeField]
    private GameObject Player;

    private GestureTouch FirstTouch(ICollection<GestureTouch> touches)
    { 
        foreach(GestureTouch touch in touches) 
        {
            return touch;
        }
        return new GestureTouch();
    }

    void Start()
    {
        CreateSwipeGesture();
    }

    private void CreateSwipeGesture()
    {
        swipeGesture = new SwipeGestureRecognizer();
        swipeGesture.Direction = SwipeGestureRecognizerDirection.Any;
        swipeGesture.StateUpdated += SwipeGestureCallback;
        swipeGesture.DirectionThreshold = 1.0f; // allow a swipe, regardless of slope
        FingersScript.Instance.AddGesture(swipeGesture);
    }

    private void SwipeGestureCallback(GestureRecognizer gesture)
    {
        if (gesture.State == GestureRecognizerState.Ended)
        {
            //HandleSwipe(gesture.FocusX, gesture.FocusY);
            PlayerFly(gesture);
            //print("Swiped from "+ gesture.StartFocusX + ","+ gesture.StartFocusY + " to "+ gesture.FocusX + ","+ gesture.FocusY + "; velocity: "+ swipeGesture.VelocityX + ", " + swipeGesture.VelocityY);
        }
    }

    private void PlayerFly(GestureRecognizer gesture)
    {
        Vector2 direction = new Vector2(gesture.DeltaX, gesture.DeltaY);
        //print(direction);
        Player.GetComponent<Rigidbody>().AddForce(-direction.x * Constant.throwForce, direction.y * Constant.throwForce, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
