using UnityEngine;
using System;

public class PlayerAnimationEvents : MonoBehaviour
{
    public event EventHandler OnPickaxeHit;

    private void PickaxeHit() => OnPickaxeHit?.Invoke(this, EventArgs.Empty);
}
