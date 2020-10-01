using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public interface ILiverExtra
{
    void HealthDepleted(HitBox other);
}

public class Liver : MonoBehaviour
{
    public float Health = 1;
    public bool IsInvincible => invicibleTime > 0;
    private float invicibleDuration = 1;
    private float invicibleTime = 0;
    [HideInInspector] public List<HurtBox> HurtBoxes;

    private ILiverExtra extra;

    void Start()
    {
        HurtBoxes = GetComponentsInChildren<HurtBox>().ToList();
        extra = GetComponent<ILiverExtra>();
    }

    void Update()
    {
        invicibleTime -= Time.deltaTime;
        invicibleTime = Mathf.Max(invicibleTime, 0);
    }

    public void Hurt(HitBox other) 
    {
        if (IsInvincible) return;

        Health -= other.Damage;

        if (Health <= 0) 
        {
            if (extra != null) 
            {
                extra.HealthDepleted(other);
            }
            else 
            {
                Destroy(gameObject);
            }
        }
        else 
        {
            invicibleTime = invicibleDuration;
        }
    }
}
