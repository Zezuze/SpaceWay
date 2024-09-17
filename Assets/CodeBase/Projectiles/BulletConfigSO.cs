using UnityEngine;

[CreateAssetMenu(fileName = "Bullet", menuName = "Projectile/Bullet")]
public class BulletConfigSO : ScriptableObject
{
    [SerializeField] private float _bulletSpeed;
    public float BulletSpeed => _bulletSpeed;
}