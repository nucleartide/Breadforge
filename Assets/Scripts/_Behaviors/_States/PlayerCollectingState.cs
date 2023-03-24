using UnityEngine;

[System.Serializable]
public class PlayerCollectingState : IState
{
    private Transform resourceBeingCollected;

    private Transform player;

    private Quaternion desiredRotation;

    private PlayerConfiguration playerConfiguration;

    private static Quaternion GetDesiredRotation(Transform resourceBeingCollected, Transform gameObject)
    {
        var toCollect = resourceBeingCollected.position - gameObject.position;
        var angle = Vector3.SignedAngle(Vector3.forward, toCollect, Vector3.up);
        return Quaternion.AngleAxis(angle, Vector3.up);
    }

    public PlayerCollectingState(Transform resourceBeingCollected, Transform player, PlayerConfiguration playerConfiguration)
    {
        this.resourceBeingCollected = resourceBeingCollected;
        this.playerConfiguration = playerConfiguration;
        this.player = player;
        desiredRotation = GetDesiredRotation(resourceBeingCollected, player);
    }

    void IState.Update()
    {
        FaceDesiredOrientation(desiredRotation, player, Time.smoothDeltaTime, playerConfiguration);
    }

    private static void FaceDesiredOrientation(Quaternion desired, Transform player, float deltaTime, PlayerConfiguration playerConfiguration)
    {
        float singleStep = playerConfiguration.RotationSpeedDegrees * deltaTime;
        player.rotation = Quaternion.RotateTowards(player.rotation, desired, singleStep);
    }
}
