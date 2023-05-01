using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class SetPointsForLine : MonoBehaviour
{
    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private Transform _transformFastening;
    [SerializeField] private Transform _transformSecondPoint;

    private void Update()
    {
        _lineRenderer.SetPosition(0, _transformFastening.position);
        _lineRenderer.SetPosition(1, _transformSecondPoint.position);
    }
}