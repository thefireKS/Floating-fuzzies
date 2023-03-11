using UnityEngine;

public class Hero : MonoBehaviour
{
    [SerializeField] private float rotationSpeed;

    private float _currentAngle, _targetAngle;
    private float _elapsedTime;

    [SerializeField] private LayerMask layerMask;

    private Camera _mainCamera;

    private void OnEnable()
    {
        _mainCamera = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Rotation(GetNewAngle());
        }
    }

    private Quaternion GetNewAngle()
    {
        Vector2 mp = Input.mousePosition;
        var ray = _mainCamera.ScreenPointToRay(mp);
        Physics.Raycast(ray, out var hitInfo, Mathf.Infinity, layerMask);
        Vector3 direction = (hitInfo.point - transform.position).normalized;
        return Quaternion.LookRotation(direction);
    }

    private void Rotation(Quaternion lookRotation)
    {
        lookRotation = new Quaternion(0, lookRotation.y, 0, lookRotation.w);
        var rotation = transform.rotation;
        float angle = Quaternion.Angle(rotation, lookRotation);
        float timeToComplete = angle / rotationSpeed;
        float donePercentage = Mathf.Min(1F, Time.deltaTime / timeToComplete);
        rotation = Quaternion.Lerp(rotation, lookRotation, donePercentage);
        transform.rotation = rotation;
    }
}
