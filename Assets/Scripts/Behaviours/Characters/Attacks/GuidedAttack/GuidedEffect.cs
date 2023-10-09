using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Timeline;

public class GuidedEffect : Effect
{
   private float speed = 15f;
    void Awake()
    {
       
    }

    void Update()
    {
        BulletDirection();
        
    }
    void BulletDirection()
    {   
        transform.position -= transform.up * speed * Time.deltaTime;

    }
}
   
