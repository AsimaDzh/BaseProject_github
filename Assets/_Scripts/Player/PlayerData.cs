using UnityEngine;

// This is a ScriptableObject that can be used to store player-related data
[CreateAssetMenu(
    fileName = "PlayerData", 
    menuName = "GameData/PlayerData", 
    order = 0)]
public class PlayerData : ScriptableObject
{
    [Header("========== Player Stats ==========")]
    [Min(1f)] public float maxHealth = 100f;
    [Min(0f)] public float maxMana = 0f;

    [Header("========== Movement ===========")]
    [Min(0f)] public float moveSpeed = 5f;
    [Min(0f)] public float jumpForce = 5f;

    [Header("========== Aditional parametrs ===========")]
    [Min(0f)] public float acceleration = 10f;
    [Min(0f)] public float rotationSpeed = 720f;
}
