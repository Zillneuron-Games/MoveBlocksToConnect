using System;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }

    private float TouchFirstPosX;
    private float TouchFirstPosY;
    private float TouchSecondPosX;
    private float TouchSecondPosY;
    private float TouchDistance;

    private bool isActive;

    public event EventHandler<InputEventArgs> eventInput;

    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        isActive = true;
        TouchDistance = 60;
    }

    private void Update()
    {
        InputListener();
    }

    public void Enable()
    {
        isActive = true;
    }

    public void Disable()
    {
        isActive = false;
    }

    private void ThrowInputEvent(EInputEvent inputState)
    {
        EventHandler<InputEventArgs> tempInput = eventInput;

        if (tempInput != null)
        {
            tempInput(this, new InputEventArgs(inputState));
        }
    }

    private void InputListener()
    {
        if (Input.touchCount > 0)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                TouchFirstPosX = Input.GetTouch(0).position.x;
                TouchFirstPosY = Input.GetTouch(0).position.y;
            }
            else if (Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                TouchSecondPosX = Input.GetTouch(0).position.x;
                TouchSecondPosY = Input.GetTouch(0).position.y;

                if (Mathf.Abs(TouchFirstPosX - TouchSecondPosX) > TouchDistance || Mathf.Abs(TouchFirstPosY - TouchSecondPosY) > TouchDistance)
                {
                    if (Mathf.Abs(TouchFirstPosX - TouchSecondPosX) > Mathf.Abs(TouchFirstPosY - TouchSecondPosY))
                    {
                        if (TouchFirstPosX - TouchSecondPosX < 0)
                        {
                            ThrowInputEvent(EInputEvent.Right);
                            return;
                        }
                        else if (TouchFirstPosX - TouchSecondPosX > 0)
                        {
                            ThrowInputEvent(EInputEvent.Left);
                            return;
                        }

                    }
                    else if (Mathf.Abs(TouchFirstPosX - TouchSecondPosX) < Mathf.Abs(TouchFirstPosY - TouchSecondPosY))
                    {
                        if (TouchFirstPosY - TouchSecondPosY < 0)
                        {
                            ThrowInputEvent(EInputEvent.Up);
                            return;
                        }
                        else if (TouchFirstPosY - TouchSecondPosY > 0)
                        {
                            ThrowInputEvent(EInputEvent.Down);
                            return;
                        }
                    }
                }
            }
        }
        else
        {

        }


        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            ThrowInputEvent(EInputEvent.Up);
            return;
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            ThrowInputEvent(EInputEvent.Down);
            return;
        }
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            ThrowInputEvent(EInputEvent.Left);
            return;
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            ThrowInputEvent(EInputEvent.Right);
            return;
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            ThrowInputEvent(EInputEvent.Escape);
            return;
        }
        else if (Input.GetKeyDown(KeyCode.N))
        {
            ThrowInputEvent(EInputEvent.Next);
            return;
        }
        else if (Input.GetKeyDown(KeyCode.M))
        {
            ThrowInputEvent(EInputEvent.Menu);
            return;
        }
        else if (Input.GetKeyDown(KeyCode.P))
        {
            ThrowInputEvent(EInputEvent.Pause);
            return;
        }
        else if (Input.GetKeyDown(KeyCode.G))
        {
            ThrowInputEvent(EInputEvent.Play);
            return;
        }
    }

    private void InputListener(object sender, InputEventArgs args)
    {
        if (!isActive)
        {
            return;
        }

        ThrowInputEvent(args.InputEvent);
    }
}