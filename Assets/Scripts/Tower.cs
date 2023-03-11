using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] private GameObject ammo;
    
    [SerializeField] private Transform gunPoint;

    [SerializeField] private Transform enemiesHolderTransform;
    private GameObject _target;
    
    [SerializeField] private int roundsPerMinute;
    private float _timeSinceLastShot = Mathf.Infinity;
    
    private bool CanShoot() => _timeSinceLastShot >= 1f / (roundsPerMinute / 60f);

    private void Shoot()
    {
        if(!CanShoot()) return;
        ChangeRotation();
        Quaternion ammoRotation = transform.localScale.x > 0 ? transform.rotation : new Quaternion(0f, 0f, 180f,0f);
        Instantiate(ammo, gunPoint.position, ammoRotation);
        _timeSinceLastShot = 0;
    }

    private void ChangeRotation()
    {
        Vector3 direction = (_target.transform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        lookRotation = new Quaternion(0, lookRotation.y, 0, lookRotation.w);
        transform.rotation = lookRotation;
    }

    private void Update()
    {
        _timeSinceLastShot += Time.fixedDeltaTime;
        if (enemiesHolderTransform.childCount > 0) _target = enemiesHolderTransform.GetChild(0).gameObject;
        if (_target != null)
        {
            Shoot();
        }
    }



}
