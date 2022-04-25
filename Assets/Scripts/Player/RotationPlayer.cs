using UnityEngine;

public class RotationPlayer : MonoBehaviour
{
    public RotationState RotationState { get; private set; } = RotationState.Idle;

    [SerializeField] private float _speedRotation = 10f;

    private readonly float _timeSetIdle = 1f;
    private float _currentTime = 0f;

    private void Start()
    {
        if (_speedRotation < 0)
            throw new System.Exception("SpeedRotation cannot be less than zero!");
    }

    private void Update()
    {
        if (_currentTime >= _timeSetIdle)
            SetIdle();
        if (Input.GetKey(KeyCode.LeftArrow))
            RotateLeft();
        if (Input.GetKey(KeyCode.RightArrow))
            RotateRight();
        _currentTime += Time.deltaTime;
    }

    private void RotateLeft() 
    {
        Vector3 leftDirection = new Vector3(0, -1, 0);

        transform.Rotate(leftDirection * _speedRotation * Time.deltaTime);
        RotationState = RotationState.Rotation;
    }

    private void RotateRight()
    {
        Vector3 rightDirection = new Vector3(0, 1, 0);

        transform.Rotate(rightDirection * _speedRotation * Time.deltaTime);
        RotationState = RotationState.Rotation;
    }

    private void SetIdle() 
    {
        RotationState = RotationState.Idle;
        _currentTime = 0f;
    }
}

public enum RotationState 
{
    Rotation,
    Idle
}