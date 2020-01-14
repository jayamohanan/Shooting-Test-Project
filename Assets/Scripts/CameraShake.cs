using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Attached to gun, not camera for shaking when fired
public class CameraShake : MonoBehaviour
{
    Vector3 originalPosition;
    private void Awake()
    {
         originalPosition= transform.localPosition;
    }
    public IEnumerator ShakeCamera(float duration, float strength)
    {
        float elapsedTime = 0.0f;
        while (elapsedTime < duration)
        {
            float z = Random.Range(-0.1f, 0.1f) * strength / 1000000.0f;
            transform.localPosition = new Vector3(originalPosition.x, originalPosition.y, z);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.localPosition = originalPosition;
    }
}
