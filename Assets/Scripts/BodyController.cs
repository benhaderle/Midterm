using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BodyController : MonoBehaviour {

    GameObject leftLeg;
    GameObject rightLeg;
    Vector3 destination;

    float paceTimer;
    float lowestTime;
    public float paceTime;
    float pace = 480;
    public float[] paceGoals;
    Vector3 lastPacePos;
    Text paceText;
    bool pressedOnce;

    public Button keep;
    public Button stop;
    public Text story;
    bool keeping = true;

	// Use this for initialization
	void Start () {
        leftLeg = GameObject.Find("Left Leg");
        rightLeg = GameObject.Find("Right Leg");
        paceText = GameObject.Find("Pace Text").GetComponent<Text>();
        destination = transform.position;
        lastPacePos = transform.position;

        paceTimer = paceTime;
        lowestTime = paceTime;
        pressedOnce = false;
	}

    public void Stop() {
        keeping = false;

        paceTime = 5f;
        lowestTime = paceTime;
        paceTimer = paceTime;

        keep.gameObject.SetActive(false);
        stop.gameObject.SetActive(false);
        story.gameObject.SetActive(false);

        rightLeg.GetComponent<LegController>().distanceToTravel = .5f;
        leftLeg.GetComponent<LegController>().distanceToTravel = .5f;
    }

    public void Keep() {
        keeping = true;
        rightLeg.GetComponent<LegController>().distanceToTravel -= 1f;

        paceTime = 5f;
        lowestTime = paceTime;
        paceTimer = paceTime;

        keep.gameObject.SetActive(false);
        stop.gameObject.SetActive(false);
        story.gameObject.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
     //   if (keeping) {

            if (Input.GetKeyDown(KeyCode.Space) && !pressedOnce) {

                if (leftLeg.transform.localPosition.z > .3f)
                    destination.z += leftLeg.GetComponent<LegController>().distanceToTravel;
                else if (rightLeg.transform.localPosition.z > .3f)
                    destination.z += rightLeg.GetComponent<LegController>().distanceToTravel;
                pressedOnce = true;
            }

            if (destination.z - transform.position.z < .1f) {
                pressedOnce = false;
                transform.position = destination;
            }
            transform.position = Vector3.Lerp(transform.position, destination, .2f);

            if (lastPacePos.z < transform.position.z && Mathf.Abs(rightLeg.transform.localPosition.z) < rightLeg.GetComponent<LegController>().distanceToTravel - 1
                && Mathf.Abs(leftLeg.transform.localPosition.z) < 1) {
                lastPacePos = transform.position;
                paceTime -= .1f;
                paceTimer = paceTime;

                if (lowestTime > paceTime)
                    lowestTime = paceTime;
            }


            paceTimer -= Time.deltaTime;

            paceText.text = "Time to Move: " + paceTimer.ToString("n1") + "\nLowest Time : " + lowestTime.ToString("n1");

            if (paceTimer < 0) {
                paceTime = lowestTime + .1f;
                paceTimer = paceTime;

            }
            if (lowestTime < 1.5f) {
                keep.gameObject.SetActive(true);
                stop.gameObject.SetActive(true);
                story.gameObject.SetActive(true);

            leftLeg.transform.localPosition = new Vector3(leftLeg.transform.localPosition.x, leftLeg.transform.localPosition.y, 0);
            rightLeg.transform.localPosition = new Vector3(rightLeg.transform.localPosition.x, rightLeg.transform.localPosition.y, 0);
        }
      //  }

           /*  OLD CODE 
           
            float paceDifference = transform.position.z - lastPacePos.z;

            int paceMinutes = (int)(pace / 60);
            int paceSeconds = (int)(pace % 60);
            int goalMinutes = (int)(paceGoals[0] / 60);
            int goalSeconds = (int)(paceGoals[0] % 60);

            if (paceMinutes > 8)
                paceText.text = "Pace: C'MON\nPR: 4:22";
            else {
                if (paceSeconds < 10)
                    paceText.text = "Pace: " + paceMinutes + ":0" + paceSeconds + "\nGoal: " + goalMinutes + ":" + goalSeconds + "\nPR: 4:22";
                else
                    paceText.text = "Pace: " + paceMinutes + ":" + paceSeconds + "\nGoal: " + goalMinutes + ":" + goalSeconds + "\nPR: 4:22";
            }
            paceTimer = 2;
            lastPacePos = transform.position;*/
        }
	
}
