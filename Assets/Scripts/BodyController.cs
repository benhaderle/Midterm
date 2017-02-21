using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyController : MonoBehaviour {

    GameObject leftLeg;
    GameObject rightLeg;
    Vector3 destination;

	// Use this for initialization
	void Start () {
        leftLeg = GameObject.Find("Left Leg");
        rightLeg = GameObject.Find("Right Leg");
        destination = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space)) {

            if (leftLeg.transform.localPosition.z > 1)
                destination.z += leftLeg.GetComponent<LegController>().distanceToTravel;
            else if (rightLeg.transform.localPosition.z > 1)
                destination.z += rightLeg.GetComponent<LegController>().distanceToTravel;
        }
        if (destination.z - transform.position.z < .1f)
            transform.position = destination;
        transform.position = Vector3.Lerp(transform.position, destination, .2f);
	}
}
