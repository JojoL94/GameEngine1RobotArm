using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MyProductionSpawner : MonoBehaviour
{
    [SerializeField] private GameObject robotArm;
    public GameObject produktPrefab;
    public float speed = 1.0f;
    private GameObject produkt;
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(produktPrefab, transform.GetChild(0).transform.position, transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        if (produkt != null)
        {
            float dist = Vector3.Distance(transform.GetChild(1).transform.position, produkt.transform.position);
            if (dist < 0.1f)
            {
                var step =  speed * Time.deltaTime; // calculate distance to move
                produkt.transform.position = Vector3.MoveTowards(produkt.transform.position, transform.GetChild(1).transform.position, step);
            }
        }   
    }

    private void OnTriggerEnter(Collider other)
    {
        produkt = other.GameObject();
    }
}
