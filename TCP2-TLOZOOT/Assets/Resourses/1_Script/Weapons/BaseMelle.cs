using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class BaseMelle : MonoBehaviour
{
    [SerializeField] float cooldownBase, knockback;
    [SerializeField] int damage, id, type, maxCombo;
    [SerializeField] int extra1, extra2, extra3;

    private float cooldown, comboCount;

    public abstract void Attack();

    public abstract void CanAttack();

    public bool IsCooldownZero()
    {
        return cooldown == 0;
    }

    public void ResetCooldown()
    {
        cooldown = cooldownBase;
    }

    public void AddCombo()
    {
        comboCount++;

        if(comboCount > maxCombo)
            comboCount = 0;
    }
}
