using PathCreation;
using System;
using UnityEngine;
using UnityEngine.Serialization;

public class EnemyAI : MonoBehaviour
{
    private GameObject _target;

    private Rigidbody _rigidbody;
    
    public PathCreator pathCreator;
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

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _target = GameObject.FindGameObjectWithTag("Boat");
        _distanceTravelled = pathCreator.path.GetClosestDistanceAlongPath(transform.position);
        _distanceStart = _distanceTravelled;
    }

    private void Update()
    {
        if((_distanceTravelled - _distanceStart) / pathCreator.path.length > loopCount)
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
        ChangeRotation();
        _rigidbody.AddRelativeForce(Vector3.forward * speed, ForceMode.Force);
        if (_rigidbody.velocity.magnitude > speed)
            _rigidbody.velocity = _rigidbody.velocity.normalized * speed;
    }
    
    private void ChangeRotation()
    {
        Vector3 direction = (_target.transform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        lookRotation = new Quaternion(transform.rotation.x, lookRotation.y, 0, lookRotation.w);
        transform.rotation = lookRotation;
    }

    private void FollowPath()
    {
        _distanceTravelled += speed * Time.deltaTime;
        transform.position = pathCreator.path.GetPointAtDistance(_distanceTravelled);
        transform.rotation = pathCreator.path.GetRotationAtDistance(_distanceTravelled);
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
