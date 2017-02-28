using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegController : MonoBehaviour {


    public KeyCode key;
    public float distanceToTravel;
    public GameObject otherLeg;

    public Vector3 destination;

    // Use this for initialization
    void Start () {
        destination = transform.position;
	}

    // Update is called once per frame
    void Update() {
        if (BodyController.keepRunning) {

            if (Input.GetKeyDown(key) && transform.localPosition.z < distanceToTravel / 2 && transform.position.z >= destination.z - 1) {

                destination = transform.position + (transform.forward * distanceToTravel * 2);
            }
            if (Mathf.Abs(transform.position.z - destination.z) < .5f)
                transform.position = destination;
            else
                transform.position = Vector3.Lerp(transform.position, destination, .45f);
        }
    }
}