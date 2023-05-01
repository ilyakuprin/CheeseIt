using UnityEngine;

public class CreatingEnvironment : MonoBehaviour
{
    [SerializeField] private GameObject[] _objects;
    [SerializeField] private int _maxNumberUnitsGrass;
    [SerializeField] private int _minNumberUnitsGrass;

    private void Start()
    {
        var random = new System.Random();
        int countUnitsGrass = random.Next(_minNumberUnitsGrass, _maxNumberUnitsGrass);

        for (int i = 0; i < countUnitsGrass; i++)
        {
            int index = random.Next(0, _objects.Length);

            GameObject objectEnvironment = Instantiate(_objects[index], transform.position, Quaternion.identity, transform);

            Vector3 _localScaleObject = Vector3.one;
            Component[] array = GetComponentsInParent<MeshRenderer>();
            foreach (var item in array)
            {
                _localScaleObject = new Vector3(_localScaleObject.x / item.gameObject.transform.localScale.x,
                                               _localScaleObject.y / item.gameObject.transform.localScale.y,
                                               _localScaleObject.z / item.gameObject.transform.localScale.z);
            }
            objectEnvironment.transform.localScale = _localScaleObject;

            double coordinateX = random.NextDouble() * (-1) + 0.5;
            double coordinateZ = random.NextDouble() * (-1) + 0.5;

            float halfHightParant = 0.5f;
            objectEnvironment.transform.localPosition = new Vector3((float)coordinateX, halfHightParant, (float)coordinateZ);
        }
    }
}
