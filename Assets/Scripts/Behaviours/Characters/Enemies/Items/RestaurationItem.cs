using UnityEngine;
using Utils;

public class RestaurationItem : Item
{
	protected override void Effect(Collider2D collision)
	{
		collision.gameObject.GetComponent<Warrior>().Heal(factor);
	}
}
