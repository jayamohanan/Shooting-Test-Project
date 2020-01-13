using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraMovement : MonoBehaviour
{
    public float cameraTurnFactor=1;
    private Vector2 oldMousePosition;
    private Vector2 deltaMousePosition;
    new private Camera camera;

    void Start()
    {
        camera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            oldMousePosition = Input.mousePosition;
        }
        if (Input.GetMouseButton(0))
        {
            deltaMousePosition = (Vector2)Input.mousePosition - oldMousePosition;
            camera.transform.Rotate(0, -deltaMousePosition.x*cameraTurnFactor,0);
        }
    }
}
