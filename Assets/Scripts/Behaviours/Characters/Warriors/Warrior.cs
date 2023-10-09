using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using System.Runtime.CompilerServices;
using UnityEditor.Tilemaps;
using UnityEngine.UIElements;



public class Warrior : Character
{ 
    private static Warrior instance;
    public Enemy closestEnemy;
    private float closestDistance;
    private float attackingTimer;
    public float attackSlowdownTime = 2f;
    public float attackSlowdownFactor = 2f;
    void Awake()
    {
        instance = this;
       
    }
    public static Warrior Instance()
    {
        return instance;
    }
    // Update is called once per frame
    void Update()
    {
        Move();
        Attack();
    } 
    void Move()
    {
        float axisX = Input.GetAxis("Horizontal");
        float axisY = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(axisX,axisY,0);
        if (attackingTimer > 0)
        {
            MoveTo(direction, attackSlowdownFactor);
            if (closestEnemy == null)
            {
                PointTo(direction);
            }
        }
        else 
        {
            MoveTo(direction);
            PointTo(direction);  
        }     
    }  
    public void CheckEnemyAtDistance(Enemy enemy, float distance)
    {
        if (distance <= range)
        {
            if (closestEnemy == null || distance < closestDistance)
            {
                closestEnemy = enemy;
                closestDistance = distance;
            }
        }
        else if (closestEnemy == enemy)
        {
            closestEnemy = null;
            closestDistance = 0;
        }
    }
    void Attack()
    {
        if(attackingTimer > 0)
        {
            attackingTimer -= Time.deltaTime;
            Aim();
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            attackingTimer = attackSlowdownTime;
            Aim();
            attacks[0].Perform("Enemies");
        }
    }
    void Aim()
    {
        if (closestEnemy != null)
        {
            PointTo(closestEnemy.transform.position - transform.position);
        }
    }
}