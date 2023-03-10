using System;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private GameObject ammo;
    
    [SerializeField] private Transform gunPoint;
    
    [SerializeField] private int roundsPerMinute;
    private float _timeSinceLastShot = Mathf.Infinity;
    
    private bool CanShoot() => _timeSinceLastShot >= 1f / (roundsPerMinute / 60f);

    private void Start()
    {
        Debug.Log(1f / (roundsPerMinute / 60f));
    }

    private void Shoot()
    {
        if(!CanShoot()) return;
        Quaternion ammoRotation = transform.localScale.x > 0 ? transform.rotation : new Quaternion(0f, 0f, 180f,0f);
        Instantiate(ammo, gunPoint.position, ammoRotation);
        _timeSinceLastShot = 0;
    }
    private void Update()
    {
        _timeSinceLastShot += Time.fixedDeltaTime;
        if (Input.touchCount > 0)
        {
            Shoot();
        }
    }
}
