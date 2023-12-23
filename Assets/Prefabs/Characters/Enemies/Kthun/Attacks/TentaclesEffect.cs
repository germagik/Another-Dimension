using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class TentaclesEffect : MonoBehaviour
{
	public float damage = 10f;
	private bool hitten = false;
	public void End()
	{
		Destroy(gameObject);
	}

	public void OnTriggerEnter2D(Collider2D collision)
    {
        if (!hitten && collision.gameObject.tag == KnownTags.Player.ToString())
        {
            Character character = collision.gameObject.GetComponent<Character>();
            character.ReceiveDamage(damage);
			hitten = true;
        }
    }
}
