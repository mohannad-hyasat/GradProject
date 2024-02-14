using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UniversalHealth : MonoBehaviour, I_AllStats
{
    public float MaxHealth;
    [HideInInspector]
    public float Health;
    public bool IsDead;
    public Animator Anim;
    public Chooser choice;
    public const float Max_Sanity = 100f;
    public float Sanity;
    public float Time_Between_Sanity_Ticks;
    private void Start()
    { 
        Health = MaxHealth;
        Sanity = Max_Sanity;
        StartCoroutine(Insanity());
    }
    /// <summary>
    /// set the maximum health of the entity
    /// </summary>
    /// <param name="Hp"></param>
    public void Set_Max_Health(float Hp)
    {
        Health = Hp;
        
    }
    /// <summary>
    /// apply damage to the entity by the amount damage
    /// </summary>
    /// <param name="damage"></param>
    public void Apply_Damage(float damage)
    {
        Health -= damage;

        if (Health <= 0 && !IsDead)
        {
            if (Anim != null)
            {
                Anim.SetTrigger("Dead");
            }
            IsDead = true;
        }
    }

    public void Apply_Insanity(float sanity_Damage)
    {
        Sanity -= sanity_Damage;
    }
   public  IEnumerator Insanity()
    {
        yield return new WaitForSeconds(Time_Between_Sanity_Ticks);
        Apply_Insanity(1);
        if (Sanity > 0)
        {
            StartCoroutine(Insanity());
        }
    }

}
public enum Chooser
{
    Player,
    Enemy,
}