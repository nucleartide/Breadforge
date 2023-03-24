using UnityEngine;

public class PlayerCollectingState : State
{
    [SerializeField]
    [NotNull]
    private PlayerConfiguration playerConfiguration;

    private Transform resourceBeingCollected;

    private Quaternion desiredRotation;

    private static Quaternion GetDesiredRotation(Transform resourceBeingCollected, Transform gameObject)
    {
        var toCollect = resourceBeingCollected.position - gameObject.position;
        var angle = Vector3.SignedAngle(Vector3.forward, toCollect, Vector3.up);
        return Quaternion.AngleAxis(angle, Vector3.up);
    }

    public void Initialize(Transform resourceBeingCollected)
    {
        this.resourceBeingCollected = resourceBeingCollected;
        desiredRotation = GetDesiredRotation(resourceBeingCollected, transform);
    }

    private static void FaceDesiredOrientation(Quaternion desired, Transform player, float deltaTime, PlayerConfiguration playerConfiguration)
    {
        float singleStep = playerConfiguration.RotationSpeedDegrees * deltaTime;
        player.rotation = Quaternion.RotateTowards(player.rotation, desired, singleStep);
    }

    private void Update()
    {
        FaceDesiredOrientation(desiredRotation, transform, Time.smoothDeltaTime, playerConfiguration);
        // TODO: how to transition away from state?
    }
}
