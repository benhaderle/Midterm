using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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

        paceTimer = 0;
        lowestTime = paceTime;
        pressedOnce = false;
	}

    public void Stop() {
        keeping = false;

        //paceTime = 5f;
        lowestTime = paceTime;
        paceTimer = 0;

        keep.gameObject.SetActive(false);
        stop.gameObject.SetActive(false);
        story.gameObject.SetActive(false);

        rightLeg.GetComponent<LegController>().distanceToTravel = .5f;
        leftLeg.GetComponent<LegController>().distanceToTravel = .5f;
    }

    public void Keep() {
        keeping = true;
        rightLeg.GetComponent<LegController>().distanceToTravel -= .5f;
        
        lowestTime = paceTime;
        paceTimer = 0;

        keep.gameObject.SetActive(false);
        stop.gameObject.SetActive(false);
        story.gameObject.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {

        destination = new Vector3(transform.position.x, transform.position.y, Mathf.Lerp(leftLeg.transform.position.z, rightLeg.transform.position.z, .5f));

        if (Mathf.Abs(transform.position.z - destination.z) < .4f)
            transform.position = destination;
        else
            transform.position = Vector3.Lerp(transform.position, destination, .2f);

        paceText.text = "Time to Move: " + paceTimer.ToString("n1") + "\nLowest Time : " + lowestTime.ToString("n1");

         if (lowestTime < 1f) {
            keep.gameObject.SetActive(true);
            stop.gameObject.SetActive(true);
            story.gameObject.SetActive(true);

            leftLeg.transform.localPosition = new Vector3(leftLeg.transform.localPosition.x, leftLeg.transform.localPosition.y, 0);
            rightLeg.transform.localPosition = new Vector3(rightLeg.transform.localPosition.x, rightLeg.transform.localPosition.y, 0);
        }
      

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
