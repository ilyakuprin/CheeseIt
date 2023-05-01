using UnityEngine;

public class PlatformController : MonoBehaviour
{
    public Transform EndPoint;

    private void Start()
    {
        WorldController.instance.OnPlatformMovement += TryDelAndAddPlatform;
    }

    private void TryDelAndAddPlatform()
    {
        if (transform.position.z < WorldController.instance.PlatformRemovalCoordinateZ)
        {
            WorldController.instance.WorldBuilder.CreatRandomPlatform();
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        if (WorldController.instance != null)
        {
            WorldController.instance.OnPlatformMovement -= TryDelAndAddPlatform;
        }
    }
}
