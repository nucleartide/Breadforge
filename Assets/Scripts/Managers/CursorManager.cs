using UnityEngine;

public class CursorManager : MonoBehaviour
{
    public void Awake() => Cursor.lockState = CursorLockMode.Locked;
}
