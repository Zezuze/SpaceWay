using UnityEngine;

public abstract class OutOfViewChecker : MonoBehaviour
{
    protected BoxCollider2D _boxCollider2D;
    protected int _boxX;
    protected int _boxY = 10;

    public virtual void Init()
    {
        _boxCollider2D = GetComponent<BoxCollider2D>();
        _boxX = (int)Mathf.Ceil(Camera.main.orthographicSize * Camera.main.aspect * 2);
        _boxCollider2D.size = new Vector2(_boxX, _boxY);
    }

    protected abstract void OnTriggerEnter2D(Collider2D other);
}
