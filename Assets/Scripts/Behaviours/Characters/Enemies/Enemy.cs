using System;
using Unity.VisualScripting;
using UnityEngine;
using Utils;

public class Enemy : Character
{
    public DropProbability[] drops = new DropProbability[0];
    private Warrior warrior;
    private Vector3 distance;
    public void Start()
    {
        warrior = Warrior.Instance(); 
    }
    void Update()
    {
        if (warrior.IsDestroyed())
        {
            return;
        }
        Move();
        Attack();
    }
    void Move()
    {
        distance = warrior.transform.position - transform.position;
        warrior.CheckEnemyAtDistance(this, distance.magnitude);
        PointTo(distance);
        if (distance.magnitude > range)
        {
            MoveTo(distance);
        }
    }
    void Attack()
    {
        if (distance.magnitude <= range) {
            attacks[0].Perform("Player");
        }
    }
    
}

[Serializable]
public class DropProbability : Probability
{
    public Item item;
}
