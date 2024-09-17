
public class CommonAsteroid : Asteroid
{
    protected override void InitBehaviors() =>
        SetMovementBehavior();

    private void SetMovementBehavior() =>
        _movable = new SimpleMovementBehavior(_rigidbody2D, AsteroidConfig);
}