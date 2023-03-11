using PathCreation;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private GameObject _target;

    private Rigidbody _rigidbody;
    
    private PathCreator _pathCreator;
    [SerializeField] private float loopCount;

    [SerializeField] private float speed;
    private float _distanceTravelled;
    private float _distanceStart;

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
}
