using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "Enemy/Enemy")]
public class EnemyConfigSO : ScriptableObject
{
    [SerializeField, Min(0)] private float _speed;
    [SerializeField, Min(0)] private int _health;
    public float Speed => _speed;
    public int Health => _health;
}
