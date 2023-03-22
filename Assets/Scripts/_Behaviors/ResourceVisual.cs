using UnityEngine;

public class ResourceVisual : MonoBehaviour
{
    public GameObject Visual
    {
        get
        {
            return gameObject.transform.GetChild(0).gameObject;
        }
    }
}
