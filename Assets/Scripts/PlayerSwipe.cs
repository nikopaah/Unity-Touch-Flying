using DigitalRubyShared;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSwipe : MonoBehaviour
{
    public FingersScript FingerScript;
    private SwipeGestureRecognizer swipeGesture;

    [SerializeField]
    private GameObject Player;

    private Vector3 MovementAmount;
    private Vector2 ActualVelocity;
    private bool isFlying = false;
    private float timer = 0.0f;

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
            //PlayerFly(gesture, swipeGesture);
            //print("Swiped from "+ gesture.StartFocusX + ","+ gesture.StartFocusY + " to "+ gesture.FocusX + ","+ gesture.FocusY + "; velocity: "+ swipeGesture.VelocityX + ", " + swipeGesture.VelocityY);
        }
        if(swipeGesture.State == GestureRecognizerState.Ended) { StartFlight(new Vector2(swipeGesture.VelocityX, swipeGesture.VelocityY)); }
    }

    private void StartFlight(Vector2 Velocity) 
    {
        isFlying = true;
        Velocity = Velocity / 100;
        ActualVelocity = Velocity;
        print(Velocity);
    }

    private void FlightPath() 
    {
        timer += Time.deltaTime;

        float xPos, yPos, zPos;

        xPos = ActualVelocity.x * Time.deltaTime;
        zPos = ActualVelocity.y * Time.deltaTime;

        // Stage A
        if (timer < 1.5f)
        {
            yPos = Mathf.Pow(1.5f, ActualVelocity.x) * Time.deltaTime;
            //Player.transform.Translate(0, zPos, 0);
        }


        // Stage B
        else if (timer < 5f)
        {
            yPos = -0.02f * ActualVelocity.x * Time.deltaTime;
            //Player.transform.Translate(0, zPos, 0);
        }


        // Stage C
        else
        {
            if (Player.transform.position.y <= 1)
            {
                isFlying = false;
                timer = 0f;
            }

            yPos = Mathf.Pow(-1.25f, ActualVelocity.x) * Time.deltaTime;
            //Player.transform.Translate(0, zPos, 0);
        }

        Player.transform.Translate(xPos, yPos, zPos);
    }

    void Update()
    {
        if (isFlying) FlightPath();
    }
}
