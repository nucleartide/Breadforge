using UnityEngine;

[CreateAssetMenu]
public class PlayerShoulderConfiguration : ScriptableObject
{
    [SerializeField]
    public float RotationSpeed = 180f;

    [SerializeField]
    public float MinXAngle = -20f;

    [SerializeField]
    public float MaxXAngle = 40f;
}
