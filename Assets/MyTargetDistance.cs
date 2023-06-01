using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyTargetDistance : MonoBehaviour
{
    public Transform target; // Das Ziel, dem gefolgt werden soll
    public float distance = 1f; // Der gewünschte Abstand

    void Update()
    {
        Vector3 targetPosition = target.position + (-target.forward * distance);
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * 5f);
        transform.LookAt(target.position);
    }
}
