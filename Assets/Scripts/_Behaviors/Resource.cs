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
        // No-op if there's nothing to collect.
        if (quantity <= 0)
            return;

        // Decrement time.
        remainingTime -= dt;
        Debug.Log($"Elapsed. Remaining time to next collection is {remainingTime} seconds.");

        // When countdown is complete,
        if (remainingTime <= 0f)
        {
            // Player has collected a batch.
            OnCollectCompleted?.Invoke(this, new OnCollectCompletedArgs { AmountCollected = resourceConfiguration.CollectedQuantity });

            // Reset timer.
            ResetRemainingTime();

            // Decrement quantity.
            quantity -= resourceConfiguration.CollectedQuantity * dt;

            // Do stuff when quantity is depleted.
            if (quantity <= 0)
            {
                OnDepleted?.Invoke(this, EventArgs.Empty);
                Destroy(gameObject);
            }
        }
    }
}
