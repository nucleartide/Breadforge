using UnityEngine;

public class ResourceVisual : MonoBehaviour
{
    [field: NotNull]
    [field: SerializeField]
    public GameObject Visual
    {
        get;
        private set;
    }
}
