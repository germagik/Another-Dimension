using UnityEngine;
using Utils;

public class PowerUpItem : Item
{
	protected override void Effect(Collider2D collision)
	{
		collision.gameObject.GetComponent<Warrior>().PowerUp(factor);
	}
}
