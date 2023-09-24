using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Attack : MonoBehaviour
{
   public Effect effect;
   public abstract void Perform();
    
}
