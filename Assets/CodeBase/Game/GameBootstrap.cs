using System.Linq;
using UnityEngine;

public class GameBootstrap : MonoBehaviour
{
    [Space, Header("Init")]
    [SerializeField] private MapBoundaries _mapBoundaries;
    [SerializeField] private ShipMovement _shipMovement;
    [SerializeField] private ShipShooting _shipShooting;
    [SerializeField] private BackgroundScroller _backgroundScroller;
    [SerializeField] private EnemySpawner _enemySpawner;
    [SerializeField] private AsteroidSpawner _asteroidSpawner;
    [SerializeField] private SpawnPositions _spawnPositions;
    [SerializeField] private GameServiceLocator _gameServiceLocator;
    [SerializeField] private OutOfViewCheckerDown _outOfViewCheckerDown;
    [SerializeField] private OutOfViewCheckerUp _outOfViewCheckerUp;
    [SerializeField] private BonusSpawner[] _bonusSpawners;

    // [SerializeField] private EnemyShipShooting _enemyShipShooting;

    // [Space, Header("Init Parameters")]

    // [SerializeField] private EnemyShip _enemyShip;

    private void Awake()
    {
        _shipMovement.Init();
        _shipShooting.Init();
        _mapBoundaries.Init();
        _backgroundScroller.Init();

        _spawnPositions.Init();
        _gameServiceLocator.Init();

        _outOfViewCheckerDown.Init();
        _outOfViewCheckerUp.Init();

        _asteroidSpawner.Init();
        _enemySpawner.Init();

        foreach (var spawner in _bonusSpawners) spawner.Init();
        
        // _enemyShipShooting.Init();
    }
}
