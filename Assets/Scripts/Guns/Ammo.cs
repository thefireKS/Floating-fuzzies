using UnityEngine;

public class Ammo : MonoBehaviour
{
    private Rigidbody _rigidbody;

    [SerializeField] private float force;

    [SerializeField] private float damage;

    private void OnEnable()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.AddRelativeForce(Vector3.forward * force,ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy")) collision.gameObject.GetComponent<Enemy>().TakeDamage(damage);
        Destroy(gameObject);
    }
}
