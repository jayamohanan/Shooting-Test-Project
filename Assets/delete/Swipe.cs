
#region CodeBackup
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class Swipe : MonoBehaviour
//{ 
//    [Range(0,1f)]
//    public float speed = 0.99f;
//    private  bool tap, swipeLeft, swipeRight, swipeUp, swipeDown;
//    bool isDragging;
//    private   Vector2 startTouch, swipeDelta;
//    public GameObject obj;

//    // Start is called before the first frame update
//    void Update()
//    {
//        tap = swipeLeft = swipeRight = swipeUp = swipeDown = false;
//        #region Standalone Inputs
//        if (Input.GetMouseButtonDown(0))
//        {
//            tap = true;
//            isDragging = true;
//            startTouch = Input.mousePosition;
//        }
//        else if (Input.GetMouseButtonUp(0))
//        {
//            isDragging = false;
//            Reset();
//        }
//        #endregion
//        #region Mobile Inputs
//        if (Input.touches.Length > 0)
//        {
//            if (Input.touches[0].phase == TouchPhase.Began)
//            {
//                isDragging = true;
//                tap = true;
//                startTouch = Input.touches[0].position;
//            }
//            else if (Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase ==TouchPhase.Canceled)
//            {
//                isDragging = false;
//                Reset();
//            }
//        }
//        #endregion
//        //calculate the distance
//        swipeDelta = Vector2.zero;
//        if (isDragging)
//        {
//            if (Input.touches.Length > 0)
//            {
//                swipeDelta = Input.touches[0].position - startTouch;
//            }
//            else if (Input.GetMouseButton(0))
//            {
//                swipeDelta = (Vector2)Input.mousePosition - startTouch;
//            }
//        }
//        //Did we cross the Deadzone

//        if (swipeDelta.magnitude > 1f)
//        {
//            //which direction
//            float x = swipeDelta.x;
//            float y = swipeDelta.y;
//            if (Mathf.Abs(x) > Mathf.Abs(y))
//            {
//                //Left or right
//                if (x < 0)
//                    obj.transform.position += Vector3.left*speed;
//                //swipeLeft = true;
//                else
//                    obj.transform.position += Vector3.right*speed;
//                    //swipeRight = true;
//            }
//            else
//            {
//                //Up or down
//                if (y < 0)
//                    obj.transform.position += Vector3.down*speed;
//                //swipeDown = true;
//                else
//                    obj.transform.position += Vector3.up*speed;
//                    //swipeUp = true;
//            }
//        }

//    }
//    private void Reset()
//    {
//        startTouch = swipeDelta = Vector2.zero;
//    }

//    // Update is called once per frame
//}
#endregion

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swipe : MonoBehaviour
{
    [Range(0, 1f)]
    public float speed = 0.99f;
    bool isDragging;
    private Vector2 startTouch, swipeDelta;
    public GameObject obj;

    void Update()
    {
        #region Standalone Inputs
        if (Input.GetMouseButtonDown(0))
        {
            isDragging = true;
            startTouch = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
            Reset();
        }
        #endregion
        #region Mobile Inputs
        if (Input.touches.Length > 0)
        {
            if (Input.touches[0].phase == TouchPhase.Began)
            {
                isDragging = true;
                startTouch = Input.touches[0].position;
            }
            else if (Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled)
            {
                isDragging = false;
                Reset();
            }
        }
        #endregion
        //calculate the distance
        swipeDelta = Vector2.zero;
        if (isDragging)
        {
            if (Input.touches.Length > 0)
            {
                swipeDelta = Input.touches[0].position - startTouch;
            }
            else if (Input.GetMouseButton(0))
            {
                swipeDelta = (Vector2)Input.mousePosition - startTouch;
            }
        }
        //Did we cross the Deadzone

        if (swipeDelta.magnitude > 1f)
        {
            //which direction
            float x = swipeDelta.x;
            float y = swipeDelta.y;
            if (Mathf.Abs(x) > Mathf.Abs(y))
            {
                //Left or right
                if (x < 0)
                    obj.transform.position += Vector3.left * speed;
                //swipeLeft = true;
                else
                    obj.transform.position += Vector3.right * speed;
                //swipeRight = true;
            }
            else
            {
                //Up or down
                if (y < 0)
                    obj.transform.position += Vector3.down * speed;
                //swipeDown = true;
                else
                    obj.transform.position += Vector3.up * speed;
                //swipeUp = true;
            }
        }

    }
    private void Reset()
    {
        startTouch = swipeDelta = Vector2.zero;
    }
}

