using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class myMatchRotateWithTargetHandler : MonoBehaviour
{
    [SerializeField] private Transform targetHandler;

    // Update is called once per frame
    void Update()
    {
        transform.rotation = targetHandler.transform.rotation;
    }
}
