using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(MeshRenderer))]
public class Target : MonoBehaviour
{
    public event Action<GameObject> TargetInvisible; //Event tells GameManager to add this back to queue as it is out of cam frustrum
    private void OnBecameInvisible()
    {
        gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;//Reset shot target's velocity for reusing
        gameObject.SetActive(false);
        TargetInvisible.Invoke(gameObject);
    }
}
