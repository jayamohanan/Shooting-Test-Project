using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraMovement : MonoBehaviour
{
    public float cameraTurnFactor=1;
    private Vector3 oldMousePosition;
    private float deltaMousePosition;
    new private Camera camera;

    void Start()
    {
        camera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetMouseButtonDown(0))
        //{
        //    oldMousePosition = Input.mousePosition;
        //}
        //deltaMousePositionX = (Input.mousePosition - oldMousePosition).x;
        //if (Input.GetMouseButton(0) && Mathf.Abs(deltaMousePositionX)>2f)
        //{
        //    camera.transform.Rotate(0, -deltaMousePositionX*cameraTurnFactor,0);
        //}
        //if (Input.GetMouseButton(0))
        //{
        //    Debug.Log("Mouse Down");
        //    if (Input.mousePosition.x > oldMousePosition.x)
        //    {
        //        camera.transform.Rotate(0, -deltaMousePosition * cameraTurnFactor, 0);
        //    }
        //    else
        //    {
        //        Debug.Log("");
        //    }
        //    oldMousePosition = Input.mousePosition;
        //}
    }
    private void OnMouseDrag()
    {
        Debug.Log("Mouse dragging");
        //deltaMousePosition = (Input.mousePosition - oldMousePosition).x;
        camera.transform.Rotate(0,-cameraTurnFactor, 0);
        //oldMousePosition = Input.mousePosition;
    }
}
