using UnityEngine;

/// <summary>
/// Simple projectile class that moves forward at a constant speed.
/// And can collide with other objects to apply damage or effects.
/// </summary>

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed = 20f;
    [SerializeField] private float maxDistance = 20f;
    [SerializeField] private float damage = 10f;
    [SerializeField] private LayerMask hitLayers;

    private Vector3 _startPosition;


    private void Start()
    {
        _startPosition = transform.position;
    }


    private void Update()
    {
        transform.position += transform.forward * (speed * Time.deltaTime);

        float _traveledDistance = Vector3.Distance(_startPosition, transform.position);
        if (_traveledDistance >= maxDistance)
            Destroy(gameObject);
    }


    private void OnTriggerEnter(Collider other)
    {
        // Check if the hit object is in the specified hit layers
        if ((hitLayers.value & (1 << other.gameObject.layer)) == 0) return;
        
        Debug.Log($"{name}: hit {other.name}", other);

        // Apply damage or effects to the hit object here
        // var _damageable = other.GetComponent<IDamageable>();
        // if (_damageable != null)
        //     _damageable.TakeDamage(damage);
        Destroy(gameObject);
    }
}
