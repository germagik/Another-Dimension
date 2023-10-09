using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
   public Effect effect;
   public float damage = 5f;
   public void Perform(string targetTag)
   {
      Effect newEffect = Instantiate(effect, transform.position, transform.rotation);
      newEffect.Initialize(targetTag, damage); 
   }
    
}
