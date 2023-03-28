using UnityEngine;

public abstract class PlayerCollectingState : StateBehaviour
{
    [SerializeField]
    [NotNull]
    protected PlayerConfiguration playerConfiguration;

    protected Resource resourceBeingCollected;

    protected Quaternion desiredRotation;

    protected static Quaternion GetDesiredRotation(Resource resourceBeingCollected, Transform gameObject)
    {
        var toCollect = resourceBeingCollected.transform.position - gameObject.position;
        var angle = Vector3.SignedAngle(Vector3.forward, toCollect, Vector3.up);
        return Quaternion.AngleAxis(angle, Vector3.up);
    }

    protected static void FaceDesiredOrientation(Quaternion desired, Transform player, float deltaTime, PlayerConfiguration playerConfiguration)
    {
        float singleStep = playerConfiguration.RotationSpeedDegrees * deltaTime;
        player.rotation = Quaternion.RotateTowards(player.rotation, desired, singleStep);
    }

    public void Initialize(Resource resourceBeingCollected)
    {
        this.resourceBeingCollected = resourceBeingCollected;
        desiredRotation = GetDesiredRotation(resourceBeingCollected, transform);
    }

    protected void Update()
    {
        FaceDesiredOrientation(desiredRotation, transform, Time.smoothDeltaTime, playerConfiguration);

        if (resourceBeingCollected != null)
            resourceBeingCollected.Elapse(Time.deltaTime);
    }
}
