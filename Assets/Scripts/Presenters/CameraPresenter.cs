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

    [SerializeField]
    new Transform camera;

    [SerializeField]
    Transform hand;

    enum PoseState
    {
        ObserveHand = 0,
        Neutral,
        ObservePlayingField,
    }

    Pose currentPose;

    void OnEnable()
    {
        currentPose = neutral;
    }

    void Update()
    {
        // [ ] update currentPose state
        // [ ] actually update the pose

        if (Input.GetKeyDown(KeyCode.W))
        {
            Debug.Log("W was pressed");
            camera.transform.position = currentPose.Camera.position;
            camera.transform.rotation = currentPose.Camera.rotation;
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            Debug.Log("S was pressed");
        }
    }

    // [ ] set pose of relevant objects
    // [ ] ease between the poses
}
