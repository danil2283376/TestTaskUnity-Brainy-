using UnityEngine;

public class WeaponBot : MonoBehaviour, IWeapon
{
    [SerializeField] private float _timeRelax = 1f;
    [SerializeField] private GameObject _bullet;
    [SerializeField] private RotateBot _rotateBot;
    [SerializeField] private int _countReboundRay = 10;
    [SerializeField] float _distanceReboundRay = 50f;

    private bool _findPlayerRebound = false;
    private float _currentTime = 0f;

    private void Start()
    {
        if (_bullet == null)
            throw new System.Exception("Need a link to the bullet");
        if (_rotateBot == null)
            throw new System.Exception("Need a link to the Script RotateBot");
        if (transform.name != "SpawnPoint_bullet")
            throw new System.Exception("Script 'Weapon' must be on the object 'SpawnPoint_bullet'");
    }

    private void Update()
    {
        _currentTime += Time.deltaTime;
    }

    private void FixedUpdate()
    {
        CheckInViewRebound(transform.position, transform.forward, _countReboundRay);
        //Debug.Log(_findPlayerRebound);
        if (_findPlayerRebound)
            Shoot();
        _findPlayerRebound = false;
    }

    private void CheckInViewRebound(Vector3 position, Vector3 direction, int countRay)
    {
        if (countRay == 0)
            return;

        Vector3 startingPosition = position;
        Ray ray = new Ray(startingPosition, direction);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, _distanceReboundRay))
        {
            direction = Vector3.Reflect(direction, hit.normal);
            position = hit.point;
        }
        else
        {
            position += direction * _distanceReboundRay;
        }

        Debug.DrawLine(startingPosition, position, Color.blue);
        if (hit.transform.GetComponent<MovePlayer>() != null)
        {
            _findPlayerRebound = true;
            return;
        }
        if (hit.transform.GetComponent<Bullet>())
            return;
        if (hit.transform.tag == "Wall")
            return;

        CheckInViewRebound(position, direction, countRay - 1);
    }

    public void Shoot()
    {
        if (_currentTime >= _timeRelax)
        {
            GameObject bullet = Instantiate(_bullet, transform.position, gameObject.transform.rotation);

            float speedBullet = bullet.GetComponent<Bullet>().Speed;

            Rigidbody rigidbody = bullet.GetComponent<Rigidbody>();

            rigidbody.AddForce(gameObject.transform.forward * speedBullet, ForceMode.VelocityChange);

            _currentTime = 0f;
        }
    }
}
