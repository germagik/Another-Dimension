using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;


public class Warrior : Character
{
    private static Warrior instance;
    public static Warrior Instance()
    {
        return instance;
    }
    protected float reaction = 0.15f;
    [SerializeField] protected AttackConfiguration[] attacks = new AttackConfiguration[0];
    protected ClosestEnemy closest;
    protected float attacking;
    protected float attackingTimer;
    [SerializeField] protected float attackSlowdownTime = 2f;
    [SerializeField] protected float attackSlowdownFactor = 2f;
    public void CheckEnemyAtDistance(Enemy enemy, float distance)
    {
        if (distance <= range)
        {
            if (closest == null || distance < closest.distance)
            {
                closest = new ClosestEnemy(enemy, distance);
            }
        }
        else if (closest != null && closest.enemy == enemy)
        {
            closest = null;
        }
    }
    protected override void Reset()
    {
        base.Reset();
        Attack[] attacksComponents = GetComponentsInChildren<Attack>();
        AttackConfiguration[] previousAttacks = attacks;
        attacks = new AttackConfiguration[attacksComponents.Length];
        for (int i = 0; i < attacksComponents.Length; i++)
        {
            Attack attack = attacksComponents[i];
            AttackConfiguration previousAttack = previousAttacks.ElementAtOrDefault(i);
            if(previousAttack != null)
            {
                previousAttack.attack = attack;
                attacks[i] = previousAttack;
            } else
            {
                attacks[i] = new AttackConfiguration(attack);
            }
        }
    }
    
    void Awake()
    {
        instance = this;
    }

    void Update()
    {
        Move();
        Attack();
    }

    void Move()
    {
        float axisX = Input.GetAxis("Horizontal");
        float axisY = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(axisX, axisY);
        if (direction.magnitude >= reaction) {
            if (attackingTimer > 0)
            {
                MoveTo(direction, attackSlowdownFactor);
                if (closest == null) {
                    PointTo(direction);
                }
            }
            else
            {
                MoveTo(direction);
                PointTo(direction);
            }
        }
    }

    void Attack()
    {
        if (attackingTimer > 0)
        {
            attackingTimer -= Time.deltaTime;
            Aim();
        }
        else
        {
            attackingTimer = 0;
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            attackingTimer = attackSlowdownTime;
            Aim();
            attacks[0].attack.Perform();
        }
    }
    void Aim()
    {
        if (closest != null)
        {
            PointTo(closest.enemy.transform.position - transform.position);
        }
    }
}


public enum AimingStrategy
{
    Closest, Stronger, Healthier
}

[Serializable]
public class AttackConfiguration
{
    public AimingStrategy aimingStrategy;
    public Attack attack;

    public AttackConfiguration(Attack newAttack, AimingStrategy newAimingStrategy = AimingStrategy.Closest) {
        attack = newAttack;
        aimingStrategy = newAimingStrategy;
    }
}

public class ClosestEnemy
{
    public Enemy enemy;
    public float distance;
    public ClosestEnemy(Enemy newEnemy, float newDistance)
    {
        enemy = newEnemy;
        distance = newDistance;
    }
}