using System.Collections;
using UnityEngine;

public class WorldBuilder : MonoBehaviour
{
    [SerializeField] private GameObject[] _obstaclePlatforms;
    [SerializeField] private double[] _likelihoodOfPlatforms;
    [SerializeField] private Transform _platformContainer;

    private Transform _lastPlatform = null;
    private float _timeWaiting = 0.03f;
    private int _indexFreePlatform = 0;
    private int _indexWallPlatform = 1;
    private int _indexSlideObstacle = 4;
    private bool _creatingWall = false;
    private bool _creatingSlide = false;

    private void Start()
    {
        int numberOfFreePlatforms = 3; 
        int numberOfObstPlatforms = 10; 
        StartCoroutine(CreatStartPlatforms(numberOfFreePlatforms, numberOfObstPlatforms));
    }    

    private IEnumerator CreatStartPlatforms(int countFreePlatforms, int numberOfObstPlatforms)
    {
        for (int i = 0; i < countFreePlatforms; i++)
        {
            CreatPlatforms(_indexFreePlatform);
            yield return new WaitForSeconds(_timeWaiting);
        }
        for (int i = 0; i < numberOfObstPlatforms; i++)
        {
            CreatRandomPlatform();
            yield return new WaitForSeconds(_timeWaiting);
        }
    }

    public void CreatRandomPlatform()
    {
        CreatPlatforms(SetIndexAfterConditionCheck(GetRandomIndex()));
    }

    private int GetRandomIndex()
    {
        var random0To1 = new System.Random().NextDouble();
        double total = 0;
        for (int i = 0; i < _obstaclePlatforms.Length; i++)
        {
            total += _likelihoodOfPlatforms[i];
            if (total >= random0To1)
            {
                return i;
            }
        }
        return 0;
    }

    private int SetIndexAfterConditionCheck(int index)
    {
        if (_creatingSlide)
        {
            _creatingSlide = false;
            return _indexFreePlatform;
        }
        else if (_creatingWall && index != _indexWallPlatform)
        {
            _creatingWall = false;
            return _indexFreePlatform;
        }
        else if (index == _indexSlideObstacle)
        {
            _creatingSlide = true;
        }
        else if (index == _indexWallPlatform)
        {
            _creatingWall = true;
        }
        return index;
    }

    private void CreatPlatforms(int index)
    {
        Vector3 position;
        if (_lastPlatform == null)
        {
            position = _platformContainer.position;
        }
        else
        {
            position = _lastPlatform.GetComponent<PlatformController>().EndPoint.position;
            position += new Vector3(0, 0, _obstaclePlatforms[index].transform.localScale.z / 2);
        }
   
        GameObject result = Instantiate(_obstaclePlatforms[index], position, Quaternion.identity, _platformContainer);
        _lastPlatform = result.transform;
    }
}
