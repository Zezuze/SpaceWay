using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour, IService
{
    [SerializeField] private EnemyShip[] _enemyPrefabs;
    [SerializeField] private LevelConfigSO _levelConfigSO;
    private List<Vector3> _spawnPositions;
    private SpawnPositions _spawnPosition;

    private PoolBase<EnemyShip> _enemyPool;
    private EnemyShipFactory _enemyShipFactory;

    public void Init()
    {
        _spawnPosition = ServiceLocator.Current.Get<SpawnPositions>();
        _spawnPositions = (List<Vector3>)ServiceLocator.Current.Get<SpawnPositions>().SpawnPosition;

        _enemyShipFactory = new EnemyShipFactory(_levelConfigSO, _enemyPrefabs, transform);
        _enemyPool = new PoolBase<EnemyShip>(Preload, GetAction, ReturnAction, _levelConfigSO.TotalCountEnemiesForPreload);
    }

    private void Start() =>
        StartCoroutine(SpawnEnemy());

    private void OnEnable()
    {
        OutOfViewCheckerDown.OnShipOut += ReturnEnemy;
        EnemyShipHealth.OnEnemyShipDied += ReturnEnemy;
    }

    private void OnDisable()
    {
        OutOfViewCheckerDown.OnShipOut -= ReturnEnemy;
        EnemyShipHealth.OnEnemyShipDied -= ReturnEnemy;
    }

    private void ReturnEnemy(EnemyShip enemyShip) =>
        _enemyPool.Return(enemyShip);

    public IEnumerator SpawnEnemy()
    {
        yield return new WaitForSeconds(2f);

        while (true)
        {
            yield return new WaitForSeconds(0.5f);

            ShowEnemy();
        }
    }

    private void ShowEnemy()
    {
        EnemyShip enemyShip = _enemyPool.GetRandom();

        int index = Random.Range(0, _spawnPositions.Count);
        enemyShip.transform.position = _spawnPositions[index];                      // enemyShip.transform.position = SetPosition(enemyShip);                  
        _spawnPosition.RemoveElementFromListByIndex(index);
    }

    private EnemyShip Preload() => _enemyShipFactory.Create();

    private void GetAction(EnemyShip ship) =>
        ship.gameObject.SetActive(true);

    private void ReturnAction(EnemyShip ship) =>
        ship.gameObject.SetActive(false);
}
