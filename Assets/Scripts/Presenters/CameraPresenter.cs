using UnityEngine;

public class CameraPresenter : MonoBehaviour
{
    [System.Serializable]
    public class Pose
    {
        /// <summary>
        /// Desired transform of the Camera GameObject.
        /// </summary>
        public Transform Camera;

        /// <summary>
        /// Desired transform of the Hand GameObject.
        /// </summary>
        public Transform Hand;
    }

    [SerializeField]
    Pose observePlayingField;

    [SerializeField]
    Pose neutral;

    [SerializeField]
    Pose observeHand;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            Debug.Log("W was pressed");
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            Debug.Log("S was pressed");
        }
    }

    // [ ] set pose of relevant objects
    // [ ] ease between the poses
}
