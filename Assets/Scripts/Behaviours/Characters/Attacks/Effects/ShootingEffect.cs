using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ShootingEffect : Effect
{
    public float speed = 15f;
    public float range = 20f;
    private Vector3 lastPosition;
    private float distance = 0f;
    void Start()
    {
        lastPosition = transform.position;
    }
    void Update()
    {
        transform.position -= transform.up * speed * Time.deltaTime; 
        Vector3 deltaPosition = transform.position - lastPosition;
        distance += deltaPosition.magnitude;
        if(distance >= range)
        {
            Destroy(gameObject);
        }
        lastPosition = transform.position;
    }
}