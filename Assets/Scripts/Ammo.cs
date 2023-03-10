using UnityEngine;

public class Ammo : MonoBehaviour
{
    private Rigidbody _rigidbody;

    [SerializeField] private float force;

    private void OnEnable()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.AddRelativeForce(Vector3.forward * force,ForceMode.Impulse);
    }
}
