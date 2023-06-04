using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyFingerHandler : MonoBehaviour
{
    public bool isTouchingSomething;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Finger is Touching Something");
        isTouchingSomething = true;
        other.attachedRigidbody.useGravity = false;
        GetComponent<MyTargetDistance>().product = other.gameObject;
        GetComponent<MyTargetDistance>().isFingerPosition = true;
        transform.root.GetComponent<MyObjectManager>().produkt = other.gameObject;
        transform.root.GetComponent<MyObjectManager>().isPickedUp = true;
    }
}
