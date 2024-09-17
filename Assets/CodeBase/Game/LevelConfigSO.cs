using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelConfig", menuName = "Game/LevelConfig")]
public class LevelConfigSO : ScriptableObject
{
    [SerializeField] private int _totalCountEnemies;
    [SerializeField] private int _totalCountEnemiesForPreload;
    [SerializeField] private int _totalCountAsteroidsForPreload;

    public int TotalCountEnemies => _totalCountEnemies;
    public int TotalCountEnemiesForPreload => _totalCountEnemiesForPreload;
    public int TotalCountAsteroidsForPreload => _totalCountAsteroidsForPreload;

    [Space]

    [SerializeField] private List<EnemyConfigOnLevel> _enemyConfigOnLevel;
    public IReadOnlyList<EnemyConfigOnLevel> EnemyConfigOnLevel => _enemyConfigOnLevel;

    [Space]

    [SerializeField] private List<AsteroidConfigOnLevel> _asteroidConfigOnLevels;
    public IReadOnlyList<AsteroidConfigOnLevel> AsteroidConfigOnLevels => _asteroidConfigOnLevels;

    [Space]
    [SerializeField] private List<BonusConfigOnLevel> _bonusConfigOnLevels;
    public IReadOnlyList<BonusConfigOnLevel> BonusConfigOnLevels => _bonusConfigOnLevels;
}

[Serializable]
public class EnemyConfigOnLevel
{
    [SerializeField] private string _enemyName;
    [SerializeField] private int _enemyID;
    [SerializeField] private int _enemyCount;
    [SerializeField] private int _enemyCountForPreload;

    public int EnemyID => _enemyID;
    public int EnemyCount => _enemyCount;
    public int EnemyCountForPreload => _enemyCountForPreload;
}

[Serializable]
public class AsteroidConfigOnLevel
{
    [SerializeField] private string _asteroidName;
    [SerializeField] private int _asteroidID;
    [SerializeField] private int _asteroidCount;
    [SerializeField] private int _asteroidCountForPreload;

    public int AsteroidID => _asteroidID;
    public int AsteroidCount => _asteroidCount;
    public int AsteroidCountForPreload => _asteroidCountForPreload;
}

[Serializable]
public class BonusConfigOnLevel
{
    [SerializeField] private string _bonusName;
    [SerializeField] private int _bonusID;
    [SerializeField] private int _bonusCount;
    [SerializeField] private int _bonusCountForPreload;

    public int BonusID => _bonusID;
    public int BonusCount => _bonusCount;
    public int BonusCountForPreload => _bonusCountForPreload;
}
