using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantEffect : Effect
{
    public float duration = 0.5f;
    void Update()
    {
        duration -= Time.deltaTime;
        if (duration <= 0)
        {
           Destroy(gameObject);
        }
    }
}
