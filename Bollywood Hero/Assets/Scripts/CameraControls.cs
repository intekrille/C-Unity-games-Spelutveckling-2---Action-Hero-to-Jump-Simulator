using UnityEngine;

public class CameraControls : MonoBehaviour {

    [SerializeField] float minimumY = -90, maximumY = 90;
    float rotationY = 0;

    void Update () {
        rotationY += Input.GetAxis("Mouse Y") * Settings.GetMouseSensitivity() * Time.deltaTime;
        rotationY = Mathf.Clamp(rotationY, -90, 90);
        transform.localEulerAngles = new Vector3(-rotationY, 0, 0);
    }
}
