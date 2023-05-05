using UnityEngine;
using System.Collections.Generic;
using System;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(CapsuleCollider))]
public class PlayerStateMachine : StateMachineBehaviour
{
    [SerializeField]
    [NotNull(IgnorePrefab = true)]
    private GameInputManager gameInput;

    [SerializeField]
    [NotNull(IgnorePrefab = true)]
    private ImmediateCollectableManager immediateCollectable;

    [SerializeField]
    [NotNull]
    private PlayerMovingState playerMovingState;

    [SerializeField]
    [NotNull]
    private CharacterController characterController;

    private bool isCollidingWithResource = false;

    private bool didCollideWithResource = false;

    public event EventHandler OnResourceCollisionEnter;

    public event EventHandler OnResourceCollisionExit;

    public event EventHandler OnNothingToMine;

    private void OnEnable()
    {
        gameInput.OnCollectStarted += GameInput_OnCollectStarted;
        gameInput.OnCollectCanceled += GameInput_OnCollectCanceled;
    }

    private void OnDisable()
    {
        gameInput.OnCollectStarted -= GameInput_OnCollectStarted;
        gameInput.OnCollectCanceled -= GameInput_OnCollectCanceled;
    }

    [System.Serializable]
    public class ResourceToPlayerState
    {
        [NotNull]
        public ResourceConfiguration Resource;

        [NotNull]
        public PlayerCollectingState PlayerState;
    }

    [SerializeField]
    private List<ResourceToPlayerState> resourcesToPlayerStates = new List<ResourceToPlayerState>();

    private PlayerCollectingState GetPlayerState(ResourceConfiguration resource)
    {
        foreach (var mapping in resourcesToPlayerStates)
            if (mapping.Resource == resource)
                return mapping.PlayerState;

        throw new System.Exception($"Resource {resource.name} does not have a corresponding PlayerCollectingState. Please double-check the resourcesToPlayerStates mapping, then try again.");
    }

    private void GameInput_OnCollectStarted(object sender, GameInputManager.GameInputArgs args)
    {
        var resource = immediateCollectable.ImmediateCollectable;
        if (resource == null)
        {
            OnNothingToMine?.Invoke(this, EventArgs.Empty);
            return;
        }

        // Given a Resource, fetch the corresponding player state.
        var collectingState = GetPlayerState(resource.Configuration);

        // Perform state transition.
        collectingState.Initialize(resource);
        TransitionTo(collectingState);
    }

    private void GameInput_OnCollectCanceled(object sender, GameInputManager.GameInputArgs args)
    {
        TransitionTo(playerMovingState);
    }

    public bool IsOverRock
    {
        get;
        private set;
    }

    private static bool UpdateIsOverRock(Transform transform, CharacterController characterController)
    {
        var characterControllerCenter = transform.position + characterController.center;
        var hits = Physics.RaycastAll(characterControllerCenter, -transform.up, 5f);

        foreach (var hit in hits)
        {
            var resource = hit.collider.gameObject.GetComponent<Resource>();
            if (resource != null && resource.Type == ResourceConfiguration.ResourceType.Rock)
            {
                return true;
            }
        }

        return false;
    }

    private void Update()
    {
        IsOverRock = UpdateIsOverRock(transform, characterController);

        // Process isCollidingWithResource.
        if (!didCollideWithResource && isCollidingWithResource)
        {
            // Emit event.
            OnResourceCollisionEnter?.Invoke(this, EventArgs.Empty);
        }
        else if (didCollideWithResource && !isCollidingWithResource)
        {
            // Emit event.
            OnResourceCollisionExit?.Invoke(this, EventArgs.Empty);
        }

        // Update state.
        didCollideWithResource = isCollidingWithResource;
        isCollidingWithResource = false;
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        var resource = hit.collider.gameObject.GetComponent<Resource>();
        var wallCollider = hit.collider.gameObject.GetComponent<WallCollider>();

        if (resource == null && wallCollider == null)
            return;

        isCollidingWithResource = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer(LayerHelpers.RESOURCE_PICKUP))
        {
            // Display some floating text feedback.
            other.GetComponent<CollectableResource>().PickUpAndDestroy();
        }
    }
}
