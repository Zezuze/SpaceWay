using UnityEngine;

public class BonusFactory
{
    private Transform _spawnerPosition;
    private Bonus _bonus;

    public BonusFactory(Bonus bonusPrefab, Transform spawnerPosition)
    {
        _bonus = bonusPrefab;
        _spawnerPosition = spawnerPosition;
    }

    public Bonus Create()
    {
        Bonus bonus = GameObject.Instantiate(_bonus);
        bonus.Init();
        bonus.transform.SetParent(_spawnerPosition);

        return bonus;
    }
}
