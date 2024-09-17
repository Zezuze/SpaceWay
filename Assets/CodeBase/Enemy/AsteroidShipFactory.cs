using UnityEngine;

public class AsteroidShipFactory
{
    private LevelConfigSO _levelConfigSO;
    private Transform _spawnerPosition;

    private int _count;
    private int _commonIndex;
    private Asteroid _asteroid;
    private Asteroid[] _allAsteroids;

    public AsteroidShipFactory(LevelConfigSO levelConfig, Asteroid[] asteroids, Transform spawnerPosition)
    {
        _commonIndex = 0;

        _count = levelConfig.AsteroidConfigOnLevels[_commonIndex].AsteroidCountForPreload;
        _asteroid = asteroids[_commonIndex];

        _allAsteroids = asteroids;
        _levelConfigSO = levelConfig;

        _spawnerPosition = spawnerPosition;
    }

    public Asteroid Create()
    {
        if (_count > 0)
        {
            Asteroid asteroid = GameObject.Instantiate(_asteroid);
            asteroid.Init();
            asteroid.transform.SetParent(_spawnerPosition);

            _count--;

            return asteroid;
        }
        else
        {
            _commonIndex++;
            if (_commonIndex < _levelConfigSO.AsteroidConfigOnLevels.Count)
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
        _count = _levelConfigSO.AsteroidConfigOnLevels[index].AsteroidCountForPreload;
        _asteroid = _allAsteroids[index];
    }
}
