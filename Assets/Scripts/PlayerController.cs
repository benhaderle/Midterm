using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float forceMod;
    public float torqueMod;

   Rigidbody rightLeg;
   Rigidbody leftLeg;

	// Use this for initialization
	void Start () {
      
        
       
        rightLeg = GameObject.Find("Right Leg").GetComponent<Rigidbody>();
        leftLeg = GameObject.Find("Left Leg").GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.K)) {
            rightLeg.AddForce(transform.forward);
        }


       /* OLD MOVEMENT CODE


        Vector3 force = new Vector3(0, 3, 1);

        if (Input.GetKeyDown(KeyCode.K)) {
            rightThigh.AddForceAtPosition(force * forceMod, transform.position - new Vector3(0, .55f, 0));
           // rightThigh.AddForce(Vector3.forward * forceMod);
           // rightThigh.AddTorque(new Vector3(0,10,1) * torqueMod);
        }
        if (Input.GetKeyDown(KeyCode.S)) {
            leftThigh.AddForceAtPosition(force * forceMod, transform.position - new Vector3(0, .55f, 0));
          //  leftThigh.AddForce(Vector3.forward * forceMod);
          //  leftThigh.AddTorque(new Vector3(0, 10, 1) * torqueMod);
        }*/
    }
}
