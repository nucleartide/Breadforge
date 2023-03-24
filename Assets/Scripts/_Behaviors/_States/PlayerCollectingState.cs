using UnityEngine;

public class PlayerCollectingState : State
{
    [SerializeField]
    [NotNull]
    private PlayerConfiguration playerConfiguration;

    private Resource resourceBeingCollected;

    private Quaternion desiredRotation;

    private static Quaternion GetDesiredRotation(Resource resourceBeingCollected, Transform gameObject)
    {
        var toCollect = resourceBeingCollected.transform.position - gameObject.position;
        var angle = Vector3.SignedAngle(Vector3.forward, toCollect, Vector3.up);
        return Quaternion.AngleAxis(angle, Vector3.up);
    }

    public void Initialize(Resource resourceBeingCollected)
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

        if (resourceBeingCollected != null)
            resourceBeingCollected.Elapse(Time.deltaTime);
    }
}
