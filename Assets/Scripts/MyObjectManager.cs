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
    private float turningRate = 60f; 
    private Quaternion _targetRotation;
    // Call this when you want to turn the object smoothly.

    void Update()
    {
        if (productionBand.GetComponent<MyProductionSpawner>().productIsWaiting && !processProduct)
        {
            processProduct = true;
            chooseBandNr = Random.Range(1, 4);
            
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
                CheckForHit();
            }
            else if (isPickedUp)
            {
                Debug.Log(chooseBandNr);
                if (chooseBandNr == 1)
                {
                    if (rotating)
                    {
                        target = hebePosition;
                        Quaternion _targetRotation = endBand1.transform.rotation;
                        if (Vector3.Distance(hebePosition.root.transform.eulerAngles, endBand1.transform.eulerAngles) > 1f)
                        {
                            
                            hebePosition.root.transform.rotation = Quaternion.RotateTowards( hebePosition.root.transform.rotation, _targetRotation, turningRate * Time.deltaTime);
                        }
                        else
                        {
                            hebePosition.root.transform.eulerAngles = endBand1.transform.eulerAngles;
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
                            if (Vector3.Distance(produkt.transform.position, hebePosition.position) > 1f)
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
                            if (Vector3.Distance(produkt.transform.position, fallPosition.position) > 1f)
                            {
                                fallPosition.transform.position = endBand1.transform.GetChild(0).transform.position +
                                                                  new Vector3(0, 2, 0);
                                target = fallPosition;
                                produkt.transform.position = Vector3.Lerp(produkt.transform.position, fallPosition.position,
                                    Time.deltaTime * 2f);
                                produkt.transform.rotation = hebePosition.root.transform.rotation;
                            }
                            else
                            {
                                produkt.transform.position = fallPosition.position;
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
                } else if (chooseBandNr == 2)
                {
                    if (rotating)
                    {
                        target = hebePosition;
                        Quaternion _targetRotation = endBand2.transform.rotation;
                        if (Vector3.Distance(hebePosition.root.transform.eulerAngles, endBand2.transform.eulerAngles) > 1f)
                        {
                            
                            hebePosition.root.transform.rotation = Quaternion.RotateTowards( hebePosition.root.transform.rotation, _targetRotation, turningRate * Time.deltaTime);
                        }
                        else
                        {
                            hebePosition.root.transform.eulerAngles = endBand2.transform.eulerAngles;
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
                            if (Vector3.Distance(produkt.transform.position, hebePosition.position) > 1f)
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
                            if (Vector3.Distance(produkt.transform.position, fallPosition.position) > 1f)
                            {
                                fallPosition.transform.position = endBand2.transform.GetChild(0).transform.position +
                                                                  new Vector3(0, 2, 0);
                                target = fallPosition;
                                produkt.transform.position = Vector3.Lerp(produkt.transform.position, fallPosition.position,
                                    Time.deltaTime * 2f);
                                produkt.transform.rotation = hebePosition.root.transform.rotation;
                            }
                            else
                            {
                                produkt.transform.position = fallPosition.position;
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
                } else if (chooseBandNr == 3)
                {
                    if (rotating)
                    {
                        Quaternion _targetRotation = endBand3.transform.rotation;
                        Vector3 to = new Vector3(0, 90, 0);
                        if (Vector3.Distance(hebePosition.root.transform.eulerAngles, endBand3.transform.eulerAngles) > 1f)
                        {
                            
                            hebePosition.root.transform.rotation = Quaternion.RotateTowards( hebePosition.root.transform.rotation, _targetRotation, turningRate * Time.deltaTime);
                        }
                        else
                        {
                            hebePosition.root.transform.eulerAngles = endBand3.transform.eulerAngles;
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
                            if (Vector3.Distance(produkt.transform.position, hebePosition.position) > 1f)
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
                            if (Vector3.Distance(produkt.transform.position, fallPosition.position) > 1f)
                            {
                                fallPosition.transform.position = endBand3.transform.GetChild(0).transform.position +
                                                                  new Vector3(0, 2, 0);
                                target = fallPosition;
                                produkt.transform.position = Vector3.Lerp(produkt.transform.position, fallPosition.position,
                                    Time.deltaTime * 2f);
                                produkt.transform.rotation = hebePosition.root.transform.rotation;
                            }
                            else
                            {
                                produkt.transform.position = fallPosition.position;
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
            Quaternion _targetRotation = productionBand.transform.rotation;
            if (Vector3.Distance(hebePosition.root.transform.eulerAngles, productionBand.transform.eulerAngles) > 1f)
            {
                            
                hebePosition.root.transform.rotation = Quaternion.RotateTowards( hebePosition.root.transform.rotation, _targetRotation, turningRate * Time.deltaTime);
            }
            else
            {
                hebePosition.root.transform.eulerAngles = productionBand.transform.eulerAngles;
                isReset = false;
            }

        }
        
    }
    public GameObject raycastObject;

    void CheckForHit(){

        RaycastHit objectHit;
        Vector3 dup = raycastObject.transform.TransformDirection(Vector3.up);
        Debug.DrawRay(raycastObject.transform.position, dup * 5, Color.green);
        if (Physics.Raycast(raycastObject.transform.position, dup, out objectHit, 5))
        {
            //do something if hit object ie
            if(objectHit.transform.tag =="Product"){
                target = productionBand.GetComponent<MyProductionSpawner>().produkt.transform;
            }
        }
    }
}