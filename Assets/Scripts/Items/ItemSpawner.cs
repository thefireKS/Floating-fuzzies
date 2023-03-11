using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] items;
    [Space(5)]
    [SerializeField] private float spawnCooldown = 3f;
    private float timer = 0f;

    private BoxCollider _boxCollider;
    private float boxColliderSize;

    private void Start()
    {
        _boxCollider = GetComponent<BoxCollider>();
        boxColliderSize = _boxCollider.size.magnitude;
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if(timer > spawnCooldown)
        {
            SpawnItem();
            timer = 0f;
        }
    }

    private void SpawnItem()
    {
        Vector3 colliderPos = _boxCollider.transform.position;
        float randomPosX = Random.Range(colliderPos.x - _boxCollider.size.x / 2, colliderPos.x + _boxCollider.size.x / 2);
        float randomPosY = Random.Range(colliderPos.y - _boxCollider.size.y / 2, colliderPos.y + _boxCollider.size.y / 2);
        float randomPosZ = Random.Range(colliderPos.z - _boxCollider.size.z / 2, colliderPos.z + _boxCollider.size.z / 2);
        Vector3 randomPos = new Vector3(randomPosX, randomPosY, randomPosZ);
        var qi = Quaternion.identity;
        var rr = Random.rotation;
        Quaternion randomRotation = new Quaternion(rr.x, qi.y, rr.z, qi.w);
        Instantiate(items[Random.Range(0, items.Length)], randomPos, randomRotation, gameObject.transform);
    }
}
