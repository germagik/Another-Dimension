using UnityEngine;
using Utils;

public class Attack : MonoBehaviour
{
	[SerializeField] protected Effect effect;
	[SerializeField] protected float damage = 20f;
	[SerializeField] protected float cooldown = 1f;
	[SerializeField] protected float duration = 0.5f;
	[SerializeField] protected float slowdownFactor = 4f;
	protected float cooldownTimer = 0f;
	protected float durationTimer = 0f;
	protected Character triggerer;
	protected AudioSource audioSource;

	public bool IsReady {
		get
		{
			return cooldownTimer <= 0;
		}
	}
	public float SlowdownFactor
	{
		get
		{
			return slowdownFactor;
		}
	}
	public bool IsPerforming {
		get
		{
			return durationTimer > 0;
		}
	}
	protected void Start()
	{
		triggerer = GetComponentInParent<Character>();
		audioSource = GetComponent<AudioSource>();
	}

	protected void Update()
	{
		CheckReady();
		CheckDuration();
	}
	public void Perform(KnownTags targetTag)
	{
		if(IsReady)
		{
			Effect newEffect = Instantiate(effect, transform.position, transform.rotation);
			newEffect.Initialize(triggerer, targetTag, damage);
			newEffect.gameObject.SetActive(true);
			audioSource.Play();
			cooldownTimer = cooldown;
			durationTimer = duration;
		}
	}
	protected void CheckReady()
	{
		if(!IsReady)
		{
			cooldownTimer -= Time.deltaTime;
			if(cooldownTimer < 0)
			{
				cooldownTimer = 0f;
			}
		}
	}

	protected void CheckDuration()
	{
		if (IsPerforming)
		{
			durationTimer -= Time.deltaTime;
			if (durationTimer < 0)
			{
				durationTimer = 0f;
			}
		}
	}

}
