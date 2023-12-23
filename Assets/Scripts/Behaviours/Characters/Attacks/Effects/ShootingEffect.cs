using UnityEngine;

public class ShootingEffect : Effect
{
    [SerializeField] protected float speed = 50f;
    [SerializeField] protected float range = 20f;
    protected Vector3 lastPosition;
    protected float distance = 0f;

    protected override void Start()
	{
		base.Start();
		lastPosition = transform.position;
	}

    void Update()
    {
        transform.position -= transform.up * speed * Time.deltaTime;
        Vector3 deltaPosition = transform.position - lastPosition;
        lastPosition = transform.position;
        distance += deltaPosition.magnitude;
        if (distance >= range)
        {
            Destroy(gameObject);
        }
    }
}
