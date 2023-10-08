using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Utils;

public class Enemy : Character
{
    public DropProbability[] drops = new DropProbability[0];
    protected Warrior warrior;

    void Start()
    {
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
        if (direction.magnitude > range)
        {
            MoveTo(direction);
            PointTo(direction);
        } else
        {
            Attack();
        }
    }

    void Attack()
    {

    }
}

[Serializable]
public class DropProbability : Probability
{
    public Item item;
}
