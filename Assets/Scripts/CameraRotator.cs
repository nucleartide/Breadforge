using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotator : MonoBehaviour
{
    [SerializeField]
    float rotationSpeed = 180f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // horizontal rotation
        var horizontalInput = Input.GetAxis("Mouse X");
        transform.rotation *= Quaternion.AngleAxis(horizontalInput * rotationSpeed * Time.smoothDeltaTime, Vector3.up);

        // vertical rotation
        var verticalInput = Input.GetAxis("Mouse Y");
        transform.rotation *= Quaternion.AngleAxis(-1f * verticalInput * rotationSpeed * Time.smoothDeltaTime, Vector3.right);

        var angles = transform.localEulerAngles;
        angles.z = 0f; // get rid of dutching if any was introduced.

        var x = transform.localEulerAngles.x;
        if (x > 180f && x < 340f)
            x = 340f;
        else if (x < 180f && x > 40f)
            x = 40f;
        transform.localEulerAngles = new Vector3(x, angles.y, 0f);
    }
}
