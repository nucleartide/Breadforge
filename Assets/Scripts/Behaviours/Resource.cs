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

    [field: NotNull]
    [field: SerializeField]
    public ResourceConfiguration Configuration
    {
        get;
        private set;
    }

    private float quantity;

    [SerializeField]
    private float remainingTime = 0f;

    public ResourceConfiguration.ResourceType Type
    {
        get
        {
            return Configuration.Type;
        }
    }

    private void Awake()
    {
        quantity = Configuration.InitialQuantity;
        ResetRemainingTime();
    }

    public void ResetRemainingTime() => remainingTime = Configuration.TimeToCollect;

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

        // When countdown is complete,
        if (remainingTime <= 0f)
        {
            // Reset timer.
            ResetRemainingTime();

            // Decrement quantity.
            quantity -= Configuration.CollectedQuantity;

            // Emit events when quantity is deducted.
            if (quantity <= 0)
            {
                OnDepleted?.Invoke(this, EventArgs.Empty);
                Destroy(gameObject);
            }
            else
            {
                OnCollectCompleted?.Invoke(this, new OnCollectCompletedArgs { AmountCollected = Configuration.CollectedQuantity });
            }
        }
    }
}
