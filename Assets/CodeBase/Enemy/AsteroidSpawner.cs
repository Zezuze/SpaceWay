using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour, IService
{
    [SerializeField] private Asteroid[] _asteroidPrefabs;
    [SerializeField] private LevelConfigSO _levelConfigSO;
    private List<Vector3> _spawnPositions;
    private SpawnPositions _spawnPosition;

    private PoolBase<Asteroid> _asteroidPool;
    private AsteroidShipFactory _asteroidFactory;

    public void Init()
    {
        _spawnPosition = ServiceLocator.Current.Get<SpawnPositions>();
        _spawnPositions = (List<Vector3>)ServiceLocator.Current.Get<SpawnPositions>().SpawnPosition;

        _asteroidFactory = new AsteroidShipFactory(_levelConfigSO, _asteroidPrefabs, transform);
        _asteroidPool = new PoolBase<Asteroid>(Preload, GetAction, ReturnAction, _levelConfigSO.TotalCountAsteroidsForPreload);
    }

    private void Start() =>
        StartCoroutine(SpawnAsteroid());

    private void OnEnable()
    {
        OutOfViewCheckerDown.OnAsteroidOut += ReturnAsteroid;
        AsteroidHealth.OnAsteroidDied += ReturnAsteroid;
    }

    private void OnDisable()
    {
        OutOfViewCheckerDown.OnAsteroidOut -= ReturnAsteroid;
        AsteroidHealth.OnAsteroidDied -= ReturnAsteroid;
    }

    public void ReturnAsteroid(Asteroid asteroid) =>
        _asteroidPool.Return(asteroid);

    public IEnumerator SpawnAsteroid()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);

            ShowEnemy();
        }
    }

    private void ShowEnemy()
    {
        Asteroid asteroid = _asteroidPool.GetRandom();

        int index = Random.Range(0, _spawnPositions.Count);
        asteroid.transform.position = _spawnPositions[index];
        _spawnPosition.RemoveElementFromListByIndex(index);
    }

    private Asteroid Preload() => _asteroidFactory.Create();

    private void GetAction(Asteroid ship) =>
        ship.gameObject.SetActive(true);

    private void ReturnAction(Asteroid ship) =>
        ship.gameObject.SetActive(false);
}