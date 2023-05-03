using UnityEngine;

public class SanityCheck : MonoBehaviour
{
    private void Start()
    {
        Debug.Log(transform.parent == null);
    }
}
