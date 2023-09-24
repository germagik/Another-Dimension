using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class GuidedEffect : Effect
{
   
    void Update()
    {
        transform.position -= transform.up * Time.deltaTime;
    }
}
