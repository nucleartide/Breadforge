using UnityEngine;
using System;

public class PlayerAnimationEvents : MonoBehaviour
{
    public event EventHandler OnPickaxeHit;

    public event EventHandler OnWalkFootstep;

    public event EventHandler OnPickUp;

    private void PickaxeHit() => OnPickaxeHit?.Invoke(this, EventArgs.Empty);

    private void WalkFootstep() => OnWalkFootstep?.Invoke(this, EventArgs.Empty);

    private void PickUp() => OnPickUp?.Invoke(this, EventArgs.Empty);
}
