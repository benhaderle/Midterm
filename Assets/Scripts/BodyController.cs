using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BodyController : MonoBehaviour {

    GameObject leftLeg;
    GameObject rightLeg;
    Vector3 destination;

    public string[] thoughtTexts;
    int thoughtIndex = 0;

    float paceTimer;
    public float paceTime;
    float pace = 480;
    float lastPacePos;
    float paceAvg;
    float paceCount;
    Text paceText;


    public Button run;
    public Button stop;
    public Button thought;
    public Text clock;
    public Image mirror;

    public Text runEndText;
    public Button runEndButton;
    public Image blackImage;

    public GameObject firstDoctor;
    public GameObject xray;
    public GameObject secondDoctor;
    public GameObject mri;
    public GameObject thirdDoctor;

    public static bool keepRunning = false;
    bool stopped = false;

    int waitingDays = 3;
    int[] waitingAmounts = new int[] { 1, 2, 7, 7, 30 };
    int waitingIndex = 0;

	// Use this for initialization
	void Start () {
        leftLeg = GameObject.Find("Left Leg");
        rightLeg = GameObject.Find("Right Leg");
        paceText = GameObject.Find("Pace Text").GetComponent<Text>();
        destination = transform.position;

        lastPacePos = transform.position.z;
        paceTimer = paceTime;
        paceAvg = 0;
        paceCount = 0;
	}

    public void Stop() {
        keepRunning = false;
        stopped = true;

        run.gameObject.SetActive(false);
        stop.gameObject.SetActive(false);
        thought.gameObject.SetActive(false);
        clock.gameObject.SetActive(false);
        mirror.gameObject.SetActive(false);

        blackImage.gameObject.SetActive(true);
        runEndText.gameObject.SetActive(true);
        runEndButton.gameObject.SetActive(true);

        runEndText.text = "You have a doctor's appointment in " + waitingDays + " days.";
    }  

    public void Run() {
        keepRunning = true;
        rightLeg.GetComponent<LegController>().distanceToTravel *= .6f;

        transform.position = new Vector3(transform.position.x, transform.position.y, -430);
        destination = transform.position;
        lastPacePos = transform.position.z;
        leftLeg.transform.localPosition = new Vector3(leftLeg.transform.localPosition.x, leftLeg.transform.localPosition.y, 0);
        leftLeg.GetComponent<LegController>().destination = leftLeg.transform.position;
        rightLeg.transform.localPosition = new Vector3(rightLeg.transform.localPosition.x, rightLeg.transform.localPosition.y, 0);
        rightLeg.GetComponent<LegController>().destination = rightLeg.transform.position;

        run.gameObject.SetActive(false);
        stop.gameObject.SetActive(false);
        thought.gameObject.SetActive(false);
        clock.gameObject.SetActive(false);
        mirror.gameObject.SetActive(false);

        pace = paceTime;
        paceTimer = 0;
        paceAvg = 0;
        paceCount = 0;
    }

    public void NextDay() {
        if (!stopped) {

            run.gameObject.SetActive(true);
            stop.gameObject.SetActive(true);
            thought.gameObject.SetActive(true);
            clock.gameObject.SetActive(true);
            mirror.gameObject.SetActive(true);

            runEndText.gameObject.SetActive(false);
            runEndButton.gameObject.SetActive(false);
            blackImage.gameObject.SetActive(false);

            thought.GetComponentInChildren<Text>().text = thoughtTexts[thoughtIndex];
            if (thoughtIndex < thoughtTexts.Length - 1)
                thoughtIndex++;
        }
        else {
            waitingDays--;

            if (waitingIndex == 0 || waitingIndex == 2 || waitingIndex == 4)
                runEndText.text = "You have a doctor's appointment in ";
            else if (waitingIndex == 1)
                runEndText.text = "You have an xray in ";
            else if (waitingIndex == 3)
                runEndText.text = "You have an MRI in ";
            else if (waitingIndex == 5)
                runEndText.text = "You can run again in ";

            if (waitingDays != 1)
                runEndText.text += waitingDays + " days.";
            else
                runEndText.text += waitingDays + " day.";

            if (waitingDays < 0) {
                if (waitingIndex == 0) {

                    firstDoctor.SetActive(true);
                }
                else if (waitingIndex == 1) {

                    xray.SetActive(true);
                }
                else if (waitingIndex == 2) {

                    secondDoctor.SetActive(true);
                }
                else if (waitingIndex == 3) {

                    mri.SetActive(true);
                }
                else if (waitingIndex == 4) {
                    thirdDoctor.SetActive(true);
                }
                else if (waitingIndex == 5)
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);

                waitingDays = waitingAmounts[waitingIndex];
                waitingIndex++;
            }
            else {
                firstDoctor.SetActive(false);
                xray.SetActive(false);
                secondDoctor.SetActive(false);
                mri.SetActive(false);
                thirdDoctor.SetActive(false);
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (keepRunning) {
            
            //moving the body
            destination = new Vector3(transform.position.x, transform.position.y, Mathf.Lerp(leftLeg.transform.position.z, rightLeg.transform.position.z, .5f));

            if (Mathf.Abs(transform.position.z - destination.z) < .4f)
                transform.position = destination;
            else
                transform.position = Vector3.Lerp(transform.position, destination, .2f);

            //updating pace/distance text
            if (pace % 60 < 10)
                paceText.text = "Pace " + (int)(pace / 60) + ":0" + (int)(pace % 60) + "\nDistance Left " + ((1000 - (transform.position.z + 430)) / 1000 * 7f).ToString("n1") + "mi";
            else
                paceText.text = "Pace " + (int)(pace / 60) + ":" + (int)(pace % 60) + "\nDistance Left " + ((1000 - (transform.position.z + 430)) / 1000 * 7f).ToString("n1") + "mi";

            paceTimer -= Time.deltaTime;

            //figuring out pace
            if (paceTimer < 0) {
                if (transform.position.z - lastPacePos == 0)
                    pace = 0;
                else {

                    pace = 450 - ((transform.position.z - lastPacePos - 10) / paceTime);
                    paceCount++;
                    paceAvg += pace;
                }

                paceTimer = paceTime;
                lastPacePos = transform.position.z;
            }
            if (transform.position.z >= 570) {
                keepRunning = false;
                runEndText.gameObject.SetActive(true);
                runEndButton.gameObject.SetActive(true);
                blackImage.gameObject.SetActive(true);

                paceAvg /= paceCount;
                if (paceAvg % 60 < 10)
                    runEndText.text = "You ran 7 miles at an average of " + (int)(paceAvg / 60) + ":0" + (int)(paceAvg % 60) + ".";
                else
                    runEndText.text = "You ran 7 miles at an average of " + (int)(paceAvg / 60) + ":" + (int)(paceAvg % 60) + ".";
            }

        }
        else if (stopped) {
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
