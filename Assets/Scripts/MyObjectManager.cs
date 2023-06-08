using System.Collections;
using System.Collections.Generic;
using System.Net;
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
    [SerializeField] private GameObject leftFingerTarget;
    [SerializeField] private GameObject rightFingerTarget;
    public Transform target;
    public GameObject produkt;
    public bool isPickedUp = false;
    private bool fallenLassen = false;
    private int chooseBandNr = 0;
    private bool processProduct = false;
    private bool rotating = false;
    public bool isReset = false;

    void Update()
    {
        if (productionBand.GetComponent<MyProductionSpawner>().productIsWaiting && !processProduct)
        {
            processProduct = true;
            chooseBandNr = 2 /*Random.Range(1, 3)*/;
        }
        else
        {
            target = defaultPosition;
        }

        if (processProduct && !isReset)
        {
            if (!isPickedUp)
            {
                target = productionBand.GetComponent<MyProductionSpawner>().endPoint;
                leftFingerTarget.GetComponent<MyTargetDistance>().target = target;
            }
            else if (isPickedUp)
            {
                if (chooseBandNr == 2)
                {
                    if (rotating)
                    {
                        target = hebePosition;
                        Vector3 to = new Vector3(0, 180, 0);
                        if (Vector3.Distance(transform.eulerAngles, to) > 1f)
                        {
                            hebePosition.root.transform.eulerAngles = Vector3.Lerp(
                                hebePosition.root.transform.rotation.eulerAngles, to, 0.6f * Time.deltaTime);
                        }
                        else
                        {
                            hebePosition.root.transform.eulerAngles = to;
                            rotating = false;
                            fallenLassen = true;
                        }

                        Vector3 targetPosition = fingerHebePosition.position + (-fingerHebePosition.forward * distance);
                        produkt.transform.position = targetPosition;
                        produkt.transform.rotation = hebePosition.root.transform.rotation;
                    }
                    else
                    {
                        if (!fallenLassen)
                        {
                            target = hebePosition;
                            if (Vector3.Distance(produkt.transform.position, hebePosition.position) > 0.5f)
                            {
                                target = hebePosition;
                                produkt.transform.position = Vector3.Lerp(produkt.transform.position,
                                    hebePosition.position,
                                    Time.deltaTime * 2f);
                            }
                            else
                            {
                                produkt.transform.position = hebePosition.position;
                                rotating = true;
                            }
                        }
                        else
                        {
                            if (Vector3.Distance(produkt.transform.position, fallPosition.position) > 0.5f)
                            {
                                target = fallPosition;
                                Vector3 targetPosition = fallPosition.position + (-fallPosition.forward * distance);
                                produkt.transform.position = Vector3.Lerp(produkt.transform.position, targetPosition,
                                    Time.deltaTime * 1f);
                                produkt.transform.rotation = hebePosition.root.transform.rotation;
                            }
                            else
                            {
                                produkt.GetComponent<Rigidbody>().useGravity = true;
                                productionBand.GetComponent<MyProductionSpawner>().produkt = null;
                                productionBand.GetComponent<MyProductionSpawner>().somethingHasSpawned = false;
                                isPickedUp = false;
                                processProduct = false;
                                fallenLassen = false;
                                isReset = true;

                            }
                        }
                    }
                }
            }
        }
        else
        {
            /*
 
 - processProduct
 - !fallenLassen
 - isPickedUp
 - MyProduktionSpawner Produkt = null;
 - MyFingerHandler isTouching Something = false;
 - MyTargetDistance isFingerPosition = false;
 leftFingerTarget.GetComponent<MyTargetDistance>().target = target;
 target = defaultPosition;
 
 if(other.GetComponent<Rigidbody>() != null)
 isTouchingSomething = true;
 other.attachedRigidbody.useGravity = false;
 GetComponent<MyTargetDistance>().product = other.gameObject;
 GetComponent<MyTargetDistance>().isFingerPosition = true;
 transform.root.GetComponent<MyObjectManager>().produkt = other.gameObject;
 transform.root.GetComponent<MyObjectManager>().isPickedUp = true;

*/
            target = defaultPosition;
            Vector3 to = new Vector3(0, 0, 0);
            if (Vector3.Distance(transform.eulerAngles, to) > 1f)
            {
                hebePosition.root.transform.eulerAngles = Vector3.Lerp(
                    hebePosition.root.transform.rotation.eulerAngles, to, 0.6f * Time.deltaTime);
            }
            else
            {
                hebePosition.root.transform.eulerAngles = to;
                isReset = false;
            }

        }
        
    }
}