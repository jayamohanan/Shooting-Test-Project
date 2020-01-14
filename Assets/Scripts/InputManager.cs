using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public float cameraTurnSpeed = 1f;
    bool isDragging;
    private Vector2 startPosition, dragDistance, deltaDragDistance;
    void Update()
    {
        #region Standalone Inputs
        if (Input.GetMouseButtonDown(0))
        {
            isDragging = true;
            startPosition = Input.mousePosition;
        }
        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }
        #endregion
        #region Touch Inputs
        if (Input.touches.Length>0)
        {
            if (Input.touches[0].phase == TouchPhase.Began)
            {
                isDragging = true;
                startPosition = Input.touches[0].position;
            }
            if (Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled)
            {
                isDragging = false;
            }
        }
        #endregion
        if (isDragging)//Restricted to Horizontal scrolling
        {
            dragDistance = (Vector2)Input.mousePosition - startPosition;
            deltaDragDistance = dragDistance - deltaDragDistance;
            if (Mathf.Abs(deltaDragDistance.x) > 1.5f)
            {
                if (deltaDragDistance.x > 0)
                    transform.Rotate(Vector3.up * cameraTurnSpeed);
                else
                    transform.Rotate(Vector3.down * cameraTurnSpeed);
            }
            else
                Debug.Log("Not rotating");
            deltaDragDistance = dragDistance;
        }
    }
}
