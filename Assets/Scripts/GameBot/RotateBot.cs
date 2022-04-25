using UnityEngine;

public class RotateBot : MonoBehaviour
{
    public bool PlayerFind { get; private set; } = false;

    [Range(0, 360)] [SerializeField] private float _viewAngle = 90f;
    [SerializeField] private float _viewDistance = 5f;
    [SerializeField] private int _countRay = 10;
    [SerializeField] private GameObject _player;
    [SerializeField] private WeaponBot _weaponBot;



    private void Start()
    {
        if (_player == null)
            throw new System.Exception("Need set link on Player!");
        if (_weaponBot == null)
            throw new System.Exception("Need set weapon Bot!");
    }

    private void FixedUpdate()
    {
        if (IsInView())
        {
            transform.LookAt(_player.transform);
            _weaponBot.Shoot();
        }
    }

    private bool IsInView()
    {
        bool result = false;
        float startPointOnCircle = -0.75f;
        for (int i = 0; i < _countRay; i++)
        {
            float x = Mathf.Sin(startPointOnCircle);
            float y = Mathf.Cos(startPointOnCircle);

            startPointOnCircle += _viewAngle * Mathf.Deg2Rad / _countRay;

            Vector3 direction = transform.TransformDirection(new Vector3(x, 0, y));
            if (GetRaycast(direction))
                result = true;
        }
        PlayerFind = result;
        return result;
    }

    private bool GetRaycast(Vector3 direction)
    {
        bool result = false;
        RaycastHit hit = new RaycastHit();
        Vector3 position = transform.position;

        if (Physics.Raycast(position, direction, out hit, _viewDistance))
        {
            if (hit.transform.GetComponent<MovePlayer>() != null)
            {
                result = true;
                Debug.DrawLine(position, hit.point, Color.green);
            }
            else
            {
                Debug.DrawLine(position, hit.point, Color.blue);
            }
        }
        else
        {
            Debug.DrawRay(position, direction * _viewDistance, Color.red);
        }
        return result;
    }
}
