using UnityEngine;

public class MeleeWeapon : WeaponBase
{
    [Header("Params of melee weapon")]
    public Transform attackOrigin;
    public float hitRadius = 1.5f;
    public LayerMask hitLayers;

    public override void Attack()
    {
        if (!CanAttack()) return;

        StartAttackCooldown();

        if (weaponData == null)
        {
            Debug.LogWarning($"{name}: WeaponData not assigned", this);
            return;
        }

        float _radius = hitRadius > 0f ? hitRadius : Range;
        
        Vector3 _origin = attackOrigin != null 
            ? attackOrigin.position 
            : (owner != null ? owner.position : transform.position);

        Collider[] _hits = Physics.OverlapSphere(_origin, _radius, hitLayers);

        if (_hits.Length == 0)
            Debug.Log($"{name}: melee attack missed");
        else
        {
            Debug.Log($"{name}: melee attack hit {_hits.Length} target(s)");
            foreach (Collider _collider in _hits)
            {
                Debug.Log($"{name}: hit {_collider.name}", _collider);

                //var _damageable = _collider.GetComponent<IDamageable>();
                //if (_damageable != null)
                //    _damageable.TakeDamage(Damage);
            }
        }
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        float _radius = hitRadius > 0f 
            ? hitRadius 
            : (weaponData != null ? weaponData.range : 1.5f);
        Vector3 _origin = attackOrigin != null 
            ? attackOrigin.position 
            : (owner != null ? owner.position : transform.position);

        Gizmos.DrawWireSphere(_origin, _radius);
    }
}
