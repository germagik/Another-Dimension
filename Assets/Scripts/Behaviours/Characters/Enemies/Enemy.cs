using System;
using Unity.VisualScripting;
using UnityEngine;
using Utils;

public class Enemy : Character
{
	public DropProbability[] drops = new DropProbability[0];
	protected Warrior warrior;

	protected override void Start()
	{
		base.Start();
		warrior = Warrior.Instance();
	}

	protected override void Update()
	{
		base.Update();
		if(warrior.IsDestroyed())
		{
			return;
		}
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
		PerformAttack(0);
	}

	void OnDisable()
	{
		if(!gameObject.scene.isLoaded) return;
		DropProbability probability = Probability.Resolve(drops) as DropProbability;
		if(probability != null)
		{
			Instantiate(probability.item, transform.position, probability.item.transform.rotation);
		}
	}
}

[Serializable]
public class DropProbability : Probability
{
	public Item item;
}
