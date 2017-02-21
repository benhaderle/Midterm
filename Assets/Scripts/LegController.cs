using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegController : MonoBehaviour {

    public KeyCode key;
    public float distanceToTravel;
    public GameObject otherLeg;

    Vector3 destination;

    // Use this for initialization
    void Start () {
        destination = transform.position;
	}
	
	// Update is called once per frame
	void Update () {


        if (Input.GetKeyDown(key) && transform.localPosition.z < 1 && transform.position == destination && otherLeg.transform.localPosition.z > -otherLeg.GetComponent<LegController>().distanceToTravel + 1) {
            destination = transform.position + (transform.forward * distanceToTravel); 
        }
        if (Mathf.Abs(transform.position.z - destination.z) < 1)
            transform.position = destination;
        else 
            transform.position = Vector3.Lerp(transform.position, destination, .4f);
	}
}