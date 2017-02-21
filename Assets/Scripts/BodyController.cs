using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BodyController : MonoBehaviour {

    GameObject leftLeg;
    GameObject rightLeg;
    Vector3 destination;

    float paceTimer = 2;
    float pace = 480;
    Vector3 lastPacePos;
    Text paceText;

	// Use this for initialization
	void Start () {
        leftLeg = GameObject.Find("Left Leg");
        rightLeg = GameObject.Find("Right Leg");
        paceText = GameObject.Find("Pace Text").GetComponent<Text>();
        destination = transform.position;
        lastPacePos = transform.position;
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

        paceTimer -= Time.deltaTime;
        if (paceTimer < 0) {
            float paceDifference = transform.position.z - lastPacePos.z;
            pace = 540f - (paceDifference * 4f);

            int paceMinutes = (int)(pace / 60);
            int paceSeconds = (int)(pace % 60);
            if (paceMinutes > 8)
                paceText.text = "Pace: C'MON\nPR: 6:40";
            else {
                if (paceSeconds < 10)
                    paceText.text = "Pace: " + paceMinutes + ":0" + paceSeconds + "\nPR: 6:40";
                else
                    paceText.text = "Pace: " + paceMinutes + ":" + paceSeconds + "\nPR: 6:40";
            }
            paceTimer = 2;
            lastPacePos = transform.position;
        }
	}
}
