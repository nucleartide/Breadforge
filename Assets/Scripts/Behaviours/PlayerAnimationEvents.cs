using UnityEngine;
using System;

public class PlayerAnimationEvents : MonoBehaviour
{
    public event EventHandler OnWalkFootstep;

    public event EventHandler OnPickaxeHit;

    /// <summary>
    /// Different from OnPickaxeHit, in that this event indicates the last frame of the animation.
    /// </summary>
    public event EventHandler OnPickaxeHitComplete;

    public event EventHandler OnPickUp;

    /// <summary>
    /// Different from OnPickUp, in that this event indicates the last frame of the animation.
    /// </summary>
    public event EventHandler OnPickUpComplete;

    public event EventHandler OnChopImpact;

    /// <summary>
    /// Different from OnChopImpact, in that this is 1 animation frame after OnChopImpact.
    /// </summary>
    public event EventHandler OnLateChopImpact;

    /// <summary>
    /// Different from OnChopImpact, in that this event indicates the last frame of the animation.
    /// </summary>
    public event EventHandler OnChopImpactComplete;

    private void WalkFootstep()
    {
#if UNITY_EDITOR
        Debug.Log("[PlayerAnimationEvents] Walk Footstep");
#endif
        OnWalkFootstep?.Invoke(this, EventArgs.Empty);
    }

    private void PickaxeHit()
    {
#if UNITY_EDITOR
        Debug.Log("[PlayerAnimationEvents] Pickaxe Hit");
#endif
        OnPickaxeHit?.Invoke(this, EventArgs.Empty);
    }

    private void PickaxeHitComplete()
    {
#if UNITY_EDITOR
        Debug.Log("[PlayerAnimationEvents] Pickaxe Hit Complete");
#endif
        OnPickaxeHitComplete?.Invoke(this, EventArgs.Empty);
    }

    private void PickUp()
    {
#if UNITY_EDITOR
        Debug.Log("[PlayerAnimationEvents] Pick Up");
#endif
        OnPickUp?.Invoke(this, EventArgs.Empty);
    }

    private void PickUpComplete()
    {
#if UNITY_EDITOR
        Debug.Log("[PlayerAnimationEvents] Pick Up Complete");
#endif
        OnPickUpComplete?.Invoke(this, EventArgs.Empty);
    }

    private void ChopImpact()
    {
#if UNITY_EDITOR
        Debug.Log("[PlayerAnimationEvents] Chop Impact");
#endif
        OnChopImpact?.Invoke(this, EventArgs.Empty);
    }

    private void LateChopImpact()
    {
#if UNITY_EDITOR
        Debug.Log("[PlayerAnimationEvents] Late Chop Impact");
#endif
        OnLateChopImpact?.Invoke(this, EventArgs.Empty);
    }

    private void ChopImpactComplete()
    {
#if UNITY_EDITOR
        Debug.Log("[PlayerAnimationEvents] Chop Impact Complete");
#endif
        OnChopImpactComplete?.Invoke(this, EventArgs.Empty);
    }
}
