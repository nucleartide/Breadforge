using UnityEngine;

public class CollectableResource : MonoBehaviour
{
    private Vector3 velocity;

    [SerializeField]
    private Vector3 gravity = new Vector3(0f, -9.8f, 0f);

    [SerializeField]
    [NotNull]
    private MoreMountains.Feedbacks.MMF_Player pickUpFeedbacks;

    [SerializeField]
    [NotNull]
    private GameObject collectableResourceVisual;

    public void SetRandomInitialImpulse()
    {
        var randomVelocity = Random.insideUnitSphere;
        velocity = new Vector3(1.5f * randomVelocity.x, 2f + 2.5f * Mathf.Abs(randomVelocity.y), 1.5f * randomVelocity.z);
    }

    private void Update()
    {
        // Apply acceleration due to gravity.
        velocity += gravity * Time.deltaTime;

        // Apply velocity.
        transform.position += velocity * Time.deltaTime;

        // Handle case where position extends beneath ground.
        if (transform.position.y < 0f)
        {
            // Halve velocity.
            if (velocity.magnitude < .2f)
                velocity = Vector3.zero;
            else
                velocity *= .5f;

            // Clamp position so it stays at ground level.
            transform.position = new Vector3(transform.position.x, 0f, transform.position.z);
        }
    }

    /// <summary>
    /// Use this instead of calling Destroy() to show some floating text when picked up.
    /// </summary>
    public void PickUpAndDestroy()
    {
        // Hide object.
        collectableResourceVisual.SetActive(false);

        // Set floating text's display text.
        var floatingText = pickUpFeedbacks.GetFeedbackOfType<MoreMountains.Feedbacks.MMF_FloatingText>();
		floatingText.Value = "+20 Copper"; // Factorio shows something like +20 Copper (40), where the parenthetical value is the current stack's value

        // Play user feedback.
        pickUpFeedbacks.PlayFeedbacks(transform.position);

        // Mark object for destruction after feedback is over.
        Destroy(gameObject, 2f);
    }
}
