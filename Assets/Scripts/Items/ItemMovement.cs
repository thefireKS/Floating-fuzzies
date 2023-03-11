using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMovement : MonoBehaviour
{
    [SerializeField] private float force;
    private Rigidbody _rigidbody;
    private void OnEnable()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.AddRelativeForce(Vector3.right * force, ForceMode.Impulse);
        Destroy(gameObject, 10f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!(collision.gameObject.CompareTag("Boat")||collision.gameObject.CompareTag("Enemy")|| collision.gameObject.CompareTag("Item")))
            Destroy(gameObject);
    }
}
