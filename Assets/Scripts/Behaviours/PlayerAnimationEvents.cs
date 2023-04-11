using UnityEngine;
using System;

public class PlayerAnimationEvents : MonoBehaviour
{
    public event EventHandler OnWalkFootstep;

    public event EventHandler OnPickaxeHit;

    /// <summary>
    /// Different from OnPickaxeHit, in that this is 1 animation frame after OnPickaxeHit.
    /// </summary>
    public event EventHandler OnLatePickaxeHit;

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

    private void WalkFootstep() => OnWalkFootstep?.Invoke(this, EventArgs.Empty);

    private void PickaxeHit() => OnPickaxeHit?.Invoke(this, EventArgs.Empty);

    private void LatePickaxeHit() => OnLatePickaxeHit?.Invoke(this, EventArgs.Empty);

    private void PickaxeHitComplete() => OnPickaxeHitComplete?.Invoke(this, EventArgs.Empty);

    private void PickUp() => OnPickUp?.Invoke(this, EventArgs.Empty);

    private void PickUpComplete() => OnPickUpComplete?.Invoke(this, EventArgs.Empty);

    private void ChopImpact() => OnChopImpact?.Invoke(this, EventArgs.Empty);

    private void LateChopImpact() => OnLateChopImpact?.Invoke(this, EventArgs.Empty);

    private void ChopImpactComplete() => OnChopImpactComplete?.Invoke(this, EventArgs.Empty);
}
