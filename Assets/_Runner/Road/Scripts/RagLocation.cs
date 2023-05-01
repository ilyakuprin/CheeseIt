using UnityEngine;
using System.Collections;

public class RagLocation : MonoBehaviour
{
    private float _localLocationZFrom = 1f;
    private float _localLocationZTo = 9f;
    private float _probabilityOfRag = 0.3f;

    private void Start()
    {
        StartCoroutine(CreatAndSetPosition());
    }

    private IEnumerator CreatAndSetPosition()
    {
        var random = new System.Random();

        var random0To1 = random.NextDouble();
        if (random0To1 <= _probabilityOfRag)
        {
            gameObject.SetActive(true);

            var positionZ = random.NextDouble() * (_localLocationZFrom - _localLocationZTo) + _localLocationZTo;
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, (float)positionZ);
        }
        else
        {
            gameObject.SetActive(false);
        }

        yield return null;
    }
}
