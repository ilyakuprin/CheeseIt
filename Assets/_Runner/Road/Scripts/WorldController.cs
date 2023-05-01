using System.Collections;
using UnityEngine;

public class WorldController : MonoBehaviour
{
    public static WorldController instance;

    public WorldBuilder WorldBuilder;
    public float PlatformRemovalCoordinateZ = -10f;

    public delegate void TryToDelAndAddPlatform();
    public event TryToDelAndAddPlatform OnPlatformMovement;

    [SerializeField] private float _runningSpeed;
    private float _currentSpeed;
    private float _climp = 0.3f;
    private float _start = 0.7f;
    private float _timer = 0f;
    private float _increaseInSpeed = 1.1f;
    private float _maxSpeed = 8f;
    private int _timeToIncreaseSpeed;
    private int _periodsOfTimeIncreaseSpeed = 20;

    private void Awake()
    {
        if (WorldController.instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            WorldController.instance = this;
        }
    }

    private void Start()
    {
        _timeToIncreaseSpeed = _periodsOfTimeIncreaseSpeed;
        _currentSpeed = 0;
        StartCoroutine(OnPlatformMovementCorutine());
    }

    private void Update()
    {
        if (_runningSpeed < _maxSpeed)
        {
            _timer += Time.deltaTime;
            if (_timer / _timeToIncreaseSpeed > 1)
            {
                _runningSpeed *= _increaseInSpeed;
                _timeToIncreaseSpeed += _periodsOfTimeIncreaseSpeed;
            }
        }
        transform.position -= Vector3.forward * _currentSpeed * Time.deltaTime;
        transform.position -= Vector3.forward * _currentSpeed * Time.deltaTime;
    }

    private IEnumerator OnPlatformMovementCorutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            if (OnPlatformMovement != null)
            {
                OnPlatformMovement();
            }
        }
    }

    public void StartRun()
    {
        _currentSpeed = _start;
    }

    public void Running()
    {
        _currentSpeed = _runningSpeed;
    }

    public void Climp()
    {
        _currentSpeed = _climp;
    }

    public void StopRun()
    {
        _currentSpeed = 0;
    }

    private void OnDestroy()
    {
        WorldController.instance = null;
    }
}
