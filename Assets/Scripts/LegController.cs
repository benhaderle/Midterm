using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegController : MonoBehaviour {


    public KeyCode key;
    public float distanceToTravel;
    public GameObject otherLeg;
    public GameObject keyText;
    public GameObject arm;

    public Vector3 destination;
    AudioSource stepSound;

    // Use this for initialization
    void Start () {
        destination = transform.position;
        stepSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update() {
        if (BodyController.keepRunning) {

            if (Input.GetKeyDown(key) && transform.localPosition.z < distanceToTravel / 2 && transform.position.z >= destination.z - 1) {
                keyText.SetActive(false);
                destination = transform.position + (transform.forward * distanceToTravel * 2);
                stepSound.PlayOneShot(stepSound.clip);
            }
                if (Mathf.Abs(transform.position.z - destination.z) < .5f)
                    transform.position = destination;
                else
                    transform.position = Vector3.Lerp(transform.position, destination, .45f);
            }
           
        }
    
}