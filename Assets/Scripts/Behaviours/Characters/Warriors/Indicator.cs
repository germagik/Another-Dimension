using UnityEngine;

public class Indicator : MonoBehaviour
{
	private Enemy target;

	public virtual void Initialize(Enemy newTarget)
	{
		target = newTarget;
	}
    void Update()
    {
		transform.up = -(target.transform.position - transform.position).normalized;
    }
}
