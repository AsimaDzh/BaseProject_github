using UnityEngine;

public abstract class WeaponBase : MonoBehaviour
{
    [Header("========== Weapon Data ==========")]
    [SerializeField] private WeaponData weaponData;
    [SerializeField] private Transform owner;

    protected float nextAttackTime = 0f;

    public float Damage => weaponData.damage;
    public float Range => weaponData.range;
    public float AttackSpeed => weaponData.attackSpeed;


    public virtual bool CanAttack()
    {
        if (weaponData == null)
        {
            Debug.LogWarning($"{name}: WeaponData not assigned", this);
            return false;
        }

        return Time.time >= nextAttackTime;
    }


    protected void StartAttackCooldown()
    {
        // if AttackSpeed is 0 or negative, we can set a default cooldown to prevent spamming attacks every frame
        float cooldown = AttackSpeed > 0f ? (1f / AttackSpeed) : 0.5f;
        nextAttackTime = Time.time + cooldown;
    }


    public abstract void Attack();
}
