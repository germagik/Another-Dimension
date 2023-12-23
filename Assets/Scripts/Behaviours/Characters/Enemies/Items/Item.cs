using UnityEngine;
using Utils;

public abstract class Item : MonoBehaviour
{
	[SerializeField] protected float factor = 0.1f;
	protected bool taken = false;
	protected Animator animator;
	protected AudioSource audioSource;

	protected virtual void Start()
	{
		audioSource = GetComponent<AudioSource>();
		animator = GetComponent<Animator>();
	}

	protected virtual void Update()
	{
		if (taken && !audioSource.isPlaying)
		{
			Destroy(gameObject);
		}
	}

	public void OnTriggerEnter2D(Collider2D collision)
	{
		if(!taken && collision.gameObject.tag == KnownTags.Player.ToString())
		{
			Effect(collision);
			animator.SetTrigger("Taken");
			audioSource.Play();
			taken = true;
		}
	}
	protected abstract void Effect(Collider2D collision);
}
