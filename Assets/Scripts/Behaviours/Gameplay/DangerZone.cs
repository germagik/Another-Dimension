using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DangerZone : MonoBehaviour
{
	public GameObject effect;
    public void Effect()
	{
		Instantiate(effect, transform.position, gameObject.transform.rotation);
		Destroy(gameObject);
	}
}
