using UnityEngine;

/// <summary>
/// Maintains a reference to the immediately collectable Resource.
///
/// Also updates the "E" signifier that signifies the immediately collectable Resource.
/// </summary>
public class ImmediateCollectableManager : MonoBehaviour
{
    /// <summary>
    /// The "E" text that should be shown above the immediately-collectible resource.
    /// </summary>
    [NotNull]
    [SerializeField]
    private GameObject signifierPrefab;

    [SerializeField]
    private Vector3 offset;

    [SerializeField]
    [NotNull(IgnorePrefab = true)]
    private PlayerStateMachine playerStateMachine;

    private GameObject signifier;

    private bool IsCollecting
    {
        get
        {
            return playerStateMachine.CurrentState is PlayerCollectingState;
        }
    }

    public Resource ImmediateCollectable
    {
        get;
        private set;
    }

    public void SetImmediateCollectable(Resource resource)
    {
        ImmediateCollectable = resource;
        SetSignifierPosition();
    }

    private void SetSignifierPosition()
    {
        if (signifier == null)
            signifier = Instantiate(signifierPrefab);

        signifier.SetActive(ImmediateCollectable != null && !IsCollecting);
        if (ImmediateCollectable == null)
            return;

        signifier.transform.position = ImmediateCollectable.transform.position + offset;
    }
}
