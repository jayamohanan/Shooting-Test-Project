using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(Rigidbody))]
public class delete : MonoBehaviour
{
    public RectTransform img;
    public GameObject cube;
    void Start()
    {
        Debug.Log("-Camera.main.transform.position.z " + -Camera.main.transform.position.z);
        Vector3 ab = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height, -Camera.main.transform.position.z));
        float a = ab.y;
        Debug.Log("ab v3 " + ab);
        Debug.Log("a float " + a);
        cube.transform.position = ab;
        img.anchoredPosition = new Vector2(0, 1080);
        //Vector3 wP = Camera.main.ScreenToWorldPoint(new Vector3(0, 2160, 10));
        //float wPY = wP.y; ;
        //cube.transform.position = wP;
        //Debug.Log("wP = "+wP+"  wPY = "+wPY+"  transform.y = "+cube.transform.position.y);

    }

    // Update is called once per frame
    void Update()
    {

    }
}
