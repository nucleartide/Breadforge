using System;
using UnityEngine;

public class Resource : MonoBehaviour
{
    public class OnCollectCompletedArgs : EventArgs
    {
        public float AmountCollected;
    }

    public event EventHandler OnDepleted;

    public event EventHandler<OnCollectCompletedArgs> OnCollectCompleted;

    [NotNull]
    [SerializeField]
    private ResourceConfiguration resourceConfiguration;

    private float quantity;

    private float remainingTime = 0f;

    private void Awake()
    {
        quantity = resourceConfiguration.InitialQuantity;
        ResetRemainingTime();
    }

    private void ResetRemainingTime() => remainingTime = resourceConfiguration.TimeToCollect;

    /// <summary>
    /// Elapse some collection time.
    /// </summary>
    public void Elapse(float dt)
    {
        if (quantity <= 0)
            // No-op.
            return;

        quantity -= resourceConfiguration.CollectedQuantity;
        OnCollectCompleted?.Invoke(this, new OnCollectCompletedArgs { AmountCollected = resourceConfiguration.CollectedQuantity });

        if (quantity <= 0)
            OnDepleted?.Invoke(this, EventArgs.Empty);

        remainingTime -= dt;
        if (remainingTime <= 0f)
            ResetRemainingTime();
    }
}
