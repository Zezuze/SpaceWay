using System.Collections.Generic;
using UnityEngine;

public abstract class BonusSpawner : MonoBehaviour, IService
{
    [SerializeField] protected LevelConfigSO _levelConfigSO;
    [SerializeField] protected Bonus _bonusPrefab;

    protected PoolBaseQueue<Bonus> _bonusPool;
    protected BonusFactory _bonusFactory;

    protected int _bonusIndex;
    protected int _bonusCountOnLvl;

    protected List<Vector3> _spawnPositions;
    protected SpawnPositions _spawnPosition;

    public virtual void Init()
    {
        _spawnPosition = ServiceLocator.Current.Get<SpawnPositions>();
        _spawnPositions = (List<Vector3>)ServiceLocator.Current.Get<SpawnPositions>().SpawnPosition;
    }
}
