using UnityEngine;

public class FollowPoint2 : MonoBehaviour
{
    [SerializeField] private Transform _point2;

    private Vector3 _offsetVector;

    private void Start()
    {
        float _offsetZ = 1.45f;
        _offsetVector = new Vector3(0, 0, _offsetZ);
    }

    private void Update()
    {
        transform.position = _point2.position - _offsetVector;
    }
}