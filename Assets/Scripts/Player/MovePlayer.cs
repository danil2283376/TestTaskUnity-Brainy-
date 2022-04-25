using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    public MoveState MoveState { get; private set; } = MoveState.Idle;
    public DirectionState DirectionState { get; private set; } = DirectionState.Idle;

    [SerializeField] private float _speed = 5f;

    private readonly float _timeSetIdle = 1f;

    private Rigidbody _player;
    private float _currentTime = 0f;

    private void Start()
    {
        _player = gameObject.GetComponent<Rigidbody>();

        if (_player == null)
            throw new System.Exception("Not find Rigidbody on player!");
        if (_speed < 0)
            throw new System.Exception("Speed cannot be less than zero!");
    }

    private void FixedUpdate()
    {
        _player.velocity = Vector3.zero;

        if (_currentTime >= _timeSetIdle)
            SetIdle();
        if (Input.GetKey(KeyCode.W))
            MoveForward();
        if (Input.GetKey(KeyCode.S))
            MoveBack();
        if (Input.GetKey(KeyCode.A))
            MoveLeft();
        if (Input.GetKey(KeyCode.D))
            MoveRight();

        _currentTime += Time.deltaTime;
    }

    private void MoveForward()
    {
        Vector3 movement = new Vector3(transform.forward.x, 0.0f, transform.forward.z);

        _player.AddForce(movement * _speed, ForceMode.Impulse);
        DirectionState = DirectionState.MovingVertical;
        MoveState = MoveState.Move;
    }

    private void MoveBack()
    {
        Vector3 movement = new Vector3(-transform.forward.x, 0.0f, -transform.forward.z);

        _player.AddForce(movement * _speed, ForceMode.Impulse);
        DirectionState = DirectionState.MovingVertical;
        MoveState = MoveState.Move;
    }

    private void MoveLeft()
    {
        _player.AddForce(-transform.right * _speed, ForceMode.Impulse);
        DirectionState = DirectionState.MovingHorizontal;
        MoveState = MoveState.Move;
    }

    private void MoveRight()
    {
        _player.AddForce(transform.right * _speed, ForceMode.Impulse);
        DirectionState = DirectionState.MovingHorizontal;
        MoveState = MoveState.Move;
    }

    private void SetIdle()
    {
        MoveState = MoveState.Idle;
        DirectionState = DirectionState.Idle;
        _currentTime = 0f;
    }

}

public enum MoveState 
{
    Idle,
    Move
}

public enum DirectionState
{
    MovingHorizontal,
    MovingVertical,
    Idle
}