using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    GameObject player;
    Vector3 offset;

    public float cameraMoveSpeed = 120.0f;
    public GameObject cameraFollowObj;
    Vector3 followPos;
    public float clampAngle = 50.0f;
    public float inputSensitivity = 100;
    public GameObject cameraObj;
    public GameObject playerObj;
    public float camDistanceXToPayer;
    public float camDistanceYToPayer;
    public float camDistanceZToPayer;
    public float mouseX;
    public float mouseY;
    public float finalInputX;
    public float finalInputZ;
    public float smoothX;
    public float smoothY;
    private float rotY = 0.0f;
    private float rotX = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        this.player = GameObject.FindGameObjectWithTag("Player");
        this.offset = transform.position - this.player.transform.position;

        //Vector3 rot = transform.localRotation.eulerAngles;
        //rotY = rot.y;
        //rotX = rot.x;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = this.offset + this.player.transform.position;
        //transform.RotateAround(this.player.transform.position, Vector3.up * Input.GetAxis("Mouse Y"), 50  * Time.deltaTime);
        //transform.LookAt(this.player.transform.position);

        //// setup the rotation of the sticks here
        //float inputX = Input.GetAxis("RightStickHorizontal");
        //float inputZ = Input.GetAxis("RightStickVertical");
        //mouseX = Input.GetAxis("Mouse X");
        //mouseY = Input.GetAxis("Mouse Y");
        //finalInputX = inputX + mouseX;
        //finalInputZ  = inputZ + mouseY;

        //rotY += finalInputX * inputSensitivity * Time.deltaTime;
        //rotX += finalInputZ * inputSensitivity * Time.deltaTime;

        //rotX = Mathf.Clamp(rotX, -clampAngle, clampAngle);

        //Quaternion localRotation = Quaternion.Euler(rotX, rotY, 0.0f);
        //transform.rotation = localRotation;
    }

    private void LateUpdate()
    {
        transform.position = this.offset + this.player.transform.position;
        //transform.RotateAround(this.player.transform.position, Vector3.up * Input.GetAxis("Mouse Y"), 50 * Time.deltaTime);
        //transform.LookAt(this.player.transform.position);
    }

    //private void CameraUpdater()
    //{
    //    //set the target object to follow
    //    Transform target = cameraFollowObj.transform;

    //    //move towards the game object that is the target
    //    float step = cameraMoveSpeed * Time.deltaTime;
    //    transform.position = Vector3.MoveTowards(transform.position, target.position, step);
    //}
}
