
public class CommonShip : EnemyShip
{
    protected override void InitBehaviors() =>
        SetMovementBehavior();

    private void SetMovementBehavior()
    {
        _movable = new SimpleMovementBehavior(_rigidbody2D, ShipConfig);
    }
}
