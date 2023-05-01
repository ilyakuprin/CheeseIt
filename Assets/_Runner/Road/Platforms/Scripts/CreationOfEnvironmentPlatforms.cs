using System.Collections;
using UnityEngine;

public class CreationOfEnvironmentPlatforms : MonoBehaviour
{
    [SerializeField] private GameObject _scaffoldingPlatform;
    [SerializeField] int _numberOfPlatformsOnOneSide;

    private float _fenceThickness = 0.05f;
    private float _timeWaiting = 0.03f;

    private void Start()
    {
        StartCoroutine(CreatScaffoldingPlatform());
    }

    private IEnumerator CreatScaffoldingPlatform()
    {
        float coordinateXCreat = 1 + _fenceThickness;

        for (int i = 0; i < _numberOfPlatformsOnOneSide * 2; i++)
        {
            if (i % 2 == 1)
            {
                coordinateXCreat *= -1;
            }

            GameObject platform = Instantiate(_scaffoldingPlatform, transform.position, Quaternion.identity, transform);
            platform.transform.localScale = Vector3.one;
            platform.transform.localPosition = new Vector3(coordinateXCreat, 0, 0);

            if (coordinateXCreat < 0)
            {
                coordinateXCreat = coordinateXCreat * (-1) + 1;
            }
            yield return new WaitForSeconds(_timeWaiting);
        }
    }
}
