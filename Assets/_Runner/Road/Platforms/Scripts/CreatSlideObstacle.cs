using UnityEngine;

public class CreatSlideObstacle : MonoBehaviour
{
    private void Start()
    {
        SetCordinates();
    }

    private void SetCordinates()
    {
        var random = new System.Random();

        // 0.5 = transform.parent.transform.localScale / transform.parent.transform.localScale / 2.
        double platformBoundary = 0.5 - transform.localScale.z / 2;
        // random.NextDouble() * (a - b) + b; a = -platformBoundary, b = 0;
        double positionZ = random.NextDouble() * (-platformBoundary);

        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, (float)positionZ);
    }
}
