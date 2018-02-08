using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    //The time in which the camera moves to the desired position
    public float dampTime = 0.2f;
    //Distance from the edge of the camera before the camera moves
    public float screenEdgeBuffer = 4f;
    //Minimum zoom distance
    public float minZoomSize = 6.5f;
    //array of players
    public GameObject[] targets;

    private Camera cam;
    //Zoom speed
    private float zoomSpeed;
    //The speed in which the camera moves to the desired position
    private Vector3 moveVelocity;

    private Vector3 desiredPosition;

    private void Awake()
    {
        cam = GetComponentInChildren<Camera>();
    }
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        Move();
        Zoom();
    }

    private void Move()
    {
        FindAveragePosition();
        transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref moveVelocity, dampTime);
    }

    /// <summary>
    /// Find average position between all the players
    /// </summary>
    private void FindAveragePosition()
    {
       
        Vector3 averagePos = new Vector3();
        int numActiveTargets = 0;

        foreach (GameObject g in targets)
        {
            //Check to see if the players are active
            if (g.gameObject.activeSelf)
            {

                averagePos += g.transform.position;
                numActiveTargets++;
            }
        }

        if (numActiveTargets > 0)
        {
            averagePos /= numActiveTargets;
        }

        desiredPosition = averagePos;
    }

    private void Zoom()
    {
        float requiredSize = FindRequiredSize();
        cam.orthographicSize = Mathf.SmoothDamp(cam.orthographicSize, requiredSize, ref zoomSpeed, dampTime);
    }

    /// <summary>
    /// Determines the distance in which the camera should zoom to keep the characters in view
    /// </summary>
    /// <returns></returns>
    private float FindRequiredSize()
    {
        //Change the desired position of the camera from world space to the camera rig's local space
        Vector3 desiredLocalPos = transform.InverseTransformPoint(desiredPosition);
        float size = 0f;

        foreach (GameObject g in targets)
        {
            if (g.gameObject.activeSelf)
            {
                //Change the position of the characters from world space to the camera rig's local space
                Vector3 targetLocalPos = transform.InverseTransformPoint(g.transform.position);

                Vector3 desiredPosToTarget = targetLocalPos - desiredLocalPos;
                size = Mathf.Max(size, Mathf.Abs(desiredPosToTarget.y));
                size = Mathf.Max(size, Mathf.Abs(desiredPosToTarget.y) / cam.aspect);
            }

        }
        size += screenEdgeBuffer;
        size = Mathf.Max(size, minZoomSize);
        return size;
    }

    public void SetStartPositionAndSize()
    {
        FindAveragePosition();
        transform.position = desiredPosition;
        cam.orthographicSize = FindRequiredSize();
    }

}