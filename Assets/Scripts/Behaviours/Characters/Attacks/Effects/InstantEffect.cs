using UnityEngine;

public class InstantEffect : Effect
{
    [SerializeField] protected float duration = 0.5f;
    void Update()
    {
        duration -= Time.deltaTime;
        if (duration <= 0) {
            Destroy(gameObject);
            return;
        }
    }
}
