using System.Collections;
using UnityEngine;

public class EnemyShipShooting : MonoBehaviour
{
    [SerializeField] Transform _bulletSpawner;
    [SerializeField] BulletMovement _projectilePrefab;

    private PoolBase<BulletMovement> _bulletPool;

    public void Awake()
    {
        _bulletPool = new PoolBase<BulletMovement>(Preload, GetAction, ReturnAction, 3);
    }

    private void OnEnable()
    {
        OutOfViewCheckerDown.OnBulletOut += ReturnBullet;
        StartCoroutine(Shoot());
    }

    private void OnDisable()
    {
        StopCoroutine(Shoot());
        OutOfViewCheckerDown.OnBulletOut -= ReturnBullet;
    }


    private IEnumerator Shoot()
    {
        yield return new WaitForSeconds(1);

        while (true)
        {
            BulletMovement bullet = _bulletPool.Get();
            bullet.transform.position = _bulletSpawner.position;

            yield return new WaitForSeconds(1);
        }
    }

    private void ReturnBullet(BulletMovement bullet)
    {
        _bulletPool.Return(bullet);
    }

    public BulletMovement Preload()
    {
        BulletMovement bullet = Instantiate(_projectilePrefab);
        bullet.Init();
        bullet.transform.SetParent(_bulletSpawner.transform);
        return bullet;
    }

    public void GetAction(BulletMovement bullet) =>
        bullet.gameObject.SetActive(true);

    public void ReturnAction(BulletMovement bullet) =>
        bullet.gameObject.SetActive(false);
}
