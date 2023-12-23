using UnityEngine;
using Utils;

public abstract class Effect : MonoBehaviour
{
	[SerializeField] protected float animationSpeed = 1f;
    protected Character triggerer;
    protected KnownTags targetTag;
	protected Animator animator;
    protected float damage;

	protected virtual void Start()
	{
		animator = GetComponent<Animator>();
		animator.SetFloat(EffectAnimationParameters.Speed.ToString(), animationSpeed);
	}
    public virtual void Initialize(Character initialTriggerer, KnownTags initialTargetTag, float initialDamage)
    {
        targetTag = initialTargetTag;
        triggerer = initialTriggerer;
        damage = initialDamage;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == targetTag.ToString())
        {
            Character character = collision.gameObject.GetComponent<Character>();
            character.ReceiveDamageFrom(triggerer, damage);
            Destroy(gameObject);
        }
    }
}
