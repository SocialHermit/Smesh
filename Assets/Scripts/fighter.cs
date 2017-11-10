using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fighter : MonoBehaviour {

    private CharacterController cont;
    private Vector3 moveVector;
    public float verticalVel;

    public Collider[] attackHitboxes;
	// Use this for initialization
	void Start () {
        cont = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.F))
        {
            LaunchAttack(attackHitboxes[0], 5f, 6f);
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            LaunchAttack(attackHitboxes[1], 10f, 12f);
        }

        if (cont.isGrounded)
        {
            verticalVel = -1;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                verticalVel = 10;
            }
        }
        else
        {
            verticalVel -= 14 * Time.deltaTime;
        }

        moveVector = Vector3.zero;
        moveVector.x = Input.GetAxis("Horizontal") * 5;
        moveVector.y = verticalVel;

        cont.Move(moveVector * Time.deltaTime);
    }

    private void LaunchAttack(Collider coll, float d, float t)
    {
        Collider[] cols = Physics.OverlapBox(coll.bounds.center, coll.bounds.extents, coll.transform.rotation, LayerMask.GetMask("Hitbox"));
        foreach (Collider c in cols)
        {
            if (c.transform.parent.parent == transform)
            { continue; }

            float damage = d;

            //switch (c.name)
            //{
            //    case "Head":
            //        damage = 30;
            //        break;
            //    case "Body":
            //        damage = 10;
            //        break;
            //    default:
            //        Debug.Log("Unknown part");
            //        break;
            //}
            CharacterController tempCont;
            Vector3 tempVector;

            c.SendMessageUpwards("TakeDamage", damage);

            tempVector = Vector3.zero;
            tempVector.y = t;
            tempCont = c.transform.parent.parent.GetComponent<CharacterController>();

            tempCont.Move(tempVector * Time.deltaTime);
        }
    }
}
