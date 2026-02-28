using UnityEngine;

public class MeleeWeapon : WeaponBase
{
    [Header("Params of melee weapon")]
    public Transform attackOrigin;
    public float hitRadius = 1.5f;
    public LayerMask hitLayers;
}
