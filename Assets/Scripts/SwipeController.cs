using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class SwipeController : MonoBehaviour
{
    private bool swiping;

    private bool eventSent;

    private Vector2 lastPosition;

    public static event Action<SwipeDirection> Swipe;
    public static event Action Tap;

    public enum SwipeDirection
    {
        Up,

        Down,

        Right,

        Left
    }

    // Update is called once per frame
    public void Update()
    {
        if (Input.touchCount == 0) return;

        if (Tap != null) Tap();

        if (Input.GetTouch(0).deltaPosition.sqrMagnitude != 0)
        {
            if (swiping == false)
            {
                swiping = true;
                lastPosition = Input.GetTouch(0).position;
                return;
            }

            if (!this.eventSent && Swipe != null)
            {
                Vector2 direction = Input.GetTouch(0).position - this.lastPosition;

                if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
                {
                    Swipe(direction.x > 0 ? SwipeDirection.Right : SwipeDirection.Left);
                }
                else
                {
                    Swipe(direction.y > 0 ? SwipeDirection.Up : SwipeDirection.Down);
                }

                this.eventSent = true;
            }
        }
        else
        {
            swiping = false;
            eventSent = false;
        }
    }
}