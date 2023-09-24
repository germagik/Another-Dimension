using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;



public class Warrior : Character
{ 
    public float speed = 10f;

    private Part[] parts;
    private Vector3 direction;
    
    public AttackConfiguration[] attacks = new AttackConfiguration[0];
    protected virtual void Reset()
    {
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
            } else {
                attacks[i] = new AttackConfiguration(attack);
            }
        }
    }
    void OnValidate()
    {
        Reset();
    }
    // Start is called before the first frame update
    void Start()
    {
       parts = GetComponentsInChildren<Part>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Aim();
        Attack();
    } 
    void Move()
    {
        float axisX = Input.GetAxis("Horizontal");
        float axisY = Input.GetAxis("Vertical");
        direction = new Vector3(axisX,axisY,0);
        if (direction.magnitude > 1)
        {
            direction = direction.normalized;
        }  
        transform.position += direction * speed *Time.deltaTime;

    }  
    void Aim()
    {
        if (direction.magnitude !=0 )
        {
            for (int i = 0; i < parts.Length; i++)
            {
                parts[i].transform.up = -direction;
            }
        }      
        
    }
    void Attack()
    {
        if (Input.GetKeyUp(KeyCode.Z))
        {
            attacks[0].attack.Perform();
        }
    }
}


public enum AimingStrategy {
    Closest, Stronger, Healthier
}

[Serializable]
public class AttackConfiguration {
    public AimingStrategy aimingStrategy;
    public Attack attack;

    public AttackConfiguration(Attack newAttack, AimingStrategy newAimingStrategy = AimingStrategy.Closest) {
        attack = newAttack;
        aimingStrategy = newAimingStrategy;
    }
}