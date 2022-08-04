using UnityEngine;

public class GameViewPresenter : MonoBehaviour
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
        ObserveHand,
        Neutral,
        ObservePlayingField,
    }

    PoseState currentPose = PoseState.Neutral;

    Pose ToPose(PoseState poseState) => poseState switch
    {
        PoseState.ObserveHand => observeHand,
        PoseState.Neutral => neutral,
        PoseState.ObservePlayingField => observePlayingField,
        _ => throw new System.Exception($"PoseState {poseState} not covered."), // Not great, since it's not a compile-time check.
    };

    void Update()
    {
        // Update current pose.
        var currentPoseInt = (int)currentPose;
        if (Input.GetKeyDown(KeyCode.W))
            currentPoseInt = Mathf.Min((int)currentPose + 1, (int)PoseState.ObservePlayingField);
        else if (Input.GetKeyDown(KeyCode.S))
            currentPoseInt = Mathf.Max((int)currentPose - 1, 0);
        currentPose = (PoseState)currentPoseInt;

        // Update the pose depending on the PoseState.
        var pose = ToPose(currentPose);

        // Ease between positions for the camera and hand.
        if (Vector3.Distance(camera.transform.position, pose.Camera.position) > 0.001f)
        {
            var speed = 2f;
            var step = speed * Time.deltaTime;
            camera.transform.position = Vector3.MoveTowards(camera.transform.position, pose.Camera.position, step);
        }
        if (Vector3.Distance(hand.transform.position, pose.Hand.position) > 0.001f)
        {
            var speed = 2f;
            var step = speed * Time.deltaTime;
            hand.transform.position = Vector3.MoveTowards(hand.transform.position, pose.Hand.position, step);
        }

        // Ease between rotations for the camera and hand.
        {
            var degrees = 90;
            var step = degrees * Time.deltaTime; // max 30 degrees per second
            camera.transform.rotation = Quaternion.RotateTowards(camera.transform.rotation, pose.Camera.rotation, step);
        }
        {
            var degrees = 180;
            var step = degrees * Time.deltaTime; // max 30 degrees per second
            hand.transform.rotation = Quaternion.RotateTowards(hand.transform.rotation, pose.Hand.rotation, step);
        }
    }
}
