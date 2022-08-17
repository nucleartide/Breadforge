using UnityEngine;

public static class Orientation
{
    public static readonly Quaternion FACE_DOWN = Quaternion.AngleAxis(90, Vector3.right);
    public static readonly Quaternion FACE_UP = Quaternion.AngleAxis(-90, Vector3.right);
}
