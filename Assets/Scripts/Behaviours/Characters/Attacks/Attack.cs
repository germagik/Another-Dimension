using UnityEngine;

public class Attack : MonoBehaviour
{
   public Effect effect;
   public float damage = 5f;
   public float cooldown = 1f;
   private float cooldownTimer = 0f;
   protected void Update()
    {
        if (cooldownTimer > 0)
        {
            cooldownTimer -= Time.deltaTime;
            if (cooldownTimer < 0) {
                cooldownTimer = 0f;
            }
        }
    }
   public void Perform(string targetTag)
   {
      if (cooldownTimer <= 0 && effect != null)
      {
         Effect newEffect = Instantiate(effect, transform.position, transform.rotation);
         newEffect.Initialize(targetTag, damage);
         newEffect.gameObject.SetActive(true);
         cooldownTimer = cooldown;
      }
   }
    
}
