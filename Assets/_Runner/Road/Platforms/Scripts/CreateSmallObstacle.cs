using System.Reflection;
using UnityEngine;

public class CreateSmallObstacle : MonoBehaviour
{
    [SerializeField] private GameObject[] _rocks;
    [SerializeField] private float _startLocalCoordinateX;
    [SerializeField] private int _numberOfRock;

    private Vector3 _localScaleRock = new Vector3(0.375f, 1.5625f, 2.941177f);

    private void Start()
    {
        SetCordinatesBox();
        CreatRocksInBox();
    }

    private void SetCordinatesBox()
    {
        var random = new System.Random();

        float[] arrayPositionX = new float[] { -transform.localScale.x, 0, transform.localScale.x };
        float positionX = arrayPositionX[random.Next(arrayPositionX.Length)];

        // 0.5 = transform.parent.transform.localScale / transform.parent.transform.localScale / 2.
        double platformBoundary = 0 + 0.5 - transform.localScale.z / 2;
        // random.NextDouble() * (a - b) + b; a = platformBoundary, b = -platformBoundary.
        double positionZ = random.NextDouble() * (platformBoundary - (-platformBoundary)) + (-platformBoundary);

        transform.localPosition = new Vector3(positionX, transform.localPosition.y, (float)positionZ);
    }

    private void CreatRocksInBox()
    {
        var random = new System.Random();
        var positionRock = new Vector3(_startLocalCoordinateX, 0, 0);
        float displacementDistance = _startLocalCoordinateX * (-2) / (_numberOfRock - 1);
        int previousIndex = -1;

        for (int i = 0; i < _numberOfRock; i++)
        {
            int index = random.Next(0, _rocks.Length);
            if (previousIndex == index)
            {
                if (index != _rocks.Length - 1)
                {
                    index += 1;
                }
                else
                {
                    index -= 1;
                }
            }
            previousIndex = index;

            GameObject rock = Instantiate(_rocks[index], transform.position, Quaternion.identity, transform);
            rock.transform.localScale = _localScaleRock;
            rock.transform.localPosition = positionRock;

            positionRock += new Vector3(displacementDistance, 0, 0);
        }
    }
}
