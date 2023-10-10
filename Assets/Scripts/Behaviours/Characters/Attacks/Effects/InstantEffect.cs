using UnityEngine;

public class InstantEffect : Effect
{
    public float duration = 1f;
    void Update()
    {
        duration -= Time.deltaTime;
        if (duration <= 0)
        {
           Destroy(gameObject);
        }
    }
}
