using System;
using UnityEngine;

public abstract class PlayerCollectingState : StateBehaviour
{
    [SerializeField]
    [NotNull]
    protected PlayerConfiguration playerConfiguration;

    [SerializeField]
    [NotNull(IgnorePrefab = true)]
    protected PlayerMovingState playerMovingState;

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

    // problem: OnDepleted is called first, and thus the animation event doesn't occur
    private void ResourceBeingCollected_OnDepleted(object sender, EventArgs eventArgs)
    {
        TransitionTo(playerMovingState);
    }

    public void Initialize(Resource resourceBeingCollected)
    {
        this.resourceBeingCollected = resourceBeingCollected;
        resourceBeingCollected.OnCollectCompleted += ResourceBeingCollected_OnCollectCompleted;
        resourceBeingCollected.OnDepleted += ResourceBeingCollected_OnDepleted;
        desiredRotation = GetDesiredRotation(resourceBeingCollected, transform);
    }

    protected virtual void OnDisable()
    {
        if (resourceBeingCollected != null)
        {
            resourceBeingCollected.OnCollectCompleted -= ResourceBeingCollected_OnCollectCompleted;
            resourceBeingCollected.OnDepleted -= ResourceBeingCollected_OnDepleted;
            resourceBeingCollected.ResetRemainingTime();
        }
    }

    private void Update()
    {
        FaceDesiredOrientation(desiredRotation, transform, Time.deltaTime, playerConfiguration);
    }

    protected void Collect()
    {
        if (resourceBeingCollected != null)
            resourceBeingCollected.Elapse(GetAmountCollectedPerAction());
    }

    protected abstract float GetAmountCollectedPerAction();

    protected abstract void OnCollectCompleted();
}
