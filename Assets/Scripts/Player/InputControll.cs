using UnityEngine;

public class InputControll : MonoBehaviour {

	bool tap, swipeLeft, swipeRight, swipeUp, swipeDown;
	bool isDragging = false;
	Vector2 startTouch, swipeDelta;
	Touch[] touches;

	public float distance = 150f;

	public Vector2 SwipeDelta { get { return swipeDelta; } }

	public bool SwipeLeft { get { return swipeLeft; } }
	public bool SwipeRight { get { return swipeRight; } }
	public bool SwipeUp { get { return swipeUp; } }
	public bool SwipeDown { get { return swipeDown;  } }
	public bool Tap { get { return tap;  } }

	#region Singleton
    public static InputControll instance;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of InputControll");
            return;
        }

        instance = this;
    }
    #endregion

	void Update()
	{
		tap = swipeLeft = swipeRight = swipeUp = swipeDown = false;

		#region Standalone Inputs
		if (Input.GetMouseButtonDown(0))	
		{
			tap = true;
			isDragging = true;
			startTouch = Input.mousePosition;
		}
		else if(Input.GetMouseButtonUp(0))
		{
			isDragging = false;
			Reset();
		}
		#endregion

		#region Mobile Inputs
		touches = Input.touches;
		if ( touches.Length != 0 )
		{
			if ( touches[0].phase == TouchPhase.Began )
			{
				tap = true;
				isDragging = true;
				startTouch = touches[0].position;
			}
			else if ( touches[0].phase == TouchPhase.Ended || touches[0].phase == TouchPhase.Canceled )
			{
				isDragging = false;
				Reset();
			}
		}
		#endregion

		//Calculate the distance
		swipeDelta = Vector2.zero;
		if( isDragging )
		{
			if ( touches.Length != 0 )
				swipeDelta = touches[0].position - startTouch;
			else if (Input.GetMouseButton(0))
				swipeDelta = (Vector2)Input.mousePosition - startTouch;
		}

		//Cross DeadZone
		if(swipeDelta.magnitude > distance)
		{
			//Direction
			float x = swipeDelta.x;
			float y = swipeDelta.y;

			if (Mathf.Abs(x) > Mathf.Abs(y))
			{
				//Left or Right
				if(x < 0)
					swipeLeft = true;
				else
					swipeRight = true;
			}
			else
			{
				//Up or Down
				if(y < 0)
					swipeDown = true;
				else
					swipeUp = true;
			}

			Reset();
		}
	}

	void Reset()
	{
		startTouch = swipeDelta = Vector2.zero;
		isDragging = false;
	}
}
