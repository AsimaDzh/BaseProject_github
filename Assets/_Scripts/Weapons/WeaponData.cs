using UnityEngine;


[CreateAssetMenu(
    fileName = "WeaponData", 
    menuName = "GameData/WeaponData", 
    order = 1)]

public class WeaponData : ScriptableObject
{
    public enum WeaponType
    {
        Melee = 0,
        Ranged = 1,
        Magic = 2
    }

    [Header("========== General ==========")]
    public string weaponName = "New Weapon";
    public WeaponType weaponType = WeaponType.Melee;
    public Sprite icon;

    [Header("========== Attack ==========")]
    [Min(0f)] public float damage = 10f;
    [Min(0.1f)] public float attackSpeed = 1f;
    [Min(0f)] public float range = 2f;
    [Min(0f)] public float knockbackForce = 0f;

    [Header("========== Animations & Effects ==========")]
    public string attackAnimationName;
    public AudioClip attackSound;

    [Header("========== Projectiles ===========")]
    public GameObject projectilePrefab;
}
