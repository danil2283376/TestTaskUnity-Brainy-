using UnityEngine;

public class WeaponPlayer : MonoBehaviour, IWeapon
{
    public bool Shooting { get; private set; } = false;
    public bool ReboundShot { get; private set; } = false;
    public Bullet CreateBullet { get; private set; } = null;

    [SerializeField] private GameObject _prefabBullet;

    private readonly float _timeNotShooting = 1f;
    private readonly float _distanceRay = 50f;
    private float _currentTime = 0f;

    private void Start()
    {
        if (_prefabBullet == null)
            throw new System.Exception("Need a link to the bullet");
        if (transform.name != "SpawnPoint_bullet")
            throw new System.Exception("Script 'Weapon' must be on the object 'SpawnPoint_bullet'");
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space))
            Shoot();
        if (Shooting)
            SetNotShooting();

        _currentTime += Time.deltaTime;
    }

    private void SetNotShooting()
    {
        if (_currentTime >= _timeNotShooting)
        {
            Shooting = false;
            ReboundShot = false;
            _currentTime = 0f;
        }
    }

    public void Shoot()
    {
        Shooting = true;
        CheckReboundShot();

        GameObject bullet = Instantiate(_prefabBullet, transform.position, gameObject.transform.rotation);

        Rigidbody rigidbody = bullet.GetComponent<Rigidbody>();

        CreateBullet = bullet.GetComponent<Bullet>();

        float speed = CreateBullet.Speed;

        rigidbody.AddForce(gameObject.transform.forward * speed, ForceMode.VelocityChange);
    }

    private void CheckReboundShot()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, _distanceRay))
        {
            // встретили препятсвие
            if (hit.transform.tag == "Obstacle")
            {
                ReboundShot = true;
            }
        }
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * _distanceRay, Color.red);
    }
}
