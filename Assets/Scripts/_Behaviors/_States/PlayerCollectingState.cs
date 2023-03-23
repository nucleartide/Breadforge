public class PlayerCollectingState : IState
{
    Resource resourceBeingCollected;

    public PlayerCollectingState(Resource resourceBeingCollected)
    {
        this.resourceBeingCollected = resourceBeingCollected;
    }

    void IState.Update()
    {
        throw new System.NotImplementedException();
    }
}
