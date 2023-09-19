using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class Enemy : Character
{
    public DropProbability[] drops = new DropProbability[0];
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

[Serializable]
public class DropProbability : Probability
{
    public Item item;
}
