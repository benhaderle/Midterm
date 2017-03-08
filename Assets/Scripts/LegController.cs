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
    public float armDest;


    // Use this for initialization
    void Start () {
        destination = transform.position;
        armDest = arm.transform.rotation.eulerAngles.x;
    }

    // Update is called once per frame
    void Update() {
        if (BodyController.keepRunning) {

            if (Input.GetKeyDown(key) && transform.localPosition.z < distanceToTravel / 2 && transform.position.z >= destination.z - 1) {
                keyText.SetActive(false);
                destination = transform.position + (transform.forward * distanceToTravel * 2);

                if (armDest == 310f) {
                    armDest = 100f;
                    otherLeg.GetComponent<LegController>().armDest = 310;
                }
                else if (armDest == 100f) {
                    armDest = 310f;
                    otherLeg.GetComponent<LegController>().armDest = 100;
                }
                else {
                    armDest = 100f;
                    otherLeg.GetComponent<LegController>().armDest = 310;
                }
            }
                if (Mathf.Abs(transform.position.z - destination.z) < .5f)
                    transform.position = destination;
                else
                    transform.position = Vector3.Lerp(transform.position, destination, .45f);
            }

         //   Debug.Log(armDest);
        Debug.Log(otherLeg.GetComponent<LegController>().armDest);

            if (Mathf.Abs(armDest - arm.transform.eulerAngles.x) < 30)
                arm.transform.eulerAngles = new Vector3(armDest, arm.transform.eulerAngles.y, arm.transform.eulerAngles.z);
            else
                arm.transform.eulerAngles = new Vector3(Mathf.LerpAngle(arm.transform.eulerAngles.x, armDest, .45f), arm.transform.eulerAngles.y, arm.transform.eulerAngles.z);
        }
    
}