using UnityEngine;

public class ShipShooting : MonoBehaviour
{
    [SerializeField] RectTransform _projectileSpawner;
    [SerializeField] BulletMovement _projectilePrefab;

    private PoolBase<BulletMovement> _bulletPool;
    private GameObject _blockProjectiles;
    private bool _canShoot;

    public void Init()
    {
        _blockProjectiles = new GameObject("ProjectileSpawner");
        _bulletPool = new PoolBase<BulletMovement>(Preload, GetAction, ReturnAction, 10);

        _canShoot = true;
    }

    private void OnEnable()
    {
        BulletCollisonHandler.OnPlayerBulletCollided += ReturnBullet;
        OutOfViewCheckerUp.OnPlayerBulletOut += ReturnBullet;
    }

    private void OnDisable()
    {
        BulletCollisonHandler.OnPlayerBulletCollided -= ReturnBullet;
        OutOfViewCheckerUp.OnPlayerBulletOut += ReturnBullet;
    }

    private void ReturnBullet(BulletMovement bullet) =>
        _bulletPool.Return(bullet);

    private void Update()
    {
        if (Input.GetMouseButton(0) && _canShoot) Shoot();
    }

    private void Shoot()
    {
        _canShoot = false;

        BulletMovement bullet = _bulletPool.Get();
        bullet.transform.position = _projectileSpawner.position;

        Invoke(nameof(ResetCanShoot), 0.2f);
    }

    private void ResetCanShoot() => _canShoot = true;

    public BulletMovement Preload()
    {
        BulletMovement bullet = Instantiate(_projectilePrefab);
        bullet.Init();
        bullet.transform.SetParent(_blockProjectiles.transform);
        return bullet;
    }

    public void GetAction(BulletMovement bulletMovement) =>
        bulletMovement.gameObject.SetActive(true);

    public void ReturnAction(BulletMovement bulletMovement) =>
        bulletMovement.gameObject.SetActive(false);
}
