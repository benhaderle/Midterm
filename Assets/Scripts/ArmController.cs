using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmController : MonoBehaviour {

    public LegController leg;
    Vector3 intialRot;

	// Use this for initialization
	void Start () {
        intialRot = transform.localEulerAngles;
	}
	
	// Update is called once per frame
	void Update () {
        if (BodyController.keepRunning) {
            if (Input.GetKeyDown(leg.key)) {
                transform.localEulerAngles = new Vector3(310f, intialRot.y, intialRot.z);

            }
            else if (Input.GetKeyDown(leg.otherLeg.GetComponent<LegController>().key))
                transform.localEulerAngles = new Vector3(100f, intialRot.y, intialRot.z);
        }
        else {
            transform.localEulerAngles = intialRot;
        }
	}
}
