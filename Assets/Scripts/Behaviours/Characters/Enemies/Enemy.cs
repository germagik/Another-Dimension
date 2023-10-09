using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using Utils;

public class Enemy : Character
{
    public DropProbability[] drops = new DropProbability[0];
    private Warrior warrior;
    public override void Start()
    {
        base.Start();
        warrior = Warrior.Instance(); 
    }
    void Update()
    {
       Move(); 
    }
    void Move()
    {
        Vector3 direction = warrior.transform.position - transform.position;
        warrior.CheckEnemyAtDistance(this, direction.magnitude);
        PointTo(direction);
        if(direction.magnitude > range)
        {
            MoveTo(direction);
        } 
        else
        {
            Attack();
        }      
    }
    void Attack()
    {
        attacks[0].Perform("Player");
    }
    
}

[Serializable]
public class DropProbability : Probability
{
    public Item item;
}
