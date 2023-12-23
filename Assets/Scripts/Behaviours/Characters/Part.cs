using UnityEngine;
using Utils;

/*
 * Clasificador que simboliza una parte de un personaje que puede girar en su propio eje
 * apuntando en la dirección en que se mueve y ejecuta las animaciones correspondinetes.
 */
public class Part : MonoBehaviour
{
	[SerializeField] protected bool rotates = true;
	[SerializeField] protected float movingAnimationSpeed = 1;
	[SerializeField] protected float slowdownAnimationSpeed = 1;
	[SerializeField] protected float attackingAnimationSpeed = 1;
	[SerializeField] protected bool enableMovingAnimations = true;
	[SerializeField] protected bool enableAttackingAnimations = true;
	protected Animator animator;
	protected Vector3 lastDirection = Vector3.zero;
	protected Character character;

	protected void Start()
	{
		character = GetComponentInParent<Character>();
		animator = GetComponent<Animator>();
	}
	protected void LateUpdate()
	{
		Move();
		Point();
	}
	protected void Move()
	{
		if(enableMovingAnimations)
		{
			animator.SetBool(PartAnimationParameters.IsMoving.ToString(), character.IsMoving);
			if(character.CurrentAttack != null)
			{
				animator.SetFloat(PartAnimationParameters.MovingSpeed.ToString(), slowdownAnimationSpeed);
			}
			else
			{
				animator.SetFloat(PartAnimationParameters.MovingSpeed.ToString(), movingAnimationSpeed);
			}
		}
	}
	protected void Point()
	{
		if(enableMovingAnimations)
		{
			animator.SetFloat(PartAnimationParameters.PointingX.ToString(), -lastDirection.x);
			animator.SetFloat(PartAnimationParameters.PointingY.ToString(), -lastDirection.y);
		}
	}
	public void PointTo(Vector3 direction)
	{
		lastDirection = direction.normalized;
		if(rotates)
		{
			transform.up = lastDirection;
		}
	}

	public void Attack()
	{
		if (enableAttackingAnimations)
		{
			animator.SetFloat(PartAnimationParameters.AttackingSpeed.ToString(), attackingAnimationSpeed);
			animator.SetTrigger(PartAnimationParameters.Attack.ToString());
		}
	}
}
