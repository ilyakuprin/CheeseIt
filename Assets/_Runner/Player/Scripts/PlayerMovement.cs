using System.Collections;
using UnityEngine;

[RequireComponent(typeof(HeashingLayerName), typeof(HeashingAnimationName))]
[RequireComponent(typeof(Animator), typeof(AnimationsName))]
[RequireComponent(typeof(CapsuleCollider), typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private WorldController _worldController;
    [SerializeField] private Animator _animator;
    [SerializeField] private CapsuleCollider _capsuleCollider;
    [SerializeField] private AnimationsName _animsClip;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private HeashingLayerName _heashingLayerName;
    [SerializeField] private HeashingAnimationName _heashingAnimationName;
    [SerializeField] private CanvasConrollerInGame _canvasConrollerInGame;

    // The sum of the array elements must be equal to one.
    private readonly float[] _jumpColliderBehaviorFraction = new float[3] { 0.35f, 0.30f, 0.35f };
    private readonly float[] _slideColliderBehaviorFraction = new float[3] { 0.1f, 0.6f, 0.3f };
    private readonly float _horizontalMovementLength = 3f;
    private readonly float _jumpHeight = 1.5f;
    private readonly float _minJumpHeightCollider = 1f;
    private readonly float _maxJumpCenterY = 2f;
    private readonly float _minSlideHeightCollider = 0.7f;
    private readonly float _minSlideCenterY = 0.4f;
    private readonly float _climpLength = 4f;
    private readonly float _timeFallFromJumpObstacle = 0.4f;
    private readonly float _wallDeathHitDistance = 1f;

    private GameObject _collisionDeath;
    private float _startCoordinateY;
    private float _directionHorizontal = 0;
    private float _directionVertical = 0;
    private byte _threePosition = 1;
    private bool _isInMovement = false;
    private bool _isColliderHeightChange = false;
    private bool _isColliderCenterChange = false;
    private bool _pressSpace = false;
    private bool _canClimp = false;
    private bool _isClimp = false;
    private bool _onWall = false;

    private void Start()
    {
        _rigidbody.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;
        _rigidbody.useGravity = false;

        _startCoordinateY = transform.position.y;

        StartCoroutine(StartGame());
    }

    private void Update()
    {
        if (!_isInMovement && !_isColliderHeightChange && !_isColliderCenterChange)
        {
            PlayerInput();

            if (_directionHorizontal != 0 || _directionVertical != 0 || _pressSpace)
            {
                HorizontalDirection();
                if (!_isInMovement)
                {
                    VerticalDirection();
                }
            }
        }

        DeathCheck();
    }

    private void PlayerInput()
    {
        _directionHorizontal = Input.GetAxisRaw("Horizontal");
        _directionVertical = Input.GetAxisRaw("Vertical");
        _pressSpace = Input.GetButton("Jump");
    }

    private void HorizontalDirection()
    {
        if (_directionHorizontal > 0)
        {
            if (_threePosition < 2)
            {
                _animator.SetTrigger(_heashingAnimationName.Right);
                _threePosition += 1;
                StartCoroutine(LinearMovement(Vector3.right, _horizontalMovementLength, _animsClip.TimeHorizontalMoving));
            }
            else
            {
                _animator.SetTrigger(_heashingAnimationName.Right);
                StartCoroutine(LinearMovement(Vector3.right, _horizontalMovementLength, _animsClip.TimeHorizontalMoving));
            }
        }
        else if (_directionHorizontal < 0)
        {
            if (_threePosition > 0)
            {
                _animator.SetTrigger(_heashingAnimationName.Left);
                _threePosition -= 1;
                StartCoroutine(LinearMovement(Vector3.left, _horizontalMovementLength, _animsClip.TimeHorizontalMoving));
            }
            else
            {
                _animator.SetTrigger(_heashingAnimationName.Left);
                StartCoroutine(LinearMovement(Vector3.left, _horizontalMovementLength, _animsClip.TimeHorizontalMoving));
            }
        }
    }

    private void VerticalDirection()
    {
        if (_directionVertical > 0)
        {
            _animator.SetTrigger(_heashingAnimationName.Up);
            float timeAnim = _animsClip.TimeJump;
            StartCoroutine(Jump(_jumpHeight));
            StartCoroutine(ColliderHeightChanging(timeAnim, _minJumpHeightCollider, _jumpColliderBehaviorFraction));
            StartCoroutine(ColliderCenterChanging(timeAnim, _maxJumpCenterY, _jumpColliderBehaviorFraction));
        }
        else if (_directionVertical < 0)
        {
            _animator.SetTrigger(_heashingAnimationName.Down);
            float timeAnim = _animsClip.TimeSlide;
            StartCoroutine(IsSlide(timeAnim));
            StartCoroutine(ColliderHeightChanging(timeAnim, _minSlideHeightCollider, _slideColliderBehaviorFraction));
            StartCoroutine(ColliderCenterChanging(timeAnim, _minSlideCenterY, _jumpColliderBehaviorFraction));
        }
        else if (_pressSpace && _canClimp)
        {
            _animator.SetTrigger(_heashingAnimationName.Climp);
            _isClimp = true;
            _worldController.Climp();
            float timeAnim = _animsClip.TimeClimp;
            StartCoroutine(LinearMovement(Vector3.up, _climpLength, timeAnim));
        }
    }

    private void DeathCheck()
    {
        if (_collisionDeath != null && _collisionDeath.layer != _heashingLayerName.Default)
        {
            StopAllCoroutines();

            StartCoroutine(Death());

            enabled = false;

            _collisionDeath = null;
        }
    }

    private IEnumerator Death()
    {
        _worldController.StopRun();

        _capsuleCollider.isTrigger = true;
        yield return new WaitForFixedUpdate();
        _animator.enabled = false;

        _canvasConrollerInGame.Death();
    }

    private IEnumerator DeathFromWall()
    {
        if (_onWall == false && _startCoordinateY < transform.position.y)
        {
            StartCoroutine(LinearMovement(Vector3.down, transform.position.y - _startCoordinateY, _animsClip.TimeDeathFromWall));
        }

        float timeWait = _animsClip.TimeDeathFromWall / 7;
        yield return new WaitForSeconds(timeWait);
        StartCoroutine(LinearMovement(Vector3.back, _wallDeathHitDistance, _animsClip.TimeDeathFromWall - timeWait));
    }


    private IEnumerator LinearMovement(Vector3 direction, float distance, float timeAnim)
    {
        _isInMovement = true;

        float time = _animsClip.TimeHorizontalMoving;
        float speed = distance / time;
        float _currentDistance = distance;
       
        while (_currentDistance > 0)
        {
            float currentSpeed = speed * Time.deltaTime;
            transform.position += direction * currentSpeed;
            _currentDistance -= currentSpeed;
            yield return null;
        }

        if (_isClimp)
        {
            _worldController.Running();
            _isClimp = false;
            _onWall = true;
            StartCoroutine(FallOffWall());
        }

        yield return _isInMovement = false;
    }

    private IEnumerator Jump(float height)
    {
        _isInMovement = true;

        float time = _animsClip.TimeJump / 2;
        float speed = height / time;
        float _currentDistance = height;
        
        for (int i = 1; i > -2; i--)
        {
            if(i == 0)
            {
                continue;
            }

            while (_currentDistance > 0)
            {
                float currentSpeed = speed * Time.deltaTime;
                transform.position += Vector3.up * currentSpeed * i; 
                _currentDistance -= currentSpeed;
                yield return null;
            }

            _currentDistance = height;
        }

        yield return _isInMovement = false;
    }

    private IEnumerator ColliderHeightChanging(float timeAnim, float minHeight, float[] colliderBehavior)
    {
        _isColliderHeightChange = true;

        float startHeight = _capsuleCollider.height;

        float time = timeAnim;
        float currentDistance = _capsuleCollider.height - minHeight;
        float speed = currentDistance / (time * colliderBehavior[0]);
        
        while (currentDistance > 0)
        {
            float currentSpeed = speed * Time.deltaTime;
            _capsuleCollider.height -= currentSpeed;
            currentDistance -= currentSpeed;
            yield return null;
        }

        yield return new WaitForSeconds(time * colliderBehavior[1]);

        currentDistance = startHeight - minHeight;
        speed = currentDistance / (time * colliderBehavior[2]);

        while (currentDistance > 0)
        {
            float currentSpeed = speed * Time.deltaTime;
            _capsuleCollider.height += currentSpeed;
            currentDistance -= currentSpeed;
            yield return null;
        }

        _capsuleCollider.height = startHeight;
        yield return _isColliderHeightChange = false;
    }

    private IEnumerator ColliderCenterChanging(float timeAnim, float centerY, float[] colliderBehavior)
    {
        _isColliderCenterChange = true; 

        Vector3 startCenter = _capsuleCollider.center;
        float colliderMovementDirection;
        if (startCenter.y < centerY)
        {
            colliderMovementDirection = 1;
        }
        else
        {
            colliderMovementDirection = -1;
        }

        float time = timeAnim;
        float currentDistance = (centerY - startCenter.y) * colliderMovementDirection;
        float speed = currentDistance / (time * colliderBehavior[0]);
        
        while (currentDistance > 0)
        {
            float currentSpeed = speed * Time.deltaTime;
            _capsuleCollider.center += new Vector3(0, currentSpeed * colliderMovementDirection, 0);
            currentDistance -= currentSpeed;
            yield return null;
        }

        yield return new WaitForSeconds(time * colliderBehavior[1]);

        currentDistance = (startCenter.y - centerY) * colliderMovementDirection;
        speed = currentDistance / (time * colliderBehavior[2]);

        while (currentDistance > 0)
        {
            float currentSpeed = speed * Time.deltaTime;
            _capsuleCollider.center -= new Vector3(0, currentSpeed * colliderMovementDirection, 0);
            currentDistance -= currentSpeed;
            yield return null;
        }

        _capsuleCollider.center = startCenter;
        yield return _isColliderCenterChange = false;
    }

    private IEnumerator FallOffWall()
    {
        float numberOfSecondsFullyClimb = 1f;
        yield return new WaitForSeconds(numberOfSecondsFullyClimb);

        RaycastHit _raycast;
        Ray _ray = new Ray(transform.position, Vector3.down);

        while (true)
        {
            Physics.Raycast(_ray, out _raycast, _jumpHeight);

            if (_raycast.collider == null)
            {
                _animator.SetTrigger(_heashingAnimationName.Drop);
                StartCoroutine(LinearMovement(Vector3.down, _climpLength, _animsClip.TimeClimp));
                break;
            }

            yield return null;
        }
        _onWall = false;
        yield return null;
    }

    private IEnumerator StartGame()
    {
        _animator.SetTrigger(_heashingAnimationName.StartRun);
        _worldController.StartRun();
        enabled = false;
        float _time = _animsClip.TimeStart;
        yield return new WaitForSeconds(_time);
        _worldController.Running();
        script.enabled = true;
    }

    private IEnumerator DeathFromJumpObstacle()
    {
        yield return new WaitForSeconds(_timeFallFromJumpObstacle);
        _worldController.StopRun();
    }

    private IEnumerator IsSlide(float timeAnim)
    {
        yield return new WaitForSeconds(timeAnim);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!_isClimp)
        {
            _collisionDeath = collision.gameObject;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        _canClimp = true;
    }
    private void OnTriggerExit(Collider other)
    {
        _canClimp = false;
    }
}
