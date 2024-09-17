using System;
using UnityEngine;

public class ShieldBonusSpawner : BonusSpawner
{
    public static event Action OnShieldsGone;

    public override void Init()
    {
        base.Init();

        _bonusFactory = new BonusFactory(_bonusPrefab, transform);
        _bonusIndex = _levelConfigSO.BonusConfigOnLevels[1].BonusID;

        _bonusPool = new PoolBaseQueue<Bonus>(Preload, GetAction, ReturnAction, _levelConfigSO.BonusConfigOnLevels[_bonusIndex].BonusCountForPreload);
        _bonusCountOnLvl = _levelConfigSO.BonusConfigOnLevels[_bonusIndex].BonusCount;
    }

    private void OnEnable()
    {
        OutOfViewCheckerDown.OnShieldOut += ReturnBonus;
        ShipCollisionHandler.OnShieldTaken += ReturnBonus;

        BackgroundScroller.OnDistanceCovered += ShowBonus;
    }

    private void OnDisable()
    {
        OutOfViewCheckerDown.OnShieldOut -= ReturnBonus;
        ShipCollisionHandler.OnShieldTaken -= ReturnBonus;

        BackgroundScroller.OnDistanceCovered -= ShowBonus;
    }

    private void ReturnBonus(Bonus bonus) =>
        _bonusPool.Return(bonus);

    private void ShowBonus()
    {
        if (_bonusCountOnLvl > 0)
        {
            _bonusCountOnLvl--;
            Debug.Log($"Shield COUNT - {_bonusCountOnLvl}");
            Debug.Log("Shield " + _bonusCountOnLvl);

            Bonus bonus = _bonusPool.Get();
            int index = UnityEngine.Random.Range(0, _spawnPositions.Count);
            bonus.transform.position = _spawnPositions[index];

            if (_bonusCountOnLvl == 0) OnShieldsGone?.Invoke();
        }
    }

    private Bonus Preload() =>
        _bonusFactory.Create();

    private void GetAction(Bonus bonus) =>
        bonus.gameObject.SetActive(true);

    private void ReturnAction(Bonus bonus) =>
        bonus.gameObject.SetActive(false);
}