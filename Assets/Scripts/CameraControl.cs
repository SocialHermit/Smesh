using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {
    public float dampTime = 0.2f;
    public float screenEdgeBuffer = 4f;
    public float minZoomSize = 6.5f;
    public Transform[] targets; //array of players;

    private Camera cam;
    private float zoomSpeed;
    private Vector3 moveVelocity;
    private Vector3 desiredPosition;

    private void Awake()
    {
        cam = GetComponentInChildren<Camera>();
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	    	
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

    private void FindAveragePosition()
    {
        Vector3 averagePos = new Vector3();
        int numActiveTargets = 0;

        foreach (Transform g in targets)
        {
            if (g.gameObject.activeSelf)
            {
                averagePos += g.position;
                numActiveTargets++;
            }
        }

        if (numActiveTargets > 0)
        {
            averagePos /= numActiveTargets;
        }

        averagePos.y = transform.position.y;

        desiredPosition = averagePos;
    }

    private void Zoom()
    {
        float requiredSize = FindRequiredSize();
        cam.orthographicSize = Mathf.SmoothDamp(cam.orthographicSize, requiredSize, ref zoomSpeed, dampTime);
    }

    private float FindRequiredSize()
    {
        Vector3 desiredLocalPos = transform.InverseTransformPoint(desiredPosition);
        float size = 0f;

        foreach (Transform g in targets)
        {
            if (g.gameObject.activeSelf)
            {
                Vector3 targetLocalPos = transform.InverseTransformPoint(g.position);
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
