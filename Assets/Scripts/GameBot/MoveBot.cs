using UnityEngine;

public class MoveBot : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;

    [SerializeField] private int _startFildX;
    [SerializeField] private int _endFildX;

    [SerializeField] private int _startFildZ;
    [SerializeField] private int _endFildZ;

    private Rigidbody _bot;
    private Vector3 _tempPosition = Vector3.zero;
    private RotateBot _rotateBot;

    private readonly float _distanceRay = 2f;
    private void Start()
    {
        _bot = transform.GetComponent<Rigidbody>();
        _rotateBot = transform.GetComponent<RotateBot>();

        if (_bot == null)
            throw new System.Exception("Not find Rigidbody on bot!");
        if (_rotateBot == null)
            throw new System.Exception("Not find RotateBot on bot!");
        SetPathAI();
    }

    private void Update()
    {
        if (_tempPosition != Vector3.zero)
            MoveToPoint();

        CheckFinishPath();
    }

    private void FixedUpdate()
    {
        CheckObstacle();
    }

    private void SetPathAI()
    {
        _tempPosition = new Vector3(
            Random.Range(_startFildX, _endFildX),
            transform.position.y,
            Random.Range(_startFildZ, _endFildZ));
    }

    private void MoveToPoint()
    {
        transform.position = Vector3.MoveTowards(transform.position, _tempPosition, _speed * Time.deltaTime);
        if (!_rotateBot.PlayerFind)
            transform.LookAt(_tempPosition);
    }

    private void CheckFinishPath()
    {
        // бот дошел до конечной точки
        if (transform.position == _tempPosition)
        {
            _tempPosition = Vector3.zero;
            SetPathAI();
        }
    }

    private void CheckObstacle() 
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, _distanceRay))
        {
            // встретили препятсвие
            if (hit.transform.GetComponent<BoxCollider>())
            {
                SetPathAI();
            }
        }
        if (Physics.Raycast(transform.position, transform.TransformDirection(-Vector3.forward), out hit, _distanceRay))
        {
            // встретили препятсвие
            if (hit.transform.GetComponent<BoxCollider>())
            {
                SetPathAI();
            }
        }
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * _distanceRay, Color.red);
    }
}
