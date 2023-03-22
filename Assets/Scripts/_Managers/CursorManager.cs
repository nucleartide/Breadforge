using UnityEngine;

[CreateAssetMenu]
public class CursorManager : Manager
{
    public override void OnManualEnable() => Cursor.lockState = CursorLockMode.Locked;
}
