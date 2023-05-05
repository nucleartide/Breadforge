using UnityEngine;

public class CollectableResource : MonoBehaviour
{
    private Vector3 velocity;

    [SerializeField]
    private Vector3 gravity = new Vector3(0f, -9.8f, 0f);

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
}
