using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour {

    private CharacterController cont;
    private Vector3 moveVector;
    private float verticalVel;

    public Collider[] attackHitboxes;
    // Use this for initialization
    void Start()
    {
        cont = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (cont.isGrounded)
        {
            verticalVel = -1;

            if (!cont.isGrounded)
            {
                verticalVel = 10;
            }
        }
        else
        {
            verticalVel -= 14 * Time.deltaTime;
        }

        moveVector = Vector3.zero;
        moveVector.y = verticalVel;

        cont.Move(moveVector * Time.deltaTime);
    }

}
