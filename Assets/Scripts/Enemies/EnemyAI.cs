using PathCreation;
using System;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private GameObject _target;

    private Rigidbody _rigidbody;
    
    private PathCreator _pathCreator;
    [SerializeField] private float loopCount;

    [SerializeField] private float speed;
    [Space(5)]
    [SerializeField] private int damage;

    [SerializeField] private float attackCooldown;

    public static Action<int> OnDamageDeal;

    private float _distanceTravelled;
    private float _distanceStart;

    private bool isAttacking;
    private float attackTimer = 0f;

    private void OnEnable()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _target = GameObject.FindGameObjectWithTag("Boat");
        _pathCreator = FindObjectOfType<PathCreator>();
        _distanceTravelled = _pathCreator.path.GetClosestDistanceAlongPath(transform.position);
        _distanceStart = _distanceTravelled;
    }

    private void Update()
    {
        if((_distanceTravelled - _distanceStart) / _pathCreator.path.length > loopCount)
        {
            GoToBoat();
        }
        else
        {
            FollowPath();
        }

        attackTimer += Time.deltaTime;

        if (isAttacking)
            Attack();
    }

    private void GoToBoat()
    {
        transform.LookAt(_target.transform);
        _rigidbody.AddRelativeForce(Vector3.forward * speed, ForceMode.Force);
        if (_rigidbody.velocity.magnitude > speed)
            _rigidbody.velocity = _rigidbody.velocity.normalized * speed;
    }

    private void FollowPath()
    {
        _distanceTravelled += speed * Time.deltaTime;
        transform.position = _pathCreator.path.GetPointAtDistance(_distanceTravelled);
        transform.rotation = _pathCreator.path.GetRotationAtDistance(_distanceTravelled);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Boat"))
            isAttacking = true;
    }

    private void Attack()
    {
        if(attackTimer > attackCooldown)
        {
            OnDamageDeal?.Invoke(damage);
            attackTimer = 0f;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Boat"))
            isAttacking = false;
    }
}
