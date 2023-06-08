using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyProductBandMover : MonoBehaviour
{
    private Transform ziel;
    public float geschwindigkeit = 1f;

    private List<Transform> beruehrteObjekte = new List<Transform>();

    private void Start()
    {
        ziel = transform.GetChild(1);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Product"))
        {
            beruehrteObjekte.Add(other.transform);
        }
    }

    private void Update()
    {
        foreach (Transform objekt in beruehrteObjekte)
        {
            float dist = Vector3.Distance(objekt.position, ziel.transform.position);
            if (dist > 0.3f)
            {
                objekt.position = Vector3.MoveTowards(objekt.position, ziel.position, geschwindigkeit * Time.deltaTime);
            }
            else
            {
                beruehrteObjekte.Remove(objekt);
            }

        }
    }
}
