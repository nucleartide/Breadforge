using UnityEngine;

public class PlayerCollectingState : IState
{
    Resource resourceBeingCollected;

    public Quaternion DesiredRotation
    {
        get;
        private set;
    }

    public PlayerCollectingState(Resource resourceBeingCollected, GameObject player)
    {
        this.resourceBeingCollected = resourceBeingCollected;
        DesiredRotation = GetDesiredRotation(resourceBeingCollected.gameObject, player);
    }

    void IState.Update()
    {
        throw new System.NotImplementedException();

    }

    private static Quaternion GetDesiredRotation(GameObject resourceBeingCollected, GameObject gameObject)
    {
        var toCollect = resourceBeingCollected.transform.position - gameObject.transform.position;
        var angle = Vector3.SignedAngle(Vector3.forward, toCollect, Vector3.up);
        return Quaternion.AngleAxis(angle, Vector3.up);
    }
}
