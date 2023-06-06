using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyTargetDistance : MonoBehaviour
{
    public Transform target; // Das Ziel, dem gefolgt werden soll
    public float distance = 1f; // Der gewünschte Abstand
    public bool isFingerPosition = false;
    public GameObject product;
    [SerializeField] private bool leftFinger;
    [SerializeField] private Transform defaultFingerPosition;
    [SerializeField] private float speed = 1f;
    private bool isReset = false;
    void Update()
    {
        isReset = transform.root.GetComponent<MyObjectManager>().isReset;
        if (isReset == true)
        {
            isFingerPosition = false;
        }
        if (isFingerPosition)
        {
            if (leftFinger)
            {
                target = product.transform.GetChild(0).transform;
            }
            else
            {
                target = product.transform.GetChild(1).transform;
            }
            Vector3 targetPosition = target.position + (-target.forward * distance);
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * speed * 2);
            transform.LookAt(targetPosition);
        }
        else
        {
            if (defaultFingerPosition != null)
            {
                target = defaultFingerPosition;
            }
            else
            {
                target = transform.root.GetComponent<MyObjectManager>().target;
            }
            Vector3 targetPosition = target.position + (-target.forward * distance);
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * speed);
            transform.LookAt(targetPosition);
        }

    }
}
