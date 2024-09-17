using UnityEngine;
using UnityEngine.Pool;

public abstract class UnityPool
{
    private ObjectPool<EnemyShip> _pool;

    protected EnemyShip _prefab;
    protected int _fixedAmount;
    protected int _maxAmount;

    public UnityPool(EnemyShip prefab, int fixedAmount, int maxAmount)
    {
        _prefab = prefab;
        _fixedAmount = fixedAmount;
        _maxAmount = maxAmount;
        _pool = new ObjectPool<EnemyShip>(OnCreateObject, OnGetObject, OnReturnObject, OnDestroyObject, false, _fixedAmount, _maxAmount);

        for (int i = 0; i < _fixedAmount; i++)
            ReturnObject(OnCreateObject());
    }

    public abstract EnemyShip OnCreateObject();
    public abstract void OnGetObject(EnemyShip obj);
    public abstract void OnReturnObject(EnemyShip obj);
    public abstract void OnDestroyObject(EnemyShip obj);

    public EnemyShip GetObject() =>
        _pool.Get();

    public void ReturnObject(EnemyShip obj) =>
        _pool.Release(obj);
}
