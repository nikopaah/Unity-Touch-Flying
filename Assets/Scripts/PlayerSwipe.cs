using DigitalRubyShared;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSwipe : MonoBehaviour
{
    public FingersScript FingerScript;
    private SwipeGestureRecognizer swipeGesture;

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
        swipeGesture.Updated += SwipeGestureCallback;
        swipeGesture.DirectionThreshold = 1.0f;
        FingerScript.AddGesture(swipeGesture);
    }

    private void SwipeGestureCallback(GestureRecognizer gesture, ICollection<GestureTouch> touches) 
    {
        if (gesture.State == GestureRecognizerState.Ended) 
        {
            GestureTouch t = FirstTouch(touches);

            if (t.IsValid())
            {
                HandleSwipe(t.X, t.Y);
                print("Swiped at "+ t.X + ", "+ t.Y + "; velocity: "+ swipeGesture.VelocityX + ", "+ swipeGesture.VelocityY + "");
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
