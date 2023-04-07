using System;
using UnityEngine;

public abstract class PlayerCollectingState : StateBehaviour
{
    [SerializeField]
    [NotNull]
    protected PlayerConfiguration playerConfiguration;

    protected Resource resourceBeingCollected;

    public ResourceConfiguration.ResourceType ResourceType
    {
        get => resourceBeingCollected.Configuration.Type;
    }

    protected Quaternion desiredRotation;

    protected float resourceCollectTime = 0f;

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

    protected virtual void OnDisable()
    {
        resourceBeingCollected.OnCollectCompleted -= ResourceBeingCollected_OnCollectCompleted;
    }

    private void ResourceBeingCollected_OnCollectCompleted(object sender, EventArgs eventArgs) => OnCollectCompleted();

    public void Initialize(Resource resourceBeingCollected)
    {
        this.resourceBeingCollected = resourceBeingCollected;
        resourceBeingCollected.OnCollectCompleted += ResourceBeingCollected_OnCollectCompleted;
        desiredRotation = GetDesiredRotation(resourceBeingCollected, transform);
    }

    private void Update()
    {
        FaceDesiredOrientation(desiredRotation, transform, Time.smoothDeltaTime, playerConfiguration);
        UpdateResourceCollection();
    }

    protected virtual void UpdateResourceCollection()
    {
        if (resourceBeingCollected != null)
            resourceCollectTime += Time.deltaTime;
        else
            resourceCollectTime = 0f;
    }

    protected abstract void OnCollectCompleted();

    protected void Collect()
    {
        if (resourceBeingCollected != null)
        {
            resourceBeingCollected.Elapse(resourceCollectTime);
            resourceCollectTime = 0f;
        }
    }
}
