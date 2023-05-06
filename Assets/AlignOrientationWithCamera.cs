using UnityEngine;
using UnityEngine.Assertions;

public class AlignOrientationWithCamera : MonoBehaviour
{
    private void Update()
    {
        Assert.IsNotNull(Camera.main);
        transform.rotation = Camera.main.transform.rotation;
    }
}
