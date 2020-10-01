using UnityEngine;

public class HurtBox : MonoBehaviour
{
    [HideInInspector] public Liver Liver;

    void Start()
    {
        Liver = GetComponentInParent<Liver>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        HitBox hitBox = other.GetComponent<HitBox>();
        Liver.Hurt(hitBox);
    }
}