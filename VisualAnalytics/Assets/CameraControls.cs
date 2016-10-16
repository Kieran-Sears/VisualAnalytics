using UnityEngine;
using System.Collections;


public class CameraControls : MonoBehaviour {
    public GameObject player;
    public float cameraSensitivity = 90;
    public float climbSpeed = 4;
    public float normalMoveSpeed = 10;
    public float slowMoveFactor = 0.25f;
    public float fastMoveFactor = 3;
    public bool lockRotation = false;
    public Camera[] cams;
    public Transform target;
    public float cameraSpeed = 10f;
    public float offset = 10f;
    public bool smoothFollow = true;
    private bool playerCameraOn = true;
    private bool analysisCameraOn;
    private float rotationX = 0.0f;
    private float rotationY = 0.0f;

    void Start() {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void FixedUpdate() {
        if (playerCameraOn) {

            if (!lockRotation) {
                rotationX += Input.GetAxis("Mouse X") * cameraSensitivity * Time.deltaTime;
                rotationY += Input.GetAxis("Mouse Y") * cameraSensitivity * Time.deltaTime;
                rotationY = Mathf.Clamp(rotationY, -90, 90);

                player.transform.localRotation = Quaternion.AngleAxis(rotationX, Vector3.up);
                player.transform.localRotation *= Quaternion.AngleAxis(rotationY, Vector3.left);
            }

            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) {
                player.transform.position += player.transform.forward * (normalMoveSpeed * fastMoveFactor) * Input.GetAxis("Vertical") * Time.deltaTime;
                player.transform.position += player.transform.right * (normalMoveSpeed * fastMoveFactor) * Input.GetAxis("Horizontal") * Time.deltaTime;
            } else if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl)) {
                player.transform.position += player.transform.forward * (normalMoveSpeed * slowMoveFactor) * Input.GetAxis("Vertical") * Time.deltaTime;
                player.transform.position += player.transform.right * (normalMoveSpeed * slowMoveFactor) * Input.GetAxis("Horizontal") * Time.deltaTime;
            } else {
                player.transform.position += player.transform.forward * normalMoveSpeed * Input.GetAxis("Vertical") * Time.deltaTime;
                player.transform.position += player.transform.right * normalMoveSpeed * Input.GetAxis("Horizontal") * Time.deltaTime;
            }

            if (Input.GetKey(KeyCode.Q)) { player.transform.position += transform.up * climbSpeed * Time.deltaTime; }
            if (Input.GetKey(KeyCode.E)) { player.transform.position -= transform.up * climbSpeed * Time.deltaTime; }



        } else if (analysisCameraOn) {
            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) {
                cams[1].transform.position += cams[1].transform.forward * (normalMoveSpeed * fastMoveFactor) * Input.GetAxis("Vertical") * Time.deltaTime;
                cams[1].transform.position += cams[1].transform.right * (normalMoveSpeed * fastMoveFactor) * Input.GetAxis("Horizontal") * Time.deltaTime;
            } else if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl)) {
                cams[1].transform.position += cams[1].transform.forward * (normalMoveSpeed * slowMoveFactor) * Input.GetAxis("Vertical") * Time.deltaTime;
                cams[1].transform.position += cams[1].transform.right * (normalMoveSpeed * slowMoveFactor) * Input.GetAxis("Horizontal") * Time.deltaTime;
            } else {
                cams[1].transform.position += cams[1].transform.forward * normalMoveSpeed * Input.GetAxis("Vertical") * Time.deltaTime;
                cams[1].transform.position += cams[1].transform.right * normalMoveSpeed * Input.GetAxis("Horizontal") * Time.deltaTime;
            }

            if (Input.GetKey(KeyCode.Q)) { cams[1].transform.position += transform.up * climbSpeed * Time.deltaTime; }
            if (Input.GetKey(KeyCode.E)) { cams[1].transform.position -= transform.up * climbSpeed * Time.deltaTime; }

            //if (target) {
            //    Vector3 newPosition = transform.position;
            //    newPosition.x = target.position.x;
            //    newPosition.z = target.position.z - offset;

            //    if (smoothFollow) {
            //        transform.position = newPosition;
            //    } else {
            //        transform.position = Vector3.Lerp(transform.position, newPosition, cameraSpeed * Time.deltaTime);
            //    }
            //}
        }
        if (Input.GetKeyDown(KeyCode.End)) {
            Cursor.visible = (Cursor.visible == false) ? true : false;
            Cursor.lockState = CursorLockMode.None;
        }
        if (Input.GetKeyDown(KeyCode.PageUp)) {
            cameraAnalysis();
        }
        if (Input.GetKeyDown(KeyCode.PageDown)) {
            cameraPlayer();
        }
    }

    public void cameraPlayer() {
        climbSpeed = 4;
        normalMoveSpeed = 10;
        slowMoveFactor = 0.25f;
        fastMoveFactor = 3;
        cams[0].enabled = true;
        cams[1].enabled = false;
        analysisCameraOn = false;
        playerCameraOn = true;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void cameraAnalysis() {
        climbSpeed = 14;
        normalMoveSpeed = 30;
        slowMoveFactor = 0.25f;
        fastMoveFactor = 2;
        cams[0].enabled = false;
        cams[1].enabled = true;
        analysisCameraOn = true;
        playerCameraOn = false;
        Cursor.lockState = CursorLockMode.None;
    }
}

