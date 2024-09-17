using UnityEngine;

public class EnemyShipFactory
{
    private LevelConfigSO _levelConfigSO;
    private Transform _spawnerPosition;

    private int _count;
    private int _commonIndex;
    private EnemyShip _enemyShip;
    private EnemyShip[] _allEnemyShips;

    public EnemyShipFactory(LevelConfigSO levelConfig, EnemyShip[] enemyShips, Transform spawnerPosition)
    {
        _commonIndex = 0;

        _count = levelConfig.EnemyConfigOnLevel[_commonIndex].EnemyCountForPreload;
        _enemyShip = enemyShips[_commonIndex];

        _allEnemyShips = enemyShips;
        _levelConfigSO = levelConfig;

        _spawnerPosition = spawnerPosition;
    }

    public EnemyShip Create()
    {
        if (_count > 0)
        {
            EnemyShip ship = GameObject.Instantiate(_enemyShip);
            ship.Init();
            ship.transform.SetParent(_spawnerPosition);

            _count--;

            return ship;
        }
        else
        {
            _commonIndex++;
            if (_commonIndex < _levelConfigSO.EnemyConfigOnLevel.Count)
            {
                SetIndexForFactory(_commonIndex);
                return Create();
            }
            else
            {
                _commonIndex = 0;
                SetIndexForFactory(_commonIndex);
                return Create();
            }
        }
    }

    private void SetIndexForFactory(int index)
    {
        _count = _levelConfigSO.EnemyConfigOnLevel[index].EnemyCountForPreload;
        _enemyShip = _allEnemyShips[index];
    }
}