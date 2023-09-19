using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class Warrior : Character
{
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
        
    }

    // Update is called once per frame
    void Update()
    {
        
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