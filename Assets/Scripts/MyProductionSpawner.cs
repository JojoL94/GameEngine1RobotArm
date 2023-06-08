using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MyProductionSpawner : MonoBehaviour
{
    [SerializeField] private GameObject robotArm;
    public GameObject produktPrefab;
    public float speed = 1.0f;
    public GameObject produkt;
    public Transform endPoint;
    public bool productIsWaiting = false;

    public bool somethingHasSpawned = false;
    // Start is called before the first frame update
    void Start()
    {
        produkt =  Instantiate(produktPrefab, transform.GetChild(0).transform.position, transform.rotation);
        endPoint = transform.GetChild(1).transform;

    }

    // Update is called once per frame
    void Update()
    {
        if (produkt != null)
        {
            float dist = Vector3.Distance(endPoint.position, produkt.transform.position);
            if (dist > 0.3f)
            {
                productIsWaiting = false;
                Debug.Log("Product is moving");
                var step =  speed * Time.deltaTime; // calculate distance to move
                produkt.transform.position = Vector3.MoveTowards(produkt.transform.position, transform.GetChild(1).transform.position, step);
            }
            else
            {
                productIsWaiting = true;
            }
        }
        else
        {
            produkt =  Instantiate(produktPrefab, transform.GetChild(0).transform.position, transform.rotation);

        }   
    }
}
