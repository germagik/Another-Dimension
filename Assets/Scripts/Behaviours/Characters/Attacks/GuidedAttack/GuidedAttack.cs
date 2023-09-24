using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuidedAttack : Attack
{
    public override void Perform()
    {
        Instantiate(effect, transform.position, transform.rotation);
    }

}
