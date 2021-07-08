using UnityEngine;

public class RotateCamera : MonoBehaviour
{
    public float rotateSpeed = 100.0f;
    public int invertPitch = 1;

    public float pitch;
    public float yaw;


    private void Update()
    {
        if (Input.touchCount == 1)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                pitch += Input.GetTouch(0).deltaPosition.y * rotateSpeed * invertPitch * Time.deltaTime;
                yaw -= Input.GetTouch(0).deltaPosition.x * rotateSpeed * invertPitch * Time.deltaTime;
                //limit so we dont do backflips
                pitch = Mathf.Clamp(pitch, -80, 80);
                //do the rotations of our camera
                transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
            }
        }
    }
}