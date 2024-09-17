using UnityEngine;

public class HealthBonusSpawner : BonusSpawner
{
    private bool _canShowBonus = true;

    public override void Init()
    {
        base.Init();

        _bonusFactory = new BonusFactory(_bonusPrefab, transform);
        _bonusIndex = _levelConfigSO.BonusConfigOnLevels[0].BonusID;

        _bonusPool = new PoolBaseQueue<Bonus>(Preload, GetAction, ReturnAction, _levelConfigSO.BonusConfigOnLevels[_bonusIndex].BonusCountForPreload);
        _bonusCountOnLvl = _levelConfigSO.BonusConfigOnLevels[_bonusIndex].BonusCount;
    }

    private void OnEnable()
    {
        OutOfViewCheckerDown.OnHealthOut += ReturnBonus;
        ShipCollisionHandler.OnHealthTaken += ReturnBonus;

        ShipHealth.OnHealthDecreased += ShowBonus;
        ShipHealth.OnDied += DisableCanShowBonus;
    }

    private void OnDisable()
    {
        OutOfViewCheckerDown.OnHealthOut -= ReturnBonus;
        ShipCollisionHandler.OnHealthTaken -= ReturnBonus;

        ShipHealth.OnHealthDecreased -= ShowBonus;
        ShipHealth.OnDied -= DisableCanShowBonus;
    }

    private void DisableCanShowBonus() =>
        _canShowBonus = false;

    private void ReturnBonus(Bonus bonus) =>
        _bonusPool.Return(bonus);

    private void ShowBonus()
    {
        if (_bonusCountOnLvl > 0 && _canShowBonus)
        {
            _bonusCountOnLvl--;
            Debug.Log($"Health COUNT - {_bonusCountOnLvl}");
            Debug.Log("Health " + _bonusCountOnLvl);

            Bonus bonus = _bonusPool.Get();
            int index = UnityEngine.Random.Range(0, _spawnPositions.Count);
            bonus.transform.position = _spawnPositions[index];
        }
    }

    private Bonus Preload() =>
        _bonusFactory.Create();

    private void GetAction(Bonus bonus) =>
        bonus.gameObject.SetActive(true);

    private void ReturnAction(Bonus bonus) =>
        bonus.gameObject.SetActive(false);
}