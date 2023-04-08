using System;
using UnityEngine;

public abstract class PlayerCollectingState : StateBehaviour
{
    [SerializeField]
    [NotNull]
    protected PlayerConfiguration playerConfiguration;

    protected Resource resourceBeingCollected;

    protected Quaternion desiredRotation;

    public ResourceConfiguration.ResourceType ResourceType
    {
        get => resourceBeingCollected.Configuration.Type;
    }

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

    private void ResourceBeingCollected_OnCollectCompleted(object sender, EventArgs eventArgs)
    {
        OnCollectCompleted();
    }

    public void Initialize(Resource resourceBeingCollected)
    {
        this.resourceBeingCollected = resourceBeingCollected;
        resourceBeingCollected.OnCollectCompleted += ResourceBeingCollected_OnCollectCompleted;
        desiredRotation = GetDesiredRotation(resourceBeingCollected, transform);
    }

    protected virtual void OnDisable()
    {
        if (resourceBeingCollected != null)
        {
            resourceBeingCollected.OnCollectCompleted -= ResourceBeingCollected_OnCollectCompleted;
            resourceBeingCollected.ResetRemainingTime();
        }
    }

    private void Update()
    {
        FaceDesiredOrientation(desiredRotation, transform, Time.smoothDeltaTime, playerConfiguration);
    }

    protected void Collect()
    {
        if (resourceBeingCollected != null)
            resourceBeingCollected.Elapse(GetAmountCollectedPerAction());
    }

    protected abstract float GetAmountCollectedPerAction();

    protected abstract void OnCollectCompleted();
}
