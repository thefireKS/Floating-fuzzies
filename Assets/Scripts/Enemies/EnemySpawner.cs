using PathCreation;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] enemies;

    [SerializeField] private PathCreator _pathCreator;

    [SerializeField] private float timeBetweenSpawns;
    private float _timeSinceLastSpawn = Mathf.Infinity;

    private float _timeElapsed = 0f;

    public bool spawnOnPath;
    [SerializeField] private float radius;

    private void OnEnable()
    {
        _pathCreator = FindObjectOfType<PathCreator>();
    }

    private void Update()
    {
        if (_timeSinceLastSpawn >= timeBetweenSpawns) Spawn();
        _timeSinceLastSpawn += Time.deltaTime;
        _timeElapsed += Time.deltaTime;

        if (_timeElapsed > 15f && timeBetweenSpawns > 0.5f)
        {
            timeBetweenSpawns -= 0.25f;
            _timeElapsed = 0f;
        }
    }

    private void Spawn()
    {
        Vector3 pointToSpawn;
        if (spawnOnPath)
        {
            var pointIndex = Random.Range(0, _pathCreator.path.NumPoints);
            pointToSpawn = _pathCreator.path.GetPoint(pointIndex);
        }
        else
        {
            var angle = Random.Range(0, 361);
            pointToSpawn = new Vector3(transform.position.x + radius * Mathf.Cos(Mathf.Deg2Rad * angle), 3f,
                transform.position.z + radius * Mathf.Sin(Mathf.Deg2Rad * angle));
        }
        var enemyToSpawn = enemies[Random.Range(0, enemies.Length)];
        Instantiate(enemyToSpawn, pointToSpawn, enemyToSpawn.transform.rotation, transform).GetComponent<EnemyAI>().pathCreator = _pathCreator;
        _timeSinceLastSpawn = 0f;
    }
}