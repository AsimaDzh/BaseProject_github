using UnityEngine;

/// <summary>
/// Simple ranged weapon that spawns a projectile when attacking. 
/// The projectile will move forward and can hit targets based on the specified layers and range. 
/// Damage and other parameters are defined in the WeaponData.
/// </summary>

public class RangedWeapon : WeaponBase
{
    [Header("========== Params of Ranged Weapon ==========")]
    public Transform shootOrigin;
    public float projectileSpeedOverride = 0f;
    public LayerMask projectileHitLayers;


    public override void Attack()
    {
        if (!CanAttack()) return;

        StartAttackCooldown();

        if (weaponData || weaponData.projectilePrefab == null)
        {
            Debug.LogWarning($"{name}: WeaponData is null or ProjectilePrefab is null", this);
            return;
        }

        // Determine spawn position and rotation for the projectile
        Vector3 _spawnPosition = shootOrigin != null
            ? shootOrigin.position
            : (owner != null ? owner.position : transform.position);

        Quaternion _spawnRotation = shootOrigin != null
            ? shootOrigin.rotation
            : (owner != null ? owner.rotation : transform.rotation);

        GameObject projectileObject = Instantiate(
            weaponData.projectilePrefab,
            _spawnPosition,
            _spawnRotation
        );

        //Projectile _projectile = projectileObject.GetComponent<Projectile>();
        //if (projectile != null)
        //{
        //    projectile.damage = Damage;
        //    projectile.maxDistance = Range;
        //    projectile.hitLayers = projectileHitLayers;

        //    if (projectileSpeedOverride > 0f)
        //        projectile.speed = projectileSpeedOverride;
        //}

        Debug.Log($"{name}: range attack, the projectile is launched with {Damage} damage and {Range} range.");
    }
}