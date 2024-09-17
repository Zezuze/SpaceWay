using System;
using System.Collections.Generic;

public class PoolBase<T>
{
    private readonly Func<T> _preloadFunc;
    private readonly Action<T> _getAction;
    private readonly Action<T> _returnAction;

    private List<T> _pool = new List<T>();
    private List<T> _active = new List<T>();

    public PoolBase(Func<T> preloadFunc, Action<T> getAction, Action<T> returnAction, int preloadCount)
    {
        _preloadFunc = preloadFunc ?? throw new ArgumentNullException(nameof(preloadFunc));
        _getAction = getAction;
        _returnAction = returnAction;

        for (int i = 0; i < preloadCount; i++)
            Return(preloadFunc());
    }

    public T GetRandom()
    {
        if (_pool.Count > 0)
        {
            int randomIndex = UnityEngine.Random.Range(0, _pool.Count);
            T item = _pool[randomIndex];

            _pool[randomIndex] = _pool[_pool.Count - 1];
            _pool.RemoveAt(_pool.Count - 1);

            _getAction(item);
            _active.Add(item);

            return item;
        }

        return Get();
    }

    public T Get()
    {
        T item = _pool.Count > 0 ? _pool[_pool.Count - 1] : _preloadFunc();
        if (_pool.Count > 0) _pool.RemoveAt(_pool.Count - 1);

        _getAction(item);
        _active.Add(item);
        return item;
    }

    public void Return(T item)
    {
        _returnAction(item);
        _pool.Add(item);
        _active.Remove(item);
    }

    public void ReturnAll()
    {
        foreach (T item in new List<T>(_active))
            Return(item);
    }
}