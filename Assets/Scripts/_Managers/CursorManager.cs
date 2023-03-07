using UnityEngine;

public class CursorManager : MonoBehaviour
{
    void Awake()
    {
        Cursor.lockState = CursorLockMode.Confined;
    }
}
