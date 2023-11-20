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

    private void Start()
    { 
        Health = MaxHealth;
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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        Apply_Damage(10f);
    }
}
public enum Chooser
{
    Player,
    Enemy,
}