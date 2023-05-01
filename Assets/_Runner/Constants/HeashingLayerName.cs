using UnityEngine;

public class HeashingLayerName : MonoBehaviour
{
    [HideInInspector] public int Default;

    private void Start()
    {
        Default = LayerMask.NameToLayer("Default");
    }
}
