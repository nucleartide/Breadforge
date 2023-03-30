using UnityEngine;

[CreateAssetMenu]
public class PlayerConfiguration : ScriptableObject
{
    public float PlayerWalkSpeed;

    public float PlayerRunSpeed;

    /// <summary>
    /// In units per second squared.
    /// </summary>
    public float GravityValue;

    /// <summary>
    /// In radians per second.
    /// </summary>
    public float RotationSpeed;

    /// <summary>
    /// Rotation speed, but in degrees
    /// </summary>
    public float RotationSpeedDegrees
    {
        get => RotationSpeed * Mathf.Rad2Deg;
    }
}
