using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(MeshRenderer))]
public class Target : MonoBehaviour
{
    public event Action<GameObject> TargetInvisible; 
    private void OnBecameInvisible()
    {
        gameObject.SetActive(false);
        TargetInvisible.Invoke(gameObject);
    }
}
