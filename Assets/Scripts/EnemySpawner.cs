using PathCreation;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] enemies;

    private PathCreator _pathCreator;
    
    [SerializeField] private float timeBetweenSpawns;
    private float _timeSinceLastSpawn = Mathf.Infinity;

    private void OnEnable()
    {
        _pathCreator = FindObjectOfType<PathCreator>();
    }

    private void Update()
    {
        if (_timeSinceLastSpawn >= timeBetweenSpawns) Spawn();
        _timeSinceLastSpawn += Time.deltaTime;
    }

    private void Spawn()
    {
        var pointIndex = Random.Range(0, _pathCreator.path.NumPoints - 1);
        var pointToSpawn = _pathCreator.path.GetPoint(pointIndex);
        var enemyToSpawn = enemies[Random.Range(0, enemies.Length - 1)];
        Instantiate(enemyToSpawn, pointToSpawn, quaternion.identity);
        _timeSinceLastSpawn = 0f;
    }
}
