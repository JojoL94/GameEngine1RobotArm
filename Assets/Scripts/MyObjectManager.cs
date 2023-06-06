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
    public bool isHebePosition = false;
    private int chooseBandNr = 0;
    private bool processProduct = false;
    private bool rotating = false;


    void Update()
    {
        if (productionBand.GetComponent<MyProductionSpawner>().productIsWaiting && !processProduct)
        {
            processProduct = true;
            chooseBandNr = 2/*Random.Range(1, 3)*/;
            rotating = true;
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
                leftFingerTarget.GetComponent<MyTargetDistance>().target = target;
            }
            else if (isPickedUp && !isHebePosition)
            {
                target = hebePosition;
                
                    if (chooseBandNr == 2)
                    {
                        Vector3 targetPosition = fingerHebePosition.position + (-fingerHebePosition.forward * distance);
                        produkt.transform.position = Vector3.Lerp(produkt.transform.position, targetPosition, Time.deltaTime * 5f);
                        produkt.transform.rotation = hebePosition.root.transform.rotation;
                        if (rotating)
                        {
                            Vector3 to = new Vector3(0, 180, 0);
                            if (Vector3.Distance(transform.eulerAngles, to) > 1f)
                            {
                                hebePosition.root.transform.eulerAngles = Vector3.Lerp(hebePosition.root.transform.rotation.eulerAngles, to, 0.5f * Time.deltaTime);
                            }
                            else
                            {
                                hebePosition.root.transform.eulerAngles = to;
                                rotating = false;
                            }
                        }
                    }
            }
        }
    }
}
