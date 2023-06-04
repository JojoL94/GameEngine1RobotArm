using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyObjectManager : MonoBehaviour
{
    [SerializeField] private GameObject productionBand;
    [SerializeField] private GameObject endBand1;
    [SerializeField] private GameObject endBand2;
    [SerializeField] private GameObject endBand3;
    [SerializeField] private Transform defaultPosition;
    [SerializeField] private Transform hebePosition;
    [SerializeField] private Transform fallPosition;
    [SerializeField] private Transform fingerHebePosition;
    [SerializeField] float distance = 0.1f;
    public Transform target;
    public GameObject produkt;
    public bool isPickedUp = false;
    
    
    private bool processProduct = false;

    // Update is called once per frame
    
    
    
    void Update()
    {
        if (productionBand.GetComponent<MyProductionSpawner>().productIsWaiting && !processProduct)
        {
            processProduct = true;
        }
        else
        {
            target = defaultPosition;
        }

        if (processProduct)
        {
            if (!isPickedUp)
            {
                target = productionBand.GetComponent<MyProductionSpawner>().endPoint;
            }
            else
            {
                target = hebePosition;
                Vector3 targetPosition = fingerHebePosition.position + (-fingerHebePosition.forward * distance);
                produkt.transform.position = Vector3.Lerp(produkt.transform.position, targetPosition, Time.deltaTime * 2f);
                produkt.transform.LookAt(hebePosition);
            }
        }
    }
}
