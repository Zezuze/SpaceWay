using UnityEngine;

public class GameServiceLocator : MonoBehaviour
{
    [SerializeField] private SpawnPositions _spawnPositions;
    [SerializeField] private EnemySpawner _enemySpawner;
    [SerializeField] private AsteroidSpawner _asteroidSpawner;

    public void Init()
    {
        ServiceLocator.Initialize();

        RegisterServices();
    }

    private void RegisterServices()
    {
        ServiceLocator.Current.Register(_spawnPositions);
        ServiceLocator.Current.Register(_enemySpawner);
        ServiceLocator.Current.Register(_asteroidSpawner);
    }
}

