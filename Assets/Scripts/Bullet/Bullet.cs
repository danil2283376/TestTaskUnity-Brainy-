using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _speed = 10f;
    [SerializeField] private GameObject _gunner;

    private Rigidbody _bullet;

    public float Speed
    {
        get
        {
            return _speed;
        }
        private set
        {
            if (value <= 0)
                throw new System.Exception("bullet speed must be greater than zero");
            else
                _speed = value;
        }
    }

    void Start()
    {
        _bullet = transform.GetComponent<Rigidbody>();

        if (_bullet == null)
            throw new System.Exception("Need set link on Rigidbody on Bullet!");
        if (_speed <= 0 )
            throw new System.Exception("Need a link to the bullet");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.GetComponent<Rigidbody>() != null)
        {
            IHealthPoint healthPoint = collision.gameObject.GetComponent<IHealthPoint>();

            if (healthPoint != null)
                healthPoint.Dead();
            Destroy(gameObject);
        }
        if (collision.transform.tag == "Wall")
            Destroy(gameObject);
        //Отскок
        if (collision.transform.tag == "Obstacle")
        {
            Vector3 newDirection = Vector3.Reflect(transform.forward, collision.contacts[0].normal);

            transform.rotation = Quaternion.LookRotation(newDirection);

            _bullet.velocity = transform.forward * _speed;
        }
    }
}
