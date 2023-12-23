using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Utils;

public class Warrior : Character
{
	private static Warrior instance;
	public static Warrior Instance() => instance;
	[SerializeField] protected float reaction = 0.15f;
	[SerializeField] protected Indicator distantIndicator;
	private Dictionary<Enemy, Indicator> distants = new Dictionary<Enemy, Indicator>();
	private ClosestEnemy closest;
	protected ClosestEnemy Closest
	{
		get
		{
			if(closest != null && closest.enemy.IsDestroyed())
			{
				closest = null;
			}
			return closest;
		}
		set
		{
			closest = value;
		}
	}

	void Awake()
	{
		instance = this;
	}

	protected override void Update()
	{
		base.Update();
		Move();
		Attack();
	}

	public void CheckEnemyAtDistance(Enemy enemy, float distance)
	{
		if(distance <= range)
		{
			if(Closest == null || distance < Closest.distance)
			{
				Closest = new ClosestEnemy(enemy, distance);
			}
		}
		else if(Closest != null && Closest.enemy == enemy)
		{
			Closest = null;
		}
		Vector2 enemyPosition = Camera.main.WorldToScreenPoint(enemy.transform.position);
		if(enemyPosition.x < 0 || enemyPosition.x > Camera.main.pixelWidth ||
			enemyPosition.y < 0 || enemyPosition.y > Camera.main.pixelHeight)
		{
			if(!distants.ContainsKey(enemy))
			{
				Indicator indicator = Instantiate(distantIndicator, transform);
				indicator.Initialize(enemy);
				distants.Add(enemy,indicator);
			}
		}
		else if(distants.ContainsKey(enemy))
		{
			Destroy(distants[enemy].gameObject);
			distants.Remove(enemy);
		}
	}

	void Move()
	{
		float axisX = Input.GetAxis("Horizontal");
		float axisY = Input.GetAxis("Vertical");
		Vector3 direction = new Vector3(axisX, axisY);
		if(direction.magnitude >= reaction)
		{
			if (Input.GetKey(KeyCode.S))
			{
				running = true;
			}
			else
			{
				running = false;
			}
			if(CurrentAttack != null)
			{
				MoveTo(direction);
				if(Closest == null)
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
	}

	void Attack()
	{
		if(CurrentAttack != null)
		{
			Aim();
		}
		if(Input.GetKey(KeyCode.A))
		{
			Aim();
			PerformAttack(0);
		}
	}
	void Aim()
	{
		if(Closest != null)
		{
			PointTo(Closest.enemy.transform.position - transform.position);
		}
	}
	public void PowerUp(float factor)
	{
		power += factor;
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
